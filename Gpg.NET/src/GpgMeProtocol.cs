namespace Gpg.NET
{
	/// <summary>
	/// Specifies the set of possible protocol values that are supported by GpgME.
	/// </summary>
	public enum GpgMeProtocol
	{
		/// <summary>
		/// The OpenPGP protocol.
		/// </summary>
		OpenPgp,
		/// <summary>
		/// Cryptographic Message Syntax.
		/// </summary>
		Cms,
		/// <summary>
		/// Under development. Please ask on gnupg-devel@gnupg.org for help.  
		/// </summary>
		GpgConf,
		/// <summary>
		/// The raw Assuan protocol.
		/// </summary>
		Assuan,
		/// <summary>
		/// Under development. Please ask on gnupg-devel@gnupg.org for help. 
		/// </summary>
		G13,
		/// <summary>
		/// Under development. Please ask on gnupg-devel@gnupg.org for help. 
		/// </summary>
		UiServer,
		/// <summary>
		/// Special protocol used for running processes.
		/// </summary>
		Spawn
	}
}
