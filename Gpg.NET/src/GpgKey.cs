using System;
using System.Globalization;

namespace Gpg.NET
{
	/// <summary>
	/// Represents a GPG key, which can be used for encryption and decryption of data.
	/// </summary>
	public class GpgKey
	{
		internal IntPtr Handle { get; }
		/// <summary>
		/// Gets the key listing mode that was active when this key was retrieved.
		/// </summary>
		public GpgMeKeylistMode KeylistMode { get; internal set; }
		/// <summary>
		/// Gets a value indicating whether the key has been revoked by its owner.
		/// </summary>
		public bool Revoked { get; internal set; }
		/// <summary>
		/// Gets a value indicating whether the key has expired.
		/// </summary>
		public bool Expired { get; internal set; }
		/// <summary>
		/// Gets a value indicating whether the key has been disabled.
		/// </summary>
		public bool Disabled { get; internal set; }
		/// <summary>
		/// Gets a value indicating whether the key is invalid.
		/// This might have several reasons, for a example for the S/MIME backend,
		/// it will be set during key listings if the key could not be validated due to missing certificates or unmatched policies. 
		/// </summary>
		public bool Invalid { get; internal set; }
		/// <summary>
		/// Gets a value indicating whether the key can be used as a recipient for encryption.
		/// </summary>
		public bool CanEncrypt { get; internal set; }
		/// <summary>
		/// Gets a value indicating whether the key can be used for signing data.
		/// </summary>
		public bool CanSign { get; internal set; }
		/// <summary>
		/// Gets a value indicating whether the key can be used for certifying other keys and manipulating subkeys.
		/// </summary>
		public bool CanCertify { get; internal set; }
		/// <summary>
		/// Gets a value indicating whether the key can be used for authentication.
		/// </summary>
		public bool CanAuthenticate { get; internal set; }
		/// <summary>
		/// Gets a value indicating whether the key can be used for qualified signatures according to local government regulations.
		/// </summary>
		public bool IsQualified { get; internal set; }
		/// <summary>
		/// Gets a value indicating whether the key is a secret key.
		/// </summary>
		public bool Secret { get; internal set; }
		/// <summary>
		///  Gets the protocol supported by the current <see cref="GpgKey"/>.
		/// </summary>
		public GpgMeProtocol Protocol { get; internal set; }
		/// <summary>
		/// Gets the issuer serial for the current <see cref="GpgKey"/>.
		/// </summary>
		public string IssuerSerial { get; internal set; }
		/// <summary>
		/// Gets the issuer name for the current <see cref="GpgKey"/>.
		/// </summary>
		public string IssuerName { get; internal set; }
		/// <summary>
		/// If <see cref="Protocol"/> is Protocol.Cms, gets the chain ID; which can be used to build the certificate chain.
		/// </summary>
		public string ChainId { get; internal set; }
		/// <summary>
		/// If <see cref="Protocol"/> is Protocol.OpenPgp, gets the owner trust for this key.
		/// </summary>
		public GpgMeValidity OwnerTrust { get; internal set; }
		/// <summary>
		/// Gets the subkeys belonging to the current <see cref="GpgKey"/>.
		/// </summary>
		public GpgSubkey[] Subkeys { get; internal set; }
		/// <summary>
		/// Gets the User Ids considered to be the owners of the current <see cref="GpgKey"/>.
		/// </summary>
		public GpgUserId[] Uids { get; internal set; }
		/// <summary>
		/// Gets the fingerprint of the primary key. This is a copy of the fingerprint of the first <see cref="GpgSubkey"/>.
		/// For an incomplete key (for example from a verification result) a subkey may be missing but this field may be set nevertheless.
		/// </summary>
		public string Fingerprint { get; internal set; }

		internal GpgKey(IntPtr handle)
		{
			var c = CultureInfo.CurrentCulture.IsNeutralCulture;
			Handle = handle;
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		public override string ToString() =>
			$"{Fingerprint}" +
			$"{(Revoked ? " !REV" : "")}" +
			$"{(Expired ? " !EXP" : "")}" +
			$" [{(CanEncrypt ? "E" : "")}" +
			$"{(CanSign ? "S" : "")}" +
			$"{(CanCertify ? "C" : "")}" +
			$"{(CanAuthenticate ? "A" : "")}]" +
			$" [{Protocol}] [{OwnerTrust}]";
	}
}
