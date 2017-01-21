using System;
using System.Runtime.InteropServices;

namespace Gpg.NET.Interop
{
	internal class AnsiCharPtrMarshaler : ICustomMarshaler
	{
		private static readonly AnsiCharPtrMarshaler Instance = new AnsiCharPtrMarshaler();

		public static ICustomMarshaler GetInstance(string cookie)
		{
			return Instance;
		}

		public object MarshalNativeToManaged(IntPtr pNativeData)
		{
			return Marshal.PtrToStringAnsi(pNativeData);
		}

		public IntPtr MarshalManagedToNative(object managedObj)
		{
			return IntPtr.Zero;
		}

		public void CleanUpNativeData(IntPtr pNativeData)
		{
			// It should be up to Gpg to free native strings.
			// Emphasis on should - I'm not actually sure.
			// Uncomment this if it turns out to be necessary after all.
			//Marshal.FreeHGlobal(pNativeData);
		}

		public void CleanUpManagedData(object managedObj)
		{
			// The managed object is a string, so no further cleanup is necessary.
		}

		public int GetNativeDataSize()
		{
			return IntPtr.Size;
		}
	}
}