using SpecIntranet.Model.Definitions.IA;
using SPMeta2.Definitions;
using SPMeta2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecIntranet.Model.Definitions.Pages;

namespace SpecIntranet.Model.Definitions.Navigation
{
    public static class SpecsTopNavigationNodes
    {
        // use ~sitecollection or ~site tokens, they are suppoerted by M2
        // use UrlUtility.CombineUrl() to construct URL in a safely manner
        // refer your definition, reuse them - enable refactoring

        public static TopNavigationNodeDefinition CompanyDocuments = new TopNavigationNodeDefinition
        {
            Title = "Company Documents",
            Url = "~sitecollection/CompanyDocuments",
            IsExternal = true
        };

        public static TopNavigationNodeDefinition SaleTasks = new TopNavigationNodeDefinition
        {
            Title = "Sales Tasks",
            Url = UrlUtility.CombineUrl("~sitecollection", SpecsLists.SalesTasks.CustomUrl),
            IsExternal = true
        };

        public static TopNavigationNodeDefinition SaleEvents = new TopNavigationNodeDefinition
        {
            Title = "Sales Events",
            Url = UrlUtility.CombineUrl("~sitecollection", SpecsLists.SalesEvents.CustomUrl),
            IsExternal = true
        };


        public static TopNavigationNodeDefinition AboutThisSite = new TopNavigationNodeDefinition
        {
            Title = "About",
            Url = UrlUtility.CombineUrl("~site/SitePages/", SpecsWebPartPages.AboutThisSite.FileName),
            IsExternal = true
        };
    }
}
