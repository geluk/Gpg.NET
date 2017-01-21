using System;
using System.IO;
using Gpg.NET.Interop;

namespace Gpg.NET
{
	public class FileGpgBuffer : GpgBuffer
	{
		private FileStream BackingFile { get; }

		private FileGpgBuffer(IntPtr handle, FileStream backingFile) : base(handle)
		{
			BackingFile = backingFile;
		}

		/// <summary>
		/// Creates a new file-based buffer. Not yet implemented.
		/// </summary>
		/// <param name="path">The path to the file from which the buffer should be created.
		/// If no file exists at the given path, a file is created.</param>
		/// <returns></returns>
		public static GpgBuffer CreateFileBuffer(string path)
		{
			// The current implementation doesn't work yet
			throw new NotImplementedException();

			var stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			var fileDescriptor = stream.SafeFileHandle.DangerousGetHandle();
			IntPtr handle;
			ErrorHandler.Check(GpgMeWrapper.gpgme_data_new_from_fd(out handle, fileDescriptor));
			return new FileGpgBuffer(handle, stream);
		}
	}
}
