using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPMeta2.Spec.Services.Common;

namespace SPMeta2.Spec.Tests.Tests
{

    [TestClass]
    public class DisplayServiceTests
    {
        #region constructors

        public DisplayServiceTests()
        {
            Service = new DisplayService();
        }

        #endregion

        #region properties

        public DisplayService Service { get; set; }

        #endregion

        #region tests

        [TestMethod]
        [TestCategory("Services.DisplayService")]
        public void ToTitleCase()
        {
            var srcValues = new string[]
            {
                "myCamelCase",
                "MyCamelCase",
                "my Camel Case",
                "My Camel Case",
                "My Camel Case ",
                " My Camel Case "
            };

            foreach (var value in srcValues)
            {
                Assert.AreEqual("My Camel Case", Service.ToTitleCase(value));
            }
        }


        [TestMethod]
        [TestCategory("Services.DisplayService")]
        public void ToTitleCase_Edges()
        {
            Assert.AreEqual(string.Empty, Service.ToTitleCase(null));
            Assert.AreEqual(string.Empty, Service.ToTitleCase(string.Empty));
            Assert.AreEqual(string.Empty, Service.ToTitleCase(""));
            Assert.AreEqual(string.Empty, Service.ToTitleCase(" "));
            Assert.AreEqual(string.Empty, Service.ToTitleCase("  "));
            Assert.AreEqual(string.Empty, Service.ToTitleCase("     "));
        }

        #endregion
    }
}
