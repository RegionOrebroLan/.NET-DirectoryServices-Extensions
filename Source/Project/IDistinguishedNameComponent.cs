using System;

namespace RegionOrebroLan.DirectoryServices
{
	public interface IDistinguishedNameComponent : IEquatable<IDistinguishedNameComponent>
	{
		#region Properties

		string Name { get; }
		string Value { get; }

		#endregion

		#region Methods

		string ToString(DistinguishedNameCase nameCase);

		#endregion
	}
}