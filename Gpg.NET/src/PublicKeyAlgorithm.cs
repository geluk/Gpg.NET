namespace Gpg.NET
{
	public enum PublicKeyAlgorithm
	{
		Unknown = 0,
		RSA = 1,
		RsaE = 2,
		RsaS = 3,
		ElgamalE = 16,
		DSA = 17,
		ECC = 18,
		Elgamal = 20,
		ECDA = 301,
		ECDH = 302,
		EdDSA = 303
	}
}