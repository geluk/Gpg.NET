namespace Gpg.NET
{
	public enum GpgMeKeylistMode
	{
		Unknown = 0,
		Local = 1,
		Extern = 2,
		Sigs = 4,
		SigNotations = 8,
		WithSecret = 16,
		WithTofu = 32,
		Ephemeral = 128,
		Validate = 256
	}
}