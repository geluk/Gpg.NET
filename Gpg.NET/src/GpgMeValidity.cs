
namespace Gpg.NET
{
	/// <summary>
	/// Enumerates key validity or trust.
	/// </summary>
	public enum GpgMeValidity
	{
		/// <summary>
		/// Unknown validity.
		/// </summary>
		Unknown,
		/// <summary>
		/// Undefined validity.
		/// </summary>
		Undefined,
		/// <summary>
		/// User ID is never valid.
		/// </summary>
		Never,
		/// <summary>
		/// Marginal validity.
		/// </summary>
		Marginal,
		/// <summary>
		/// User ID is fully valid.
		/// </summary>
		Full,
		/// <summary>
		/// Ultimate validity.
		/// </summary>
		Ultimate
	}
}