using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RegionOrebroLan.DirectoryServices;

namespace UnitTests
{
	[TestClass]
	public class DistinguishedNameComponentTest
	{
		#region Methods

		private static IDistinguishedNameComponentValidator CreateAcceptanceDistinguishedNameComponentValidator()
		{
			var distinguishedNameComponentValidatorMock = new Mock<IDistinguishedNameComponentValidator>();

			distinguishedNameComponentValidatorMock.Setup(distinguishedNameComponentValidator => distinguishedNameComponentValidator.ValidateName(It.IsAny<string>())).Returns([]);
			distinguishedNameComponentValidatorMock.Setup(distinguishedNameComponentValidator => distinguishedNameComponentValidator.ValidateValue(It.IsAny<string>())).Returns([]);

			return distinguishedNameComponentValidatorMock.Object;
		}

		private static DistinguishedNameComponent CreateDefaultDistinguishedNameComponent(string name = "TestName", string value = "TestValue")
		{
			return new DistinguishedNameComponent(name, value, CreateAcceptanceDistinguishedNameComponentValidator());
		}

		[TestMethod]
		public async Task NameCase_ShouldReturnNoneByDefault()
		{
			await Task.CompletedTask.ConfigureAwait(false);

			Assert.AreEqual(DistinguishedNameCase.None, CreateDefaultDistinguishedNameComponent().NameCase);
		}

		#endregion
	}
}