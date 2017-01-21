namespace Gpg.NET
{
	public enum GpgMeProtocol
	{
		OpenPgp,
		Cms,
		GpgConf,
		Assuan,
		G13,
		UiServer,
		Spawn,
		Default = 254,
		Unknown = 255
	}
}
