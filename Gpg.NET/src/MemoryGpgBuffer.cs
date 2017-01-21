using System;
using System.IO;
using System.Text;
using Gpg.NET.Interop;

namespace Gpg.NET
{
	/// <summary>
	/// Represents a memory-backed data buffer, used for sending data to GPG.
	/// </summary>
	public class MemoryGpgBuffer : GpgBuffer
	{
		private MemoryGpgBuffer(IntPtr handle) : base(handle) { }

		/// <summary>
		/// Creates a new, empty memory-mapped buffer.
		/// </summary>
		/// <returns>The buffer that was created.</returns>
		public static MemoryGpgBuffer Create()
		{
			IntPtr handle;
			ErrorHandler.Check(GpgMeWrapper.gpgme_data_new(out handle));
			return new MemoryGpgBuffer(handle);
		}

		/// <summary>
		/// Creates a new memory-mapped buffer and initialises it with the given data.
		/// </summary>
		/// <param name="content">The bytes that should be written to the buffer.</param>
		/// <returns>The buffer that was created.</returns>
		public static MemoryGpgBuffer Create(byte[] content)
		{
			var buffer = Create();
			buffer.Write(content,0, content.Length);
			buffer.Position = 0;
			return buffer;
		}

		/// <summary>
		/// Creates a new memory-mapped buffer and initialises it with text data using the specified encoding.
		/// </summary>
		/// <param name="content">A string of text to be encoded and stored in the buffer.</param>
		/// <param name="encoding">The encoding to be used to encode the text.</param>
		/// <returns>The buffer that was created.</returns>
		public static MemoryGpgBuffer CreateFromString(string content, Encoding encoding)
		{
			return Create(Encoding.UTF8.GetBytes(content));
		}

		/// <summary>
		/// Creates a new memory-mapped buffer and initialises it with UTF-8 encoded text data.
		/// </summary>
		/// <param name="content">A string of text, to be encoded as UTF-8 and stored in the buffer.</param>
		/// <returns>The buffer that was created.</returns>
		public static MemoryGpgBuffer CreateFromString(string content)
		{
			return CreateFromString(content, Encoding.UTF8);
		}

		/// <summary>
		/// Creates a new memory-mapped buffer and initialises it with the contents of a file.
		/// </summary>
		/// <param name="path">The path to the file of which the content should be stored in the buffer.</param>
		/// <returns>The buffer that was created.</returns>
		public static MemoryGpgBuffer CreateFromFile(string path)
		{
			var bytes = File.ReadAllBytes(path);
			return Create(bytes);
		}
	}
}
