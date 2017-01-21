using System;

namespace Gpg.NET
{
	/// <summary>
	/// Wraps a GpgME error code.
	/// </summary>
	public class GpgMeException : GpgNetException
	{
		/// <summary>
		/// The name of the GpgME error returned.
		/// </summary>
		public GpgMeError GpgMeError { get; }
		/// <summary>
		/// The GpgME error as an integer.
		/// </summary>
		public int GpgErrorCode => (int)GpgMeError;

		/// <summary>
		/// Creates a new GpgMeException.
		/// </summary>
		/// <param name="error">The GpgMeError that was returned.</param>
		/// <param name="message">The error message associated with the error code.</param>
		public GpgMeException(GpgMeError error, string message) : base(message)
		{
			GpgMeError = error;
		}
	}
}