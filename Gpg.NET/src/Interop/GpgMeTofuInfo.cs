using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Gpg.NET.Interop
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	struct GpgMeTofuInfo
	{
		public IntPtr Next;
		public UInt32 Flags;
		public ushort Signatures;
		public ushort Encryptions;
		public uint FirstSignature;
		public uint LastSignature;
		public uint FirstEncryption;
		public uint LastEncryption;
	}
}
