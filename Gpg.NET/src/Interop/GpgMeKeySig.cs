using System;
using System.Runtime.InteropServices;

namespace Gpg.NET.Interop
{
	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi, Size = 76)]
	struct GpgMeKeySig
	{
		[FieldOffset(0)]
		public IntPtr Next;
		[FieldOffset(4)]
		public UInt32 Flags;
		[FieldOffset(8)]
		public PublicKeyAlgorithm PublicKeyAlgorithm;
		[FieldOffset(12)]
		public string KeyId;

		// A hidden field is located at offset 16, taking up 17 bytes.
		// This puts the next possible offset at 33, but since it needs
		// to be a multiple of 4, we skip 3 bytes and continue at 36.
		[FieldOffset(36)]
		public int Timestamp;
		[FieldOffset(40)]
		public int Expires;
		[FieldOffset(44)]
		public GpgMeError Status;

		// Another hidden field is located at offset 48
		[FieldOffset(52)]
		public string Uid;
		[FieldOffset(56)]
		public string Name;
		[FieldOffset(60)]
		public string Comment;
		[FieldOffset(64)]
		public uint SigClass;
		[FieldOffset(68)]
		public GpgMeSigNotation Notations;
		// Another hidden field is located at offset 72
	}
}
