using System;

namespace Gpg.NET
{
	/// <summary>
	/// Represents a generic error in Gpg.NET.
	/// </summary>
	public class GpgNetException : Exception
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="GpgNetException"/> class.
		/// </summary>
		public GpgNetException(string message) : base(message) {}
	}
}