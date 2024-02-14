using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{
	// ReSharper disable All
	[TestClass]
	[SuppressMessage("Naming", "CA1716:Identifiers should not match keywords")]
	public static class Global
	{
		#region Fields

		public const string DefaultEnvironment = "Integration-test";
		public static readonly DirectoryInfo ProjectDirectory = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent!.Parent!.Parent!;

		#endregion
	}
	// ReSharper restore All
}