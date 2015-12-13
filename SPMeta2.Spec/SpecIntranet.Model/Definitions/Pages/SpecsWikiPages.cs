using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPMeta2.Definitions;
using SPMeta2.Enumerations;

namespace SpecIntranet.Model.Definitions.Pages
{
    public static class SpecsWikiPages
    {
        public static WikiPageDefinition FAQ = new WikiPageDefinition
        {
            Title = "FAQ",
            FileName = "FAQ.aspx",
        };

        public static WikiPageDefinition HowTos = new WikiPageDefinition
        {
            Title = "HowTos",
            FileName = "HowTos.aspx",
            Content = "Some fascinating how-tows here..."
        };
    }
}
