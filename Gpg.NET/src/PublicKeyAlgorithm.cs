using System;

namespace Gpg.NET
{
	/// <summary>
	/// Enumerates public key algorithms supported by GpgME.
	/// </summary>
	public enum PublicKeyAlgorithm
	{
		/// <summary>
		/// An unknown algorithm.
		/// </summary>
		Unknown = 0,
		/// <summary>
		/// The RSA (Rivest, Shamir, Adleman) algorithm.
		/// </summary>
		RSA = 1,
		/// <summary>
		/// The RSA algorith, for encryption and decryption only.
		/// </summary>
		[Obsolete]
		RsaE = 2,
		/// <summary>
		/// The RSA algorithm, for signing and verification only.
		/// </summary>
		[Obsolete]
		RsaS = 3,
		/// <summary>
		/// Also indicates Elgamal and is used specifically in GnuPG.
		/// </summary>
		ElgamalE = 16,
		/// <summary>
		/// DSA (Digital Signature Algorithm).
		/// </summary>
		DSA = 17,
		/// <summary>
		/// Generic indicator for elliptic curve algorithms.
		/// </summary>
		ECC = 18,
		/// <summary>
		/// Elgamal.
		/// </summary>
		Elgamal = 20,
		/// <summary>
		/// Elliptic Curve Digital Signature Algorithm as defined by FIPS 186-2 and RFC-6637.
		/// </summary>
		ECDSA = 301,
		/// <summary>
		/// Elliptic Curve Diffie-Hellmann as defined by RFC-6637
		/// </summary>
		ECDH = 302,
		/// <summary>
		/// The Edwards DSA Algorithm.
		/// </summary>
		EdDSA = 303
	}
}