namespace Gpg.NET
{
	/// <summary>
	/// Represents a GPG user ID, which is a component of a GPG key.
	/// One key can have many user IDs. The first one in the list is the main (or primary) user ID.
	/// </summary>
	public class GpgUserId
	{
		/// <summary>
		/// Gets the validity of the current User ID.
		/// </summary>
		public GpgMeValidity Validity { get; internal set; }
		/// <summary>
		/// Gets the User ID string.
		/// </summary>
		public string Uid { get; internal set; }
		/// <summary>
		/// Gets the name component of the current User ID, if available.
		/// </summary>
		public string Name { get; internal set; }
		/// <summary>
		/// Gets the email component of the current User ID, if available.
		/// </summary>
		public string Email { get; internal set; }
		/// <summary>
		/// Gets the comment component of the current User ID, if available.
		/// </summary>
		public string Comment { get; internal set; }
		/// <summary>
		/// Gets the email address (addr-spec from RFC-5322) of the user ID string.
		/// This is generally the same as <see cref="Email"/> but might be slightly different.
		/// </summary>
		public string Address { get; internal set; }

		public override string ToString()
		{
			return $"{Uid} ({Validity})";
		}
	}
}