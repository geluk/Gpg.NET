using System;

namespace Gpg.NET
{
	public class GpgNetException : Exception
	{
		public GpgNetException(string message) : base(message) { }
	}
}