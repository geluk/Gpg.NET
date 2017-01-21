using Gpg.NET.Interop;

namespace Gpg.NET
{
	internal class ErrorHandler
	{
		internal static void Check(GpgMeError error)
		{
			if (error != GpgMeError.GPG_ERR_NO_ERROR)
			{
				throw new GpgMeException(error, GpgMeWrapper.gpgme_strerror(error));
			}
		}
	}
}
