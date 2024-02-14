namespace RegionOrebroLan.DirectoryServices
{
	public interface IDistinguishedNameComponentValidator
	{
		#region Methods

		IList<Exception> ValidateName(string name);
		IList<Exception> ValidateValue(string value);

		#endregion
	}
}