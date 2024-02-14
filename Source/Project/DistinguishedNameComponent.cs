using System.Diagnostics.CodeAnalysis;

namespace RegionOrebroLan.DirectoryServices
{
	public class DistinguishedNameComponent : IDistinguishedNameComponent
	{
		#region Fields

		private const StringComparison _nameComparison = StringComparison.OrdinalIgnoreCase;
		private const StringComparison _valueComparison = StringComparison.OrdinalIgnoreCase;
		public const char DefaultNameValueDelimiter = '=';

		#endregion

		#region Constructors

		public DistinguishedNameComponent(string name, string value, IDistinguishedNameComponentValidator distinguishedNameComponentValidator)
		{
			if(distinguishedNameComponentValidator == null)
				throw new ArgumentNullException(nameof(distinguishedNameComponentValidator));

			var exceptions = distinguishedNameComponentValidator.ValidateName(name);

			if(exceptions.Any())
				throw exceptions.First();

			exceptions = distinguishedNameComponentValidator.ValidateValue(value);

			if(exceptions.Any())
				throw exceptions.First();

			this.Name = name;
			this.Value = value;
		}

		#endregion

		#region Properties

		public virtual string Name { get; }
		public virtual DistinguishedNameCase NameCase { get; set; } = DistinguishedNameCase.None;
		protected internal virtual StringComparison NameComparison => _nameComparison;
		protected internal virtual char NameValueDelimiter => DefaultNameValueDelimiter;
		public virtual string Value { get; }
		protected internal virtual StringComparison ValueComparison => _valueComparison;

		#endregion

		#region Methods

		public override bool Equals(object obj)
		{
			return this.Equals(obj as IDistinguishedNameComponent);
		}

		public virtual bool Equals(IDistinguishedNameComponent other)
		{
			if(other == null)
				return false;

			if(!string.Equals(this.Name, other.Name, this.NameComparison))
				return false;

			// ReSharper disable ConvertIfStatementToReturnStatement

			if(!string.Equals(this.Value, other.Value, this.ValueComparison))
				return false;

			// ReSharper restore ConvertIfStatementToReturnStatement

			return true;
		}

		public override int GetHashCode()
		{
			return this.ToString().ToUpperInvariant().GetHashCode();
		}

		public override string ToString()
		{
			return this.ToString(this.NameCase);
		}

		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		public virtual string ToString(DistinguishedNameCase nameCase)
		{
			var name = this.Name;

			name = nameCase switch
			{
				DistinguishedNameCase.Lower => name.ToLowerInvariant(),
				DistinguishedNameCase.Upper => name.ToUpperInvariant(),
				DistinguishedNameCase.None => name,
				_ => throw new InvalidOperationException($"Name-case \"{nameCase}\" is invalid.")
			};

			return name + this.NameValueDelimiter + this.Value;
		}

		#endregion
	}
}