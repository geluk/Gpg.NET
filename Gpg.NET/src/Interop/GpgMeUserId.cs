using System;
using System.Runtime.InteropServices;

namespace Gpg.NET.Interop
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	internal struct GpgMeUserId
	{
		public IntPtr Next;
		public UInt32 Flags;
		public GpgValidity Validity;
		public string Uid;
		public string Name;
		public string Email;
		public string Comment;
		public IntPtr Signatures;
		// TODO: ignore GPG-internal variable
		public IntPtr LastSignature;
		public string Address;
		public IntPtr TofuInfo;

		public GpgUserId ToGpgUserId()
		{
			return new GpgUserId
			{
				Validity = Validity,
				Uid = Uid,
				Name = Name,
				Email = Email,
				Comment = Comment,
				Address = Address
			};
		}
	}
}
