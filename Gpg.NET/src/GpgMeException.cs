using System;

namespace Gpg.NET
{
	public class GpgMeException : Exception
	{
		public GpgMeError GpgMeError { get; }
		public int GpgErrorCode => (int)GpgMeError;

		public GpgMeException(GpgMeError error, string message) : base(message)
		{
			GpgMeError = error;
		}
	}
}