namespace Gpg.NET
{
	/// <summary>
	/// Thrown when the lookup of a GPG key fails.
	/// </summary>
	public class GpgKeyNotFoundException : GpgNetException
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="GpgKeyNotFoundException"/> class.
		/// </summary>
		public GpgKeyNotFoundException(string message) :base(message){}
	}
}