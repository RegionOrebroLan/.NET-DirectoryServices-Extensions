using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.DirectoryServices;

namespace IntegrationTests
{
	[TestClass]
	public class DistinguishedNameParserTest
	{
		#region Properties

		protected internal virtual IDistinguishedNameComponentValidator DistinguishedNameComponentValidator => new DistinguishedNameComponentValidator();

		#endregion

		#region Methods

		[TestMethod]
		public async Task Parse_Test()
		{
			await Task.CompletedTask.ConfigureAwait(false);

			var distinguishedName = new DistinguishedNameParser(this.DistinguishedNameComponentValidator).Parse("E=first-name.last-name@example.org, SERIALNUMBER=ABxxxxxxxxxx-abc123, G=First-name, SN=Last-name, CN=First-name Last-name [abc123], O=Company, L=ÅÄÖ åäö, C=SE");

			Assert.AreEqual(8, distinguishedName.Components.Count);
			Assert.AreEqual("E", distinguishedName.Components[0].Name);
			Assert.AreEqual("first-name.last-name@example.org", distinguishedName.Components[0].Value);
			Assert.AreEqual("SERIALNUMBER", distinguishedName.Components[1].Name);
			Assert.AreEqual("ABxxxxxxxxxx-abc123", distinguishedName.Components[1].Value);
			Assert.AreEqual("G", distinguishedName.Components[2].Name);
			Assert.AreEqual("First-name", distinguishedName.Components[2].Value);

			var parent = distinguishedName.Parent;
			Assert.AreEqual(7, parent.Components.Count);
			Assert.AreEqual("SERIALNUMBER", parent.Components[0].Name);
			Assert.AreEqual("ABxxxxxxxxxx-abc123", parent.Components[0].Value);
		}

		#endregion
	}
}