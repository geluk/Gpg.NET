using System;
using System.Runtime.InteropServices;
using Gpg.NET.Utilities;

namespace Gpg.NET.Interop
{
	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi, Size = 72)]
	internal struct GpgMeSubkey
	{
		[FieldOffset(0)]
		public IntPtr Next;
		[FieldOffset(4)]
		// Several flags are packed into a single 32-bit integer,
		// so we explicitly define its size here.
		public UInt32 Flags;
		[FieldOffset(8)]
		public PublicKeyAlgorithm PublicKeyAlgorithm;
		[FieldOffset(12)]
		public int Length;
		[FieldOffset(16)]
		public string KeyId;

		// A hidden field is located at offset 20, taking up 17 bytes.
		// This puts the next possible offset at 37, but since it needs
		// to be a multiple of 4, we skip 3 bytes and continue at 40.
		[FieldOffset(40)]
		public string Fingerprint;
		[FieldOffset(44)]
		// GpgME defines these as signed 32-bit integers, however they
		// are really unsigned.
		public uint Timestamp;
		[FieldOffset(48)]
		public uint Expires;
		[FieldOffset(52)]
		public string CardNumber;
		[FieldOffset(56)]
		public string Curve;
		[FieldOffset(60)]
		public string Keygrip;

		public GpgSubkey ToGpgSubkey(IntPtr handle)
		{
			return new GpgSubkey(handle)
			{
				// Read the boolean flags from the Flags field.
				// This may not work on big-endian systems.
				Revoked = ((Flags >> 0) & 1) == 1,
				Expired = ((Flags >> 1) & 1) == 1,
				Disabled = ((Flags >> 2) & 1) == 1,
				Invalid = ((Flags >> 3) & 1) == 1,
				CanEncrypt = ((Flags >> 4) & 1) == 1,
				CanSign = ((Flags >> 5) & 1) == 1,
				CanCertify = ((Flags >> 6) & 1) == 1,
				Secret = ((Flags >> 7) & 1) == 1,
				CanAuthenticate = ((Flags >> 8) & 1) == 1,
				IsQualified = ((Flags >> 9) & 1) == 1,
				IsCardkey = ((Flags >> 10) & 1) == 1,
				PublicKeyAlgorithm = PublicKeyAlgorithm,
				Length = Length,
				KeyId = KeyId,
				Fingerprint = Fingerprint,
				Created = Helpers.DateTimeFromEpochTime(Timestamp),
				Expires = Helpers.DateTimeFromEpochTime(Expires),
				CardNumber = CardNumber,
				Curve = Curve,
				Keygrip = Keygrip
			};
		}
	}
}
