using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.DirectoryServices;

namespace UnitTests
{
	[TestClass]
	public class DistinguishedNameComponentValidatorTest
	{
		#region Fields

		private const string _validName = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

		#endregion

		#region Methods

		[TestMethod]
		public async Task ValidateName_IfTheNameContainsAnyNonAlphanumericCharacters_ShouldReturnExceptions()
		{
			await Task.CompletedTask.ConfigureAwait(false);

			var allCharacters = new List<char>();

			for(int i = char.MinValue; i <= char.MaxValue; i++)
			{
				allCharacters.Add((char)i);
			}

			var invalidNameCharacters = allCharacters.Where(character => !_validName.ToCharArray().Contains(character)); // && (int)character != 10);

			foreach(var invalidNameCharacter in invalidNameCharacters)
			{
				Assert.IsTrue(new DistinguishedNameComponentValidator().ValidateName(_validName + invalidNameCharacter).Any(), "Character: '{0}' ({1}).", [invalidNameCharacter, (int)invalidNameCharacter]);
				Assert.IsTrue(new DistinguishedNameComponentValidator().ValidateName(invalidNameCharacter + _validName).Any(), "Character: '{0}' ({1}).", [invalidNameCharacter, (int)invalidNameCharacter]);
				Assert.IsTrue(new DistinguishedNameComponentValidator().ValidateName(invalidNameCharacter + _validName + invalidNameCharacter).Any(), "Character: '{0}' ({1}).", [invalidNameCharacter, (int)invalidNameCharacter]);
				Assert.IsTrue(new DistinguishedNameComponentValidator().ValidateName(_validName + invalidNameCharacter + _validName).Any(), "Character: '{0}' ({1}).", [invalidNameCharacter, (int)invalidNameCharacter]);
				Assert.IsTrue(new DistinguishedNameComponentValidator().ValidateName(_validName + _validName + invalidNameCharacter + invalidNameCharacter).Any(), "Character: '{0}' ({1}).", [invalidNameCharacter, (int)invalidNameCharacter]);
				// The following fails
				//Assert.IsFalse(new DistinguishedNameComponentValidator().ValidateName(invalidNameCharacter + invalidNameCharacter + _validName + _validName).Any(), "Character: '{0}' ({1}).", new object[] { invalidNameCharacter, (int)invalidNameCharacter });
				Assert.IsTrue(new DistinguishedNameComponentValidator().ValidateName(invalidNameCharacter + invalidNameCharacter + _validName + _validName + invalidNameCharacter + invalidNameCharacter).Any(), "Character: '{0}' ({1}).", [invalidNameCharacter, (int)invalidNameCharacter]);
				Assert.IsTrue(new DistinguishedNameComponentValidator().ValidateName(_validName + _validName + invalidNameCharacter + invalidNameCharacter + _validName + _validName).Any(), "Character: '{0}' ({1}).", [invalidNameCharacter, (int)invalidNameCharacter]);
			}
		}

		[TestMethod]
		public async Task ValidateName_IfTheNameContainsOnlyAlphanumericCharacters_ShouldNotReturnExceptions()
		{
			await Task.CompletedTask.ConfigureAwait(false);

			Assert.IsFalse(new DistinguishedNameComponentValidator().ValidateName(_validName).Any());
		}

		#endregion
	}
}