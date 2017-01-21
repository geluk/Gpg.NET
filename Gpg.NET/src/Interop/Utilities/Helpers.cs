using System;

namespace Gpg.NET.Utilities
{
	internal static class Helpers
	{
		/// <summary>
		/// Turns a Unix timestamp into a DateTime object.
		/// </summary>
		public static DateTime DateTimeFromEpochTime(long epochTime) => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(epochTime);
	}
}
