using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPMeta2.BuiltInDefinitions;
using SPMeta2.Standard.Definitions;
using SPMeta2.Standard.Enumerations;

namespace SpecIntranet.Model.Definitions.Pages
{
    public static class SpecsPublishingPages
    {
        // use BuiltInPublishingPageLayoutNames to refer OOTB page layouts
        // or define the target file name

        #region samples

        public static PublishingPageDefinition About = new PublishingPageDefinition
        {
            Title = "About",
            FileName = "About.aspx",
            Content = "Some facinatins content about the company",
            PageLayoutFileName = BuiltInPublishingPageLayoutNames.ArticleLeft
        };

        public static PublishingPageDefinition AnnualPerformance = new PublishingPageDefinition
        {
            Title = "AnnualPerformance",
            FileName = "AnnualPerformance.aspx",
            PageLayoutFileName = BuiltInPublishingPageLayoutNames.BlankWebPartPage
        };

        #endregion
    }
}
