using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RegionOrebroLan.DirectoryServices
{
	public class DistinguishedName : IDistinguishedName
	{
		#region Fields

		public const char DefaultComponentDelimiter = ',';

		#endregion

		#region Properties

		protected internal virtual char ComponentDelimiter => DefaultComponentDelimiter;
		public virtual IList<IDistinguishedNameComponent> Components { get; } = [];
		public virtual DistinguishedNameCase NameCase { get; set; } = DistinguishedNameCase.None;

		public virtual IDistinguishedName Parent
		{
			get
			{
				if(this.Components.Count < 2)
					return null;

				var parent = new DistinguishedName();

				foreach(var component in this.Components.Skip(1))
				{
					parent.Components.Add(component);
				}

				return parent;
			}
		}

		#endregion

		#region Methods

		public override bool Equals(object obj)
		{
			return this.Equals(obj as IDistinguishedName);
		}

		public bool Equals(IDistinguishedName other)
		{
			if(other == null)
				return false;

			if(this.Components.Count != other.Components.Count)
				return false;

			return !this.Components.Where((component, i) => !component.Equals(other.Components[i])).Any();
		}

		public override int GetHashCode()
		{
			return this.ToString().ToUpperInvariant().GetHashCode();
		}

		public override string ToString()
		{
			return this.ToString(this.NameCase);
		}

		public virtual string ToString(DistinguishedNameCase nameCase)
		{
			return string.Join(this.ComponentDelimiter.ToString(CultureInfo.InvariantCulture), this.Components.Select(component => component.ToString(nameCase)).ToArray());
		}

		#endregion
	}
}