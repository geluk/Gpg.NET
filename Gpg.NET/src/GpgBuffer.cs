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

		/// <summary>
		/// Gets a value indicating whether this stream supports reading.
		/// </summary>
		public override bool CanRead => true;
		/// <summary>
		/// Gets a value indicating whether this stream supports seeking.
		/// </summary>
		public override bool CanSeek => true;
		/// <summary>
		/// Gets a value indicating whether this stream supports writing.
		/// </summary>
		public override bool CanWrite => true;

		/// <summary>
		/// Gets the length in bytes of the stream.
		/// </summary>
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

		/// <summary>
		/// Sets the position of the current stream.
		/// </summary>
		/// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
		/// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
		/// <returns></returns>
		public override long Seek(long offset, SeekOrigin origin)
		{
			// The values for SeekOrigin (WinAPI) and SeekPosition (GCC) are the same,
			// so we can just cast this. Feels dirty though...
			return GpgMeWrapper.gpgme_data_seek(Handle, (int) offset, (SeekPosition)origin);
		}

		/// <summary>
		/// Sets the length of the current stream.
		/// </summary>
		/// <param name="value">The desired length of the current stream in bytes.</param>
		public override void SetLength(long value)
		{
			// TODO: seek to value-1, then return to original position
			throw new NotImplementedException();
		}

		/// <summary>
		/// Initialises a new instance of the <see cref="GpgBuffer"/> class.
		/// </summary>
		/// <param name="handle"></param>
		protected internal GpgBuffer(IntPtr handle)
		{
			Handle = handle;
		}

		/// <summary>
		/// Reads a sequence of bytes from the current stream and advances the position
		/// within the stream by the number of bytes read.
		/// </summary>
		public override int Read(byte[] buffer, int offset, int count)
		{
			return GpgMeHelper.Read(Handle, buffer, offset, count);
		}

		/// <summary>
		/// Writes a sequence of bytes from the current stream and advances the position
		/// within the stream by the number of bytes written.
		/// </summary>
		public override void Write(byte[] buffer, int offset, int count)
		{
			GpgMeHelper.Write(Handle, buffer, offset, count);
		}

		/// <summary>
		/// This method has no effect.
		/// </summary>
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
