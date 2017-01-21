namespace Gpg.NET
{
	/// <summary>
	/// Contains information about a GPG Engine.
	/// </summary>
	public class EngineInfo
	{
		/// <summary>
		/// Gets the protocol for which the engine is used.
		/// </summary>
		public GpgMeProtocol Protocol { get; }
		/// <summary>
		/// Gets the file name of the executable of the crypto engine.
		/// </summary>
		public string Filename { get; }
		/// <summary>
		/// Gets the directory name of the engine's configuration directory. If it is null, the default directory is used.
		/// </summary>
		public string HomeDir { get; }
		/// <summary>
		/// Gets the version number of the crypto engine.
		/// </summary>
		public string Version { get; }
		/// <summary>
		/// Gets the minimum required version number of the engine for GpgME to work correctly.
		/// </summary>
		public string ReqVersion { get; }

		internal EngineInfo(GpgMeProtocol protocol, string filename, string homeDir, string version, string reqVersion)
		{
			Protocol = protocol;
			Filename = filename;
			HomeDir = homeDir;
			Version = version;
			ReqVersion = reqVersion;
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		public override string ToString()
		{
			return $"{Protocol} ver: \"{Version}\" req: \"{ReqVersion}\" home: \"{HomeDir}\" filename: \"{Filename}\"";
		}
	}
}
