using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RegionOrebroLan.DirectoryServices
{
	public class DistinguishedNameParser : BasicParser<IDistinguishedName>
	{
		#region Constructors

		public DistinguishedNameParser(IDistinguishedNameComponentValidator distinguishedNameComponentValidator)
		{
			this.DistinguishedNameComponentValidator = distinguishedNameComponentValidator ?? throw new ArgumentNullException(nameof(distinguishedNameComponentValidator));
		}

		#endregion

		#region Properties

		protected internal virtual IDistinguishedNameComponentValidator DistinguishedNameComponentValidator { get; }
		public virtual DistinguishedNameCase NameCase { get; set; } = DistinguishedNameCase.None;

		#endregion

		#region Methods

		public override IDistinguishedName Parse(string value)
		{
			if(value == null)
				throw new ArgumentNullException(nameof(value));

			if(value.Length == 0)
				throw new ArgumentException("The value can not be empty.", nameof(value));

			var distinguishedName = new DistinguishedName { NameCase = this.NameCase };

			try
			{
				foreach(var component in this.Split(value, DistinguishedName.DefaultComponentDelimiter))
				{
					var componentParts = this.Split(component, DistinguishedNameComponent.DefaultNameValueDelimiter).ToArray(); // Maybe we should use: this.Split(component, DistinguishedNameComponent.DefaultNameValueDelimiter, 2).ToArray();

					if(componentParts.Length != 2)
						throw new FormatException($"Each component in the distinguished name must consist of a name and a value separated by \"{DistinguishedNameComponent.DefaultNameValueDelimiter}\".");

					distinguishedName.Components.Add(new DistinguishedNameComponent(componentParts[0].Trim(), componentParts[1], this.DistinguishedNameComponentValidator) { NameCase = this.NameCase });
				}
			}
			catch(Exception exception)
			{
				throw new FormatException($"The distinguished name \"{value}\" is invalid.", exception);
			}

			return distinguishedName;
		}

		protected internal virtual IEnumerable<string> Split(string value, char separator)
		{
			return this.Split(value, separator, int.MaxValue);
		}

		protected internal virtual IEnumerable<string> Split(string value, char separator, int count)
		{
			if(value == null)
				throw new ArgumentNullException(nameof(value));

			if(count < 0)
				throw new ArgumentOutOfRangeException(nameof(count), "The count can not be less than zero.");

			var temporaryValueParts = value.Split([separator]);

			var valueParts = new List<string>();

			for(var i = 0; i < temporaryValueParts.Length; i++)
			{
				if(valueParts.Count == count - 1)
				{
					valueParts.Add(string.Join(separator.ToString(CultureInfo.InvariantCulture), temporaryValueParts.Skip(i).ToArray()));
					break;
				}

				var valuePart = temporaryValueParts[i];

				while(valuePart.EndsWith(@"\", StringComparison.OrdinalIgnoreCase) && i < temporaryValueParts.Length - 1)
				{
					valuePart += separator + temporaryValueParts[i + 1];
					i++;
				}

				valueParts.Add(valuePart);
			}

			return valueParts.ToArray();
		}

		public override bool TryParse(string value, out IDistinguishedName distinguishedName)
		{
			distinguishedName = null;

			return !string.IsNullOrEmpty(value) && base.TryParse(value, out distinguishedName);
		}

		#endregion
	}
}