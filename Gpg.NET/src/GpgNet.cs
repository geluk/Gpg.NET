using System;
using System.Collections.Generic;
using System.IO;
using Gpg.NET;
using Gpg.NET.Interop;

namespace Gpg.NET
{
	public static class GpgNet
	{
		// TODO: Annotate these
		public static string Version { get; private set; }
		public static bool Initialised { get; private set; }
		public static IReadOnlyList<EngineInfo> AvailableEngines { get; private set; }

		public static string Homedir { get; private set; }
		public static string Sysconfdir { get; private set; }
		public static string Bindir { get; private set; }
		public static string Libdir { get; private set; }
		public static string Libexecdir { get; private set; }
		public static string Datadir { get; private set; }
		public static string Localedir { get; private set; }
		public static string AgentSocket { get; private set; }

		/// <summary>
		/// Initialises the underlying GpgME library.
		/// </summary>
		/// <param name="installDir">When set, overrides the default GpgME installation directory.
		/// This should point to the bin directory of the GPG installation directory.</param>
		/// <param name="minLibraryVersion">
		/// The minimum required version of GpgME. Set to null to disable this version check.
		/// </param>
		/// <param name="minGpgVersion">, 
		/// The minimum required version of Gpg. Set to null to disable this version check.
		/// </param>
		public static void Initialise(string installDir = null, string minLibraryVersion = "1.8.0", string minGpgVersion = "2.0.0")
		{
			if (Initialised)
			{
				throw new InvalidOperationException("GpgME has already been initialised.");
			}

			// Global flags should be set before initialisation
			if (minGpgVersion != null)
			{
				if (GpgMeWrapper.gpgme_set_global_flag("require-gnupg", minGpgVersion) != 0)
				{
					throw new GpgNetException("Failed to set minimum required GnuPG version.");
				}
			}
			if (installDir != null)
			{
				var result = GpgMeWrapper.gpgme_set_global_flag("w32-inst-dir", installDir);
				if (result != 0)
				{
					throw new GpgNetException("Failed to set Win32 install dir.");
				}
			}
			// The version check is required as it initialises GpgME
			Version = GpgMeWrapper.gpgme_check_version(minLibraryVersion);
			if (Version == null)
			{
				throw new GpgNetException("Minimum required GpgME version is not met.");
			}
			
			// Get information about the GPG engines available
			AvailableEngines = GpgMeHelper.GetEngines();
			// Get information about the current GPG configuration
			Homedir = GpgMeWrapper.gpgme_get_dirinfo("homedir");
			Sysconfdir = GpgMeWrapper.gpgme_get_dirinfo("sysconfdir");
			Bindir = GpgMeWrapper.gpgme_get_dirinfo("bindir");
			Libdir = GpgMeWrapper.gpgme_get_dirinfo("libdir");
			Libexecdir = GpgMeWrapper.gpgme_get_dirinfo("libexecdir");
			Datadir = GpgMeWrapper.gpgme_get_dirinfo("datadir");
			Localedir = GpgMeWrapper.gpgme_get_dirinfo("localedir");
			AgentSocket = GpgMeWrapper.gpgme_get_dirinfo("agent-socket");
			Initialised = true;
		}

		/// <summary>
		/// Ensures the given protocol is available. If it isn't, an exception is thrown.
		/// </summary>
		/// <param name="protocol">The <see cref="GpgMeProtocol"/> to check for.</param>
		/// <exception cref="GpgMeException">Thrown if the given protocol is not available, badly configured, or otherwise unusable.</exception>
		public static void EnsureProtocol(GpgMeProtocol protocol)
		{
			ErrorHandler.Check(GpgMeWrapper.gpgme_engine_check_version(protocol));
		}

		/// <summary>
		/// Enable GpgME debugging. This should be done before GpgME is initialised.
		/// </summary>
		/// <param name="debugPath">
		/// The path to the file debug output should be saved to. If this is set to its default of null,
		/// debug output is saved to a file named <code>gpgme-debug.txt</code> in the current working directory.
		/// </param>
		/// <param name="debugLevel">The verbosity level of GpgME. A higher number increases verbosity.</param>
		public static void EnableDebugging(string debugPath = null, int debugLevel = 9)
		{
			if (Initialised)
			{
				throw new InvalidOperationException("Debugging needs to be enabled before GpgME is initialised.");
			}
			if (debugPath == null)
			{
				debugPath = Path.Combine(Environment.CurrentDirectory, "gpgme-debug.txt");
			}
			if (GpgMeWrapper.gpgme_set_global_flag("debug", $"{debugLevel};{debugPath}") != 0)
			{
				throw new GpgNetException("Failed to enable debugging.");
			}
		}
	}
}
