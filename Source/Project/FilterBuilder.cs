using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;

namespace RegionOrebroLan.DirectoryServices
{
	public class FilterBuilder(params string?[] filters) : IFilterBuilder
	{
		#region Fields

		private static readonly ConcurrentDictionary<FilterOperator, string> _operatorValueCache = new();

		#endregion

		#region Properties

		public virtual IList<string?> Filters { get; } = new List<string?>(filters);
		public virtual FilterOperator Operator { get; set; } = FilterOperator.And;
		protected internal virtual ConcurrentDictionary<FilterOperator, string> OperatorValueCache => _operatorValueCache;

		#endregion

		#region Methods

		public virtual string? Build()
		{
			// ReSharper disable All

			var resolvedFilters = new List<string>();

			foreach(var filter in this.Filters.Where(filter => !string.IsNullOrWhiteSpace(filter)))
			{
				var resolvedFilter = filter!.StartsWith("(", StringComparison.OrdinalIgnoreCase) ? filter : $"({filter})";

				resolvedFilters.Add(resolvedFilter);
			}

			if(!resolvedFilters.Any())
				return null;

			return $"({this.GetOperatorValue(this.Operator)}{string.Join(string.Empty, resolvedFilters)})";

			// ReSharper restore All
		}

		protected internal virtual string GetOperatorValue(FilterOperator filterOperator)
		{
			return this.OperatorValueCache.GetOrAdd(filterOperator, key =>
			{
				var filterOperatorValue = key.ToString();
				var type = typeof(FilterOperator);

				var descriptionAttribute = type.GetMember(filterOperatorValue).FirstOrDefault()?.GetCustomAttribute<DescriptionAttribute>(false);

				return descriptionAttribute != null ? descriptionAttribute.Description : filterOperatorValue;
			});
		}

		#endregion
	}
}