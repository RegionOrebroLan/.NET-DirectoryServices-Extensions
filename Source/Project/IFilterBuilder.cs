namespace RegionOrebroLan.DirectoryServices
{
	public interface IFilterBuilder
	{
		#region Properties

		IList<string?> Filters { get; }
		FilterOperator Operator { get; set; }

		#endregion

		#region Methods

		string? Build();

		#endregion
	}
}