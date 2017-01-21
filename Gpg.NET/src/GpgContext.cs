using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gpg.NET.Interop;

namespace Gpg.NET
{
	public class GpgContext : IDisposable
	{
		internal IntPtr Handle { get; }

		// TODO: ASCII Armor support: https://www.gnupg.org/documentation/manuals/gpgme/ASCII-Armor.html#ASCII-Armor
		// TODO: Protocol selection: https://www.gnupg.org/documentation/manuals/gpgme/Protocol-Selection.html#Protocol-Selection
		// TODO: Sender selection: https://www.gnupg.org/documentation/manuals/gpgme/Setting-the-Sender.html#Setting-the-Sender
		// TODO: Pinentry selection and loopback implementation: https://www.gnupg.org/documentation/manuals/gpgme/Pinentry-Mode.html#Pinentry-Mode
		// TODO: Keylist mode selection: https://www.gnupg.org/documentation/manuals/gpgme/Key-Listing-Mode.html#Key-Listing-Mode
		// TODO: Key creation: https://www.gnupg.org/documentation/manuals/gpgme/Generating-Keys.html#Generating-Keys

		internal GpgContext(IntPtr handle)
		{
			Handle = handle;
		}

		/// <summary>
		/// Creates a new <see cref="GpgContext"/> and returns it.
		/// </summary>
		/// <returns>The <see cref="GpgContext"/> that was created.</returns>
		public static GpgContext CreateContext()
		{
			IntPtr handle;
			ErrorHandler.Check(GpgMeWrapper.gpgme_new(out handle));
			return new GpgContext(handle);
		}

		/// <summary>
		/// Attempts to decrypt the contents of a data buffer.
		/// If required, the user will be prompted for a password using the default pinentry program.
		/// </summary>
		/// <param name="cipher">A DataBuffer containing the data to be decrypted.</param>
		/// <returns>A DataBuffer containing the decrypted data.</returns>
		public GpgBuffer Decrypt(GpgBuffer cipher)
		{
			var output = MemoryGpgBuffer.Create();
			ErrorHandler.Check(GpgMeWrapper.gpgme_op_decrypt(Handle, cipher.Handle, output.Handle));
			output.Position = 0;
			return output;
		}

		/// <summary>
		/// Attempts to encrypt the contents of a data buffer for the given recipient.
		/// </summary>
		/// <param name="plain">A DataBuffer containing the plaintext data to be encrypted.</param>
		/// <param name="recipient">The GPG key for which the data should be encrypted.</param>
		/// <returns>A DataBuffer containing the encrypted data.</returns>
		public GpgBuffer Encrypt(GpgBuffer plain, GpgKey recipient)
		{
			return Encrypt(plain, new[] { recipient });
		}

		/// <summary>
		/// Attempts to encrypt the contents of a data buffer for multiple recipients.
		/// </summary>
		/// <param name="plain">A DataBuffer containing the plaintext data to be encrypted.</param>
		/// <param name="recipients">The GPG keys for which the data should be encrypted.</param>
		/// <returns>A DataBuffer containing the encrypted data.</returns>
		public GpgBuffer Encrypt(GpgBuffer plain, IEnumerable<GpgKey> recipients)
		{
			// Transform the recipient list into a list of GpgME key handles
			var rcpHandles = recipients.Select(rcp => rcp.Handle).ToArray();
			var output = MemoryGpgBuffer.Create();
			ErrorHandler.Check(GpgMeWrapper.gpgme_op_encrypt(Handle, rcpHandles, GpgMeEncryptFlags.AlwaysTrust, plain.Handle, output.Handle));
			output.Position = 0;
			return output;
		}

		/// <summary>
		/// Returns all keys matching the given search parameters.
		/// If no parameters are given, all keys in the keyring will be returned.
		/// </summary>
		/// <param name="pattern">One or more GPG key IDs to search for. The default value (null) matches all keys.</param>
		/// <param name="privateOnly">True if only GPG keys for which a private key is available should be returned, false otherwise.</param>
		/// <returns>A list of GPG keys matching the given search parameters.</returns>
		public IEnumerable<GpgKey> FindKeys(string pattern = null, bool privateOnly = false)
		{
			return GpgMeHelper.GetKeys(this, pattern, privateOnly);
		}

		/// <summary>
		/// Returns the first key matching the given search parameters.
		/// If no parameters are given, all keys in the keyring will be returned.
		/// </summary>
		/// <param name="pattern">One or more GPG key IDs to search for. The default value (null) matches all keys.</param>
		/// <param name="privateOnly">True if only GPG keys for which a private key is available should be returned, false otherwise.</param>
		/// <returns>The first GPG key that matches the given search parameters, or null if there are no matches.</returns>
		public GpgKey FindKey(string pattern = null, bool privateOnly = false)
		{
			return FindKeys(pattern, privateOnly).FirstOrDefault();
		}

		public void Dispose()
		{
			GpgMeWrapper.gpgme_release(Handle);
		}
	}
}
