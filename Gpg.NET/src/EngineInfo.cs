namespace Gpg.NET
{
	public class EngineInfo
	{
		public GpgMeProtocol Protocol { get; }
		public string Filename { get; }
		public string HomeDir { get; }
		public string Version { get; }
		public string ReqVersion { get; }

		internal EngineInfo(GpgMeProtocol protocol, string filename, string homeDir, string version, string reqVersion)
		{
			Protocol = protocol;
			Filename = filename;
			HomeDir = homeDir;
			Version = version;
			ReqVersion = reqVersion;
		}

		public override string ToString()
		{
			return $"{Protocol} ver: \"{Version}\" req: \"{ReqVersion}\" home: \"{HomeDir}\" filename: \"{Filename}\"";
		}
	}
}
