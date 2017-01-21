using System;

namespace Gpg.NET
{
	/// <summary>
	/// Represents a GPG subkey, which belongs to a <see cref="GpgKey"/> and is the
	/// key that's actually responsible for cryptographic operations.
	/// </summary>
	public class GpgSubkey
	{
		internal  IntPtr Handle { get; }
		/// <summary>
		/// Gets a value indicating whether the subkey has been revoked by its owner.
		/// </summary>
		public bool Revoked { get; internal set; }
		/// <summary>
		/// Gets a value indicating whether the subkey has expired.
		/// </summary>
		public bool Expired { get; internal set; }
		/// <summary>
		/// Gets a value indicating whether the subkey has been disabled.
		/// </summary>
		public bool Disabled { get; internal set; }
		// TODO: figure this one out too
		public bool Invalid { get; internal set; }
		/// <summary>
		/// Gets a value indicating whether the subkey can be used as a recipient for encryption.
		/// </summary>
		public bool CanEncrypt { get; internal set; }
		/// <summary>
		/// Gets a value indicating whether the subkey can be used for signing data.
		/// </summary>
		public bool CanSign { get; internal set; }
		/// <summary>
		/// Gets a value indicating whether the subkey can be used for certifying other keys and manipulating subkeys.
		/// </summary>
		public bool CanCertify { get; internal set; }
		/// <summary>
		/// Gets a value indicating whether the subkey can be used for authentication.
		/// </summary>
		public bool CanAuthenticate { get; internal set; }
		/// <summary>
		/// Gets a value indicating whether the subkey is a secret key.
		/// </summary>
		public bool Secret { get; internal set; }
		/// <summary>
		/// Gets a value indicating whether the subkey can be used for qualified signatures according to local government regulations.
		/// </summary>
		public bool IsQualified { get; internal set; }
		/// <summary>
		/// Gets a value indicating whether the subkey is stored on a smart card.
		/// </summary>
		public bool IsCardkey { get; internal set; }
		/// <summary>
		/// Gets the algorithm used for this subkey.
		/// </summary>
		public PublicKeyAlgorithm PublicKeyAlgorithm { get; internal set; }
		/// <summary>
		/// Gets the length (in bits) of the subkey.
		/// </summary>
		public int Length { get; internal set; }
		/// <summary>
		/// Gets the ID of the subkey in hexadecimal digits.
		/// </summary>
		public string KeyId { get; internal set; }
		/// <summary>
		/// Gets the fingerprint of the subkey in hexadecimal digits, if available.
		/// </summary>
		public string Fingerprint { get; internal set; }
		/// <summary>
		/// Gets the date and time at which the subkey was created.
		/// </summary>
		public DateTime Created { get; internal set; }
		/// <summary>
		/// Gets the date and time at which the subkey expires.
		/// </summary>
		public DateTime Expires { get; internal set; }
		/// <summary>
		/// If the subkey is stored on a smart card, gets the serial number of the smart card holding this key.
		/// </summary>
		public string CardNumber { get; internal set; }
		/// <summary>
		/// If the subkey algorithm is an ECC Algorith, gets the name of the curve.
		/// </summary>
		public string Curve { get; internal set; }
		/// <summary>
		/// Gets the keygrip of the current subkey, if available.
		/// </summary>
		public string Keygrip { get; internal set; }

		internal GpgSubkey(IntPtr handle)
		{
			Handle = handle;
		}

		public override string ToString() =>
			$"{KeyId}" +
			$"{(Revoked ? " !REV" : "")}" +
			$"{(Expired ? " !EXP" : "")}" +
			$"{(Disabled ? " !DIS" : "")}" +
			$" ({PublicKeyAlgorithm}{(Curve == null ? "" : $"/{Curve}" )}-{Length})" +
			$" [{(CanEncrypt ? "E" : "")}" +
			$"{(CanSign ? "S" : "")}" +
			$"{(CanCertify ? "C" : "")}" +
			$"{(CanAuthenticate ? "A" : "")}]" +
			$" [Exp: {Expires:d}]";
	}
}
