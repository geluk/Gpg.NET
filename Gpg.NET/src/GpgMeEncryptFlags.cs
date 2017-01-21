namespace Gpg.NET
{
	public enum GpgMeEncryptFlags
	{
		AlwaysTrust = 1,
		NoEncryptTo = 2,
		NoCompress = 4,
		Prepare = 8,
		ExpectSign = 16,
		EncryptSymmetric = 32
	}
}
