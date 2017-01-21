using System;
using System.Runtime.InteropServices;

namespace Gpg.NET.Interop
{
	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
	struct GpgMeSigNotation
	{
		[FieldOffset(0)]
		public IntPtr Next;
		[FieldOffset(4)]
		public string Name;
		[FieldOffset(8)]
		public string Value;

		// two length fields are located at offset 12 and 16,
		// which can be ignored since length is an intrinsic
		// property of C# strings.
		[FieldOffset(20)]
		public GpgMeSigNotationFlags NotationFlags;
		[FieldOffset(40)]
		// Several flags are packed into a single 32-bit integer,
		// so we explicitly define its size here.
		public UInt32 Flags;
	}
}