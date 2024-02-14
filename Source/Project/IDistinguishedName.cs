namespace RegionOrebroLan.DirectoryServices
{
	public interface IDistinguishedName : IEquatable<IDistinguishedName>
	{
		#region Properties

		IList<IDistinguishedNameComponent> Components { get; }
		IDistinguishedName Parent { get; }

		#endregion
	}
}