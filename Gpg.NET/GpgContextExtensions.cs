using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Text;

namespace Gpg.NET
{
	public static class GpgContextExtensions
	{
		/// <summary>
		/// Encrypts a string and writes the ciphertext to a file.
		/// </summary>
		/// <param name="context">The <see cref="GpgContext"/> to operate on.</param>
		/// <param name="plaintext">The plaintext to be encrypted.</param>
		/// <param name="outputFilePath">The path where the encrypted file should be saved.</param>
		/// <param name="encryptFlags">The encryption flags to be used.</param>
		/// <param name="overwrite">Whether the existing file should be overwritten if a file already exists at <paramref name="outputFilePath"/>.</param>
		/// <param name="recipients">The GPG keys for which the data should be encrypted.</param>
		public static void EncryptString(this GpgContext context, string plaintext, string outputFilePath, IEnumerable<GpgKey> recipients, EncryptFlags encryptFlags = EncryptFlags.None, bool overwrite = false)
		{
			using (var file = File.Open(outputFilePath, overwrite ? FileMode.Create : FileMode.CreateNew))
			using (var plain = MemoryGpgBuffer.CreateFromString(plaintext))
			using (var cipher = context.Encrypt(plain, recipients, encryptFlags))
			{
				cipher.CopyTo(file);
			}
		}

		/// <summary>
		/// Decrypts a file and returns its contents.
		/// </summary>
		/// <param name="context">The <see cref="GpgContext"/> to operate on.</param>
		/// <param name="inputFilePath">The path to the encrypted file.</param>
		public static string DecryptFile(this GpgContext context, string inputFilePath)
		{
			using (var cipher = MemoryGpgBuffer.CreateFromFile(inputFilePath))
			using (var reader = new StreamReader(context.Decrypt(cipher)))
			{
				return reader.ReadToEnd();
			}
		}

		/// <summary>
		/// Decrypts a file and writes its decrypted content to another file.
		/// </summary>
		/// <param name="context">The <see cref="GpgContext"/> to operate on.</param>
		/// <param name="inputFilePath">The path to the encrypted file.</param>
		/// <param name="outputFilePath">The path where the decrypted file should be saved.</param>
		/// <param name="overwrite">Whether the existing file should be overwritten if a file already exists at <paramref name="outputFilePath"/>.</param>
		public static void DecryptFile(this GpgContext context, string inputFilePath, string outputFilePath, bool overwrite = false)
		{
			using (var file = File.Open(outputFilePath, overwrite ? FileMode.Create : FileMode.CreateNew))
			using (var cipher = MemoryGpgBuffer.CreateFromFile(inputFilePath))
			using (var plain = context.Decrypt(cipher))
			{
				plain.CopyTo(file);
			}
		}

		/// <summary>
		/// Encrypts a file and writes the ciphertext to another file.
		/// </summary>
		/// <param name="context">The <see cref="GpgContext"/> to operate on.</param>
		/// <param name="inputFilePath">The path to the unencrypted file.</param>
		/// <param name="outputFilePath">The path where the encrypted file should be saved.</param>
		/// <param name="encryptFlags">The encryption flags to be used.</param>
		/// <param name="overwrite">Whether the existing file should be overwritten if a file already exists at <paramref name="outputFilePath"/>.</param>
		/// <param name="recipients">The GPG keys for which the data should be encrypted.</param>
		public static void EncryptFile(this GpgContext context, string inputFilePath, string outputFilePath, IEnumerable<GpgKey> recipients, EncryptFlags encryptFlags = EncryptFlags.None, bool overwrite = false)
		{
			using (var file = File.Open(outputFilePath, overwrite ? FileMode.Create : FileMode.CreateNew))
			using (var plain = MemoryGpgBuffer.CreateFromFile(inputFilePath))
			using (var cipher = context.Encrypt(plain, recipients, encryptFlags))
			{
				cipher.CopyTo(file);
			}
		}
	}
}
