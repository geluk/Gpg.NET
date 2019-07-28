using System;

namespace Gpg.NET
{
	/// <summary>
	/// Specifies the behaviour of a key listing operation.
	/// </summary>
	[Flags]
	public enum GpgArmorMode
	{
		/// <summary>
		/// Unknown or no options set.
		/// </summary>
		Off = 0,
		/// <summary>
		/// Search the local keyring for keys.
		/// </summary>
		On = 1
	}
}