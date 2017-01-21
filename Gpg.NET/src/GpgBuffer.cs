using System;
using System.IO;
using System.Text;
using Gpg.NET.Interop;
using Gpg.NET.Interop.GCC;

namespace Gpg.NET
{
	/// <summary>
	/// Represents a data buffer, used for sending data to GPG.
	/// </summary>
	public abstract class GpgBuffer : Stream
	{
		internal IntPtr Handle { get; }

		public override bool CanRead => true;
		public override bool CanSeek => true;
		public override bool CanWrite => true;

		public override long Length
		{
			get
			{
				// Save the current position
				var cur = GpgMeWrapper.gpgme_data_seek(Handle, 0, SeekPosition.Cur);
				// Seeking to an offset of 0 relative to the end will return the position of the final byte
				var end = GpgMeWrapper.gpgme_data_seek(Handle, 0, SeekPosition.End);
				// Restore the original position
				var res = GpgMeWrapper.gpgme_data_seek(Handle, cur, SeekPosition.Set);
				if (cur == -1 || end == -1 || res == -1)
				{
					throw new InvalidOperationException("Failed to get stream length");
				}
				// The length of the stream is simply the position of the final byte plus one.
				return end + 1;
			}
		}

		/// <summary>
		/// Gets or sets the current position within the buffer.
		/// </summary>
		public override long Position
		{
			get
			{
				// Seeking to an offset of 0 relative to the current position will return the current position.
				var pos = GpgMeWrapper.gpgme_data_seek(Handle, 0, SeekPosition.Cur);
				if (pos == -1) throw new InvalidOperationException("Failed to get Position");
				return pos;
			}
			set
			{
				var pos = GpgMeWrapper.gpgme_data_seek(Handle, (int)value, SeekPosition.Set);
				if (pos == -1) throw new InvalidOperationException("Failed to set Position");
			}
		}
		public override long Seek(long offset, SeekOrigin origin)
		{
			// The values for SeekOrigin (WinAPI) and SeekPosition (GCC) are the same,
			// so we can just cast this. Feels dirty though...
			return GpgMeWrapper.gpgme_data_seek(Handle, (int) offset, (SeekPosition)origin);
		}

		public override void SetLength(long value)
		{
			// TODO: seek to value-1, then return to original position
			throw new NotImplementedException();
		}

		protected GpgBuffer(IntPtr handle)
		{
			Handle = handle;
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return GpgMeHelper.Read(Handle, buffer, offset, count);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			GpgMeHelper.Write(Handle, buffer, offset, count);
		}

		public override void Flush()
		{
			// Data is written directly to the buffer, so Flush() is not required
		}

		/// <summary>
		/// Requests GPG to release the buffer and free the memory it has taken up.
		/// </summary>
		public new void Dispose()
		{
			base.Dispose();
			GpgMeWrapper.gpgme_data_release(Handle);
		}

	}
}
