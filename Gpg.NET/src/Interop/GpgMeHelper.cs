using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Gpg.NET.Interop
{
	/// <summary>
	/// Contains various helper functions to make it easier to interface with GpgME.
	/// </summary>
	internal class GpgMeHelper
	{
		/// <summary>
		/// Returns a list of all engines available for use.
		/// </summary>
		public static List<EngineInfo> GetEngines()
		{
			GpgMeEngineInfo info;
			ErrorHandler.Check(GpgMeWrapper.gpgme_get_engine_info(out info));
			return info.ToList();
		}

		/// <summary>
		/// Returns a list of all engines available for use with the given context.
		/// </summary>
		public static List<EngineInfo> GetEngines(GpgContext ctx)
		{
			var engineInfoPtr = GpgMeWrapper.gpgme_ctx_get_engine_info(ctx.Handle);
			var engineInfo = Marshal.PtrToStructure<GpgMeEngineInfo>(engineInfoPtr);
			return engineInfo.ToList();
		}

		/// <summary>
		/// Searches the keyring for all keys that match the given search parameters.
		/// </summary>
		public static IEnumerable<GpgKey> FindKeys(GpgContext ctx, string pattern, bool privateOnly)
		{
			// Start the key listing operation
			ErrorHandler.Check(GpgMeWrapper.gpgme_op_keylist_start(ctx.Handle, pattern, privateOnly));

			// Grab the first key
			IntPtr keyPtr;
			var result = GpgMeWrapper.gpgme_op_keylist_next(ctx.Handle, out keyPtr);
			while (result == GpgMeError.GPG_ERR_NO_ERROR)
			{
				// If the operation succeeds, keyPtr will have been assigned a value.
				// Grab the GpgMeKey structure belonging to the pointer
				var key = Marshal.PtrToStructure<GpgMeKey>(keyPtr);

				// Turn the GpgMeKey into a GpgKey (resolves the subkeys and uids linked lists)
				yield return key.ToGpgKey(keyPtr);
				// Try grabbing the next key
				result = GpgMeWrapper.gpgme_op_keylist_next(ctx.Handle, out keyPtr);
			}
			// End the key listing operation
			ErrorHandler.Check(GpgMeWrapper.gpgme_op_keylist_end(ctx.Handle));
		}

		/// <summary>
		/// Searches the keyring for all keys that match the given search parameters,
		/// and stops the search as soon as the first match is found.
		/// </summary>
		public static GpgKey FindKey(GpgContext ctx, string pattern, bool privateOnly)
		{
			ErrorHandler.Check(GpgMeWrapper.gpgme_op_keylist_start(ctx.Handle, pattern, privateOnly));
			// Grab the first key
			IntPtr keyPtr;
			var result = GpgMeWrapper.gpgme_op_keylist_next(ctx.Handle, out keyPtr);
			if (result != GpgMeError.GPG_ERR_NO_ERROR) return null;
			// If the operation succeeds, keyPtr will have been assigned a value.
			// Grab the GpgMeKey structure belonging to the pointer
			var key = Marshal.PtrToStructure<GpgMeKey>(keyPtr);

			// Turn the GpgMeKey into a GpgKey (resolves the subkeys and uids linked lists)
			return key.ToGpgKey(keyPtr);
		}

		public static int Read(IntPtr dh, byte[] buffer, int offset, int count)
		{
			// Allocate an unmanaged memory buffer
			var bufPtr = Marshal.AllocHGlobal(count);
			// Allow GpgMe to fill the unmanaged buffer
			var read = GpgMeWrapper.gpgme_data_read(dh, bufPtr, count);
			// Copy the unmanaged buffer into the managed buffer
			Marshal.Copy(bufPtr, buffer, offset, count);
			// And free the unmanaged buffer
			Marshal.FreeHGlobal(bufPtr);
			return read;
		}

		public static void Write(IntPtr dh, byte[] buffer, int offset, int count)
		{
			// Copy the relevant data into a zero-offset buffer
			var copyBuffer = new byte[count];
			Array.Copy(buffer, offset, copyBuffer, 0, count);
			// Send the zero-offset buffer to GPG
			var written = GpgMeWrapper.gpgme_data_write(dh, copyBuffer, count);
			if (written != count)
			{
				throw new InvalidOperationException("Unable to write all data to the GPG buffer.");
			}
		}

		/// <summary>
		/// Read a GpgME data buffer into a C# byte buffer.
		/// </summary>
		public static byte[] ReadBuffer(IntPtr dh, int bufferSize = 4096)
		{
			var allData = new List<byte>();
			int read;
			do
			{
				// Allocate an unmanaged memory buffer
				var bufPtr = Marshal.AllocHGlobal(bufferSize);
				// Allow GpgMe to fill the unmanaged buffer
				read = GpgMeWrapper.gpgme_data_read(dh, bufPtr, bufferSize);

				// Copy the unmanaged buffer into a managed buffer
				var buffer = new byte[bufferSize];
				Marshal.Copy(bufPtr, buffer, 0, bufferSize);
				// And free the unmanaged buffer
				Marshal.FreeHGlobal(bufPtr);
				allData.AddRange(buffer.Take(read));

			} while (read == bufferSize);

			// All those copies are pretty slow, but for now, we'll just have to live with that.
			return allData.ToArray();
		}
	}
}