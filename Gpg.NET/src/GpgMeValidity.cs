namespace Gpg.NET
{
	/// <summary>
	/// Enumerates key validity or trust.
	/// </summary>
	public enum GpgMeValidity
	{
		Unknown,
		Undefined,
		Never,
		Marginal,
		Full,
		Ultimate
	}
}