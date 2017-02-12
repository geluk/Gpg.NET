using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Gpg.NET.Interop
{
	internal static class Kernel32
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr LoadLibrary(string libname);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern bool FreeLibrary(IntPtr hModule);

		private static IntPtr handle;

		public static void Load(string libraryName)
		{
			handle = LoadLibrary(libraryName);
			if (handle == IntPtr.Zero)
			{
				var error = Marshal.GetLastWin32Error();
				throw new Exception($"Failed to load library {libraryName}. Erro code: {error}");
			}
		}
	}
}
