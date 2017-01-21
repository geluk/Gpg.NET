using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Gpg.NET.Interop
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi/*, Size = 104*/)]
	internal struct GpgMeKey
	{
		public int refs;
		// Several flags are packed into a single 32-bit integer,
		// so we explicitly define its size here.
		public UInt32 Flags;
		public GpgMeProtocol Protocol;
		public string IssuerSerial;
		public string IssuerName;
		public string ChainId;
		public GpgMeValidity OwnerTrust;
		public IntPtr Subkeys;
		public IntPtr Uids;
		public IntPtr lastSubkey;
		public IntPtr lastUid;
		public GpgMeKeylistMode KeylistMode;
		public string Fingerprint;

		public GpgKey ToGpgKey(IntPtr handle)
		{
			return new GpgKey(handle)
			{
				KeylistMode = KeylistMode,
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
				Protocol = Protocol,
				IssuerSerial = IssuerSerial,
				IssuerName = IssuerName,
				ChainId = ChainId,
				OwnerTrust = OwnerTrust,
				Uids = GetUserIds().ToArray(),
				Subkeys = GetSubkeys().ToArray(),
				Fingerprint = Fingerprint
			};
		}

		public IEnumerable<GpgUserId> GetUserIds()
		{
			var currentPtr = Uids;
			while (currentPtr != IntPtr.Zero)
			{
				var currentStructure = Marshal.PtrToStructure<GpgMeUserId>(currentPtr);
				currentPtr = currentStructure.Next;
				yield return currentStructure.ToGpgUserId();
			}
		}

		public IEnumerable<GpgSubkey> GetSubkeys()
		{
			var currentPtr = Subkeys;
			while (currentPtr != IntPtr.Zero)
			{
				var currentStructure = Marshal.PtrToStructure<GpgMeSubkey>(currentPtr);
				currentPtr = currentStructure.Next;
				yield return currentStructure.ToGpgSubkey(currentPtr);
			}
		}
	}
}
