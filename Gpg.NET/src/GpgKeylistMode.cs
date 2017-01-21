using System;

namespace Gpg.NET
{
	/// <summary>
	/// Specifies the behaviour of a key listing operation.
	/// </summary>
	[Flags]
	public enum GpgKeylistMode
	{
		/// <summary>
		/// Unknown or no options set.
		/// </summary>
		Unknown = 0,
		/// <summary>
		/// Search the local keyring for keys.
		/// </summary>
		Local = 1,
		/// <summary>
		/// Search an external source for keys.
		/// </summary>
		Extern = 2,
		/// <summary>
		/// Include signatures.
		/// </summary>
		Sigs = 4,
		/// <summary>
		/// Include signature notations.
		/// </summary>
		SigNotations = 8,
		/// <summary>
		/// Include information about the presence of a corresponding secret key in a public key listing.
		/// A public key listing with this mode is slower than a standard listing but can be used instead of a second run to list the secret keys.
		/// </summary>
		WithSecret = 16,
		/// <summary>
		/// Include information pertaining to the TOFU trust model.
		/// </summary>
		WithTofu = 32,
		/// <summary>
		/// Include keys flagged as ephemeral.
		/// </summary>
		Ephemeral = 128,
		/// <summary>
		/// Validate keys/certificates instead of getting the validity information from an internal cache. 
		/// This might be an expensive operation and is in general not useful. 
		/// Currently only implemented for the S/MIME backend and ignored for other backends. 
		/// </summary>
		Validate = 256
	}
}