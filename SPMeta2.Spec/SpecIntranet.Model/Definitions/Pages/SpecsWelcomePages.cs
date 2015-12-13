using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPMeta2.BuiltInDefinitions;
using SPMeta2.Definitions;
using SPMeta2.Utils;

namespace SpecIntranet.Model.Definitions.Pages
{
    public static class SpecsWelcomePages
    {
        // use UrlUtility.CombineUrl() to construct URL in a safely manner
        // refer your definition, reuse them - enable refactoring

        public static WelcomePageDefinition HomeLandingPage = new WelcomePageDefinition
        {
            Url = UrlUtility.CombineUrl("SitePages", SpecsWebPartPages.LandingPage.FileName)
        };
    }
}
