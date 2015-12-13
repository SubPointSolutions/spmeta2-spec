using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecIntranet.Model.Models;
using SPMeta2.Models;
using SPMeta2.Spec.Defaults;
using SPMeta2.Spec.Services;
using SPMeta2.Spec.Themes;

namespace SPMeta2.Spec.Tests.Tests
{
    [TestClass]
    public class SpecTests
    {
        #region constructors
        public SpecTests()
        {

        }
        #endregion

        #region methods

        #endregion

        #region tests

        [TestMethod]
        [TestCategory("Spec.Validation")]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_Raise_ArgumentException_On_Null_Metadata()
        {
            var specService = new SpecService();

            specService.GenerateSpecs(null, new[] { new ModelNode() });
        }


        [TestMethod]
        [TestCategory("Spec.Validation")]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_Raise_ArgumentException_On_Null_Models()
        {
            var specService = new SpecService();

            specService.GenerateSpecs(new SpecMetadata(), null);
        }

        [TestMethod]
        [TestCategory("Spec.Validation")]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_Raise_ArgumentException_On_Empty_Models()
        {
            var specService = new SpecService();

            specService.GenerateSpecs(new SpecMetadata(), new ModelNode[] { });
        }

        [TestMethod]
        [TestCategory("Spec")]
        public void Generate_SpecIntranet_Spec()
        {
            // 1 - create spec service
            var specService = new SpecService();

            // 2- define metadata of the specs to be generat
            // SpecMetadata follows Nuspec Reference - https://docs.nuget.org/create/nuspec-reference
            var specMetadata = new SpecMetadata
            {
                Id = "Spec.Intranet.Model",
                Title = "Spec Intranet",
                Version = "1.0.0.0",
                Description = "Spec Intranet - enables users to colaborate efficiently",
                Summary = "Stop dreaming about it, you have found the easy intranet solution. Spec Intranet is everything you need to create an intranet instantly and boots up provictivity in few sample clicks.",
                Copyright = "Copyright © Spec Intranet 2015"
            };

            // 3 - optionally create an instance of the theme
            // default themes are under 'SPMeta2.Spec.Defaults' namespace

            // spec theme
            // ThemeBase specTheme = null;
            //ThemeBase specTheme = new DefaultTheme();
            //ThemeBase specTheme = new DefaultBlueprintTheme();
            //ThemeBase specTheme = new DefaultMetroBootstrapTheme();
            ThemeBase specTheme = new DefaultMetroBootstrapBlueprintTheme();

            // 4 - create a list of the models to be used in the final spec
            var specModels = new List<ModelNode>();

            var siteModel = new SpecsSiteModel();
            var rootWebModel = new SpecsRootWebModel();

            specModels.Add(siteModel.GetFieldsAndContentTypesModel());
            specModels.Add(siteModel.GetSiteFeaturesModel());
            specModels.Add(siteModel.GetSiteSecurityModel());
            specModels.Add(siteModel.GetUserCustomActionModel());

            specModels.Add(rootWebModel.GetModel());
            specModels.Add(rootWebModel.GetStyleLibraryModel());

            // 5 - generate the spec
            // the spec is one-page HTML file

            // current themes use Bootstrap, embed all css/js/font in the page
            // all themes are 'printable', so that you can print all the info as you need
            var specFileContent = specService.GenerateSpecs(
                                    specMetadata,
                                    specModels,
                                    specTheme);

            // just save the file and open in the browser
            //var specFileName = string.Format("m2-spec-{0}.html", Environment.TickCount);
            var specFileName = string.Format("m2-spec-{0}.html", specTheme.Name);
            File.WriteAllText(specFileName, specFileContent);

            //Process.Start("chrome.exe", specFileName);
            Process.Start("explorer.exe", specFileName);
        }

        #endregion
    }
}
