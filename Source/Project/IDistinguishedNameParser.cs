namespace RegionOrebroLan.DirectoryServices
{
	public interface IDistinguishedNameParser
	{
		#region Methods

		IDistinguishedName Parse(string value);

		#endregion
	}
}