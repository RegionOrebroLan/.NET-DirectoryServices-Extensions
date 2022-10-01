using System.ComponentModel;

namespace RegionOrebroLan.DirectoryServices
{
	public enum FilterOperator
	{
		[Description("&")] And,
		[Description("!")] Not,
		[Description("|")] Or
	}
}