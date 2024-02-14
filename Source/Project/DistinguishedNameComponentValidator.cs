using System.Text.RegularExpressions;

namespace RegionOrebroLan.DirectoryServices
{
	public class DistinguishedNameComponentValidator : IDistinguishedNameComponentValidator
	{
		#region Fields

		private IEnumerable<char> _invalidValueCharacters;
		private static readonly IEnumerable<char> _specialInvalidValueCharacters = ['/'];
		private static readonly Regex _validNameRegularExpression = new(@"^[0-9a-zA-Z]+\z$", RegexOptions.Compiled);

		#endregion

		#region Properties

		protected internal virtual char ComponentDelimiter => DistinguishedName.DefaultComponentDelimiter;
		protected internal virtual IEnumerable<char> InvalidValueCharacters => this._invalidValueCharacters ??= new[] { this.ComponentDelimiter, this.NameValueDelimiter }.Concat(this.SpecialInvalidValueCharacters);
		protected internal virtual char NameValueDelimiter => DistinguishedNameComponent.DefaultNameValueDelimiter;
		protected internal virtual IEnumerable<char> SpecialInvalidValueCharacters => _specialInvalidValueCharacters;
		protected internal virtual Regex ValidNameRegularExpression => _validNameRegularExpression;

		#endregion

		#region Methods

		public virtual IList<Exception> ValidateName(string name)
		{
			var exceptions = new List<Exception>();

			if(name == null)
				exceptions.Add(new ArgumentNullException(nameof(name)));
			else if(name.Length == 0)
				exceptions.Add(new ArgumentException("The name can not be empty.", nameof(name)));
			else if(!this.ValidNameRegularExpression.IsMatch(name))
				exceptions.Add(new ArgumentException($"The name \"{name}\" is invalid.", nameof(name)));

			return exceptions;
		}

		public virtual IList<Exception> ValidateValue(string value)
		{
			var exceptions = new List<Exception>();

			if(value == null)
			{
				exceptions.Add(new ArgumentNullException(nameof(value)));
			}
			else
			{
				var temporaryValue = value;

				// ReSharper disable All

				foreach(var invalidValueCharacter in this.InvalidValueCharacters)
				{
					temporaryValue = temporaryValue.Replace(@"\" + invalidValueCharacter, string.Empty);
				}

				foreach(var character in temporaryValue)
				{
					foreach(var invalidValueCharacter in this.InvalidValueCharacters)
					{
						if(character.Equals(invalidValueCharacter))
							exceptions.Add(new ArgumentException($"The value \"{value}\" is invalid. The value can not contain the character '{invalidValueCharacter}'. If you need to include the character you have to escape it.", nameof(value)));
					}
				}

				// ReSharper restore All
			}

			return exceptions;
		}

		#endregion
	}
}