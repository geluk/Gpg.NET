using System;
using System.Runtime.InteropServices;
using Gpg.NET.Interop.GCC;

namespace Gpg.NET.Interop
{
	internal class GpgMeWrapper
	{
		private const string GpgMeDll = @"libgpgme-11.dll";
		//private const string GpgMeDll = @"C:\Program Files (x86)\GnuPG\bin\libgpgme-11.dll";
		// SETUP

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AnsiCharPtrMarshaler))]
		public static extern string gpgme_check_version(string required_version);

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern GpgMeError gpgme_engine_check_version(GpgMeProtocol protocol);

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern int gpgme_set_global_flag(string name, string value);

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AnsiCharPtrMarshaler))]
		public static extern string gpgme_get_dirinfo(string what);

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern GpgMeError gpgme_get_engine_info(out GpgMeEngineInfo info);

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern void gpgme_set_locale(IntPtr ctx, Locale category, string value);

		// DATA BUFFERS

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern GpgMeError gpgme_data_new(out IntPtr dh);

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern GpgMeError gpgme_data_new_from_fd(out IntPtr dh, IntPtr fd);

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern void gpgme_data_release(IntPtr dh);

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern int gpgme_data_read(IntPtr dh, IntPtr buffer, int length);

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern int gpgme_data_write(IntPtr dh, byte[] buffer, int length);

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern int gpgme_data_seek(IntPtr dh, int offset, SeekPosition whence);

		// CONTEXTS
		//		create/delete
		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern GpgMeError gpgme_new(out IntPtr ctx);

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern void gpgme_release(IntPtr ctx);
		//		encrypt/decrypt
		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern GpgMeError gpgme_op_decrypt(IntPtr ctx, IntPtr cipher, IntPtr plain);

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern GpgMeError gpgme_op_encrypt(IntPtr ctx, IntPtr[] recp, EncryptFlags flags, IntPtr plain, IntPtr cipher);
		//		engine info
		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr gpgme_ctx_get_engine_info(IntPtr ctx);

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern void gpgme_ctx_set_engine_info(IntPtr ctx, GpgMeProtocol proto, string file_name, string home_dir);
		//		keylist mode
		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern GpgKeylistMode gpgme_get_keylist_mode(IntPtr ctx);

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern GpgMeError gpgme_set_keylist_mode(IntPtr ctx, GpgKeylistMode mode);
		//		ASCII armor mode
		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern void gpgme_set_armor(IntPtr ctx, GpgArmorMode yes);

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern GpgArmorMode gpgme_get_armor(IntPtr ctx);

		// LISTING KEYS

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern GpgMeError gpgme_op_keylist_start(IntPtr ctx, string pattern, bool secret_only);

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern GpgMeError gpgme_op_keylist_next(IntPtr ctx, out IntPtr key);

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern GpgMeError gpgme_op_keylist_end(IntPtr ctx);

		// PROTOCOLS

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern GpgMeError gpgme_set_protocol(IntPtr ctx, GpgMeProtocol protocol);

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		public static extern GpgMeProtocol gpgme_get_protocol(IntPtr ctx);

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AnsiCharPtrMarshaler))]
		public static extern string gpgme_get_protocol_name(GpgMeProtocol protocol);

		// ERROR HANDLING

		[DllImport(GpgMeDll, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AnsiCharPtrMarshaler))]
		public static extern string gpgme_strerror(GpgMeError err);
	}
}
