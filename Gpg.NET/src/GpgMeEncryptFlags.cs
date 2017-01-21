namespace Gpg.NET
{
	/// <summary>
	/// A set of flags representing encryption settings used by GPG.
	/// </summary>
	public enum GpgMeEncryptFlags
	{
		/// <summary>
		/// Trust all recipients, regardless of their validity.
		/// </summary>
		AlwaysTrust = 1,
		/// <summary>
		/// Do not encrypt to the default or hidden default recipients in addition to the recipients specified.
		/// </summary>
		NoEncryptTo = 2,
		/// <summary>
		/// Do not compress the plaintext before encrypting it.
		/// </summary>
		NoCompress = 4,
		/// <summary>
		/// Used with the UI server protocol to prepare an encryption.
		/// </summary>
		Prepare = 8,
		/// <summary>
		/// Used with the UI server protocol tell the server to also expect a sign commmand.
		/// </summary>
		ExpectSign = 16,
		/// <summary>
		/// Also encrypt the output symmetrically, even if recipients are provided.
		/// </summary>
		EncryptSymmetric = 32
	}
}
