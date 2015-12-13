using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecIntranet.Model.Consts;
using SpecIntranet.Model.Definitions.Features;
using SpecIntranet.Model.Definitions.IA;
using SpecIntranet.Model.Definitions.Navigation;
using SpecIntranet.Model.Definitions.Pages;
using SpecIntranet.Model.Definitions.UI;
using SPMeta2.BuiltInDefinitions;
using SPMeta2.Extensions;
using SPMeta2.Models;
using SPMeta2.Syntax.Default;
using SPMeta2.Syntax.Default.Utils;
using SPMeta2.VS.CSharp.Extensions;

namespace SpecIntranet.Model.Models
{
    /// <summary>
    /// Describes the root web.
    /// 
    /// The main responcibility is to describe and separate what needs to be deployed on the root web level:
    /// - web features
    /// - style library provision
    /// - anything else
    /// 
    /// Construct your model using AddXXX() or AddHostXXX() syntax.
    /// If you have bunch of the 'plain' things to push, then use .AddDefinitionsFromStaticClassType() method.
    /// 
    /// It is a good idea to avoid big model preferring small model per every reasonable operation.
    /// The final artifact separation and grouping is tootally up to you and your prpoject needs.
    /// 
    /// 
    /// Read more here - http://docs.subpointsolutions.com/spmeta2/models/
    /// </summary>
    public class SpecsRootWebModel
    {
        public ModelNode GetModel()
        {
            var model = SPMeta2Model.NewWebModel(web =>
            {
                web
                    .AddWebFeature(SpecsWebFeatures.DisableMinimalDownloadStrategy)
                    .AddWebFeature(SpecsWebFeatures.EnableTeamCollabirationLists)
                    .AddWebFeature(SpecsWebFeatures.WikiPageHomePage)

                    .AddTopNavigationNode(SpecsTopNavigationNodes.CompanyDocuments)
                    .AddTopNavigationNode(SpecsTopNavigationNodes.SaleTasks)
                    .AddTopNavigationNode(SpecsTopNavigationNodes.SaleEvents)
                    .AddTopNavigationNode(SpecsTopNavigationNodes.AboutThisSite)

                    .AddQuickLaunchNavigationNode(SpecsQuickNavigationNodes.CompanyDocuments)
                    .AddQuickLaunchNavigationNode(SpecsQuickNavigationNodes.Services)
                    .AddQuickLaunchNavigationNode(SpecsQuickNavigationNodes.Orders)
                    .AddQuickLaunchNavigationNode(SpecsQuickNavigationNodes.SaleTasks)
                    .AddQuickLaunchNavigationNode(SpecsQuickNavigationNodes.SaleEvents)
                    .AddQuickLaunchNavigationNode(SpecsQuickNavigationNodes.AboutThisSite)

                    .AddList(SpecsLists.CompanyDocuments, list =>
                    {
                        list
                            .AddContentTypeLink(SpecsContentTypes.CompanyDocument)
                            .AddContentTypeLink(SpecsContentTypes.SalesProposal)
                            .AddContentTypeLink(SpecsContentTypes.ProductDocument)

                            .AddListView(SpecsListViews.LastTenDocuments)
                            .AddListView(SpecsListViews.LastTenDocumentsMainPage)

                            .SpecsSetDefaultListContentType(SpecsContentTypes.CompanyDocument);
                    })
                    .AddList(SpecsLists.Orders, list =>
                    {
                        list
                            .AddContentTypeLink(SpecsContentTypes.OrderDocument)
                            .AddListView(SpecsListViews.Last25Orders)
                            .AddListView(SpecsListViews.Last10OrdersMainPage)

                            .SpecsSetDefaultListContentType(SpecsContentTypes.OrderDocument);
                    })
                    .AddList(SpecsLists.Services, list =>
                    {
                        list
                            .AddContentTypeLink(SpecsContentTypes.ProductOrService)
                            .AddListView(SpecsListViews.AllProducts)
                            .AddListView(SpecsListViews.LastTenServices)

                            .SpecsSetDefaultListContentType(SpecsContentTypes.ProductOrService);
                    })
                    .AddList(SpecsLists.SalesTasks, list =>
                    {

                    })
                    .AddList(SpecsLists.SalesEvents, list =>
                    {
                        list
                            .AddContentTypeLink(SpecsContentTypes.SaleEvents)

                            .SpecsSetDefaultListContentType(SpecsContentTypes.SaleEvents);
                    })
                    .AddHostList(BuiltInListDefinitions.SitePages, list =>
                    {
                        list.AddWebPartPage(SpecsWebPartPages.LandingPage, page =>
                        {
                            page
                                .AddWebPart(SpecsWebparts.NewServices)
                                .AddWebPart(SpecsWebparts.SalesTasks)
                                .AddWebPart(SpecsWebparts.LastDocuments)
                                .AddWebPart(SpecsWebparts.LastOrders)
                                .AddWebPart(SpecsWebparts.SalesEvents);
                        });

                        list.AddWebPartPage(SpecsWebPartPages.AboutThisSite, page =>
                        {
                            page
                                .AddWebPart(SpecsWebparts.M2YammerFeed)
                                .AddWebPart(SpecsWebparts.AboutThisSite);
                        });
                    })

                    .AddWelcomePage(SpecsWelcomePages.HomeLandingPage);
            });

            return model;
        }

        public ModelNode GetStyleLibraryModel()
        {
            var webModel = SPMeta2Model.NewWebModel(rootWeb =>
            {
                // AddHostList() to indicate that we don't provision list, but just look it up
                rootWeb.AddHostList(BuiltInListDefinitions.StyleLibrary, list =>
                {
                    //LoadModuleFilesFromLocalFolder() helper gets everything from the local folder
                    //and creates a new M2 model full of folders/module files

                    //everything you have in SpecsConsts.DefaultStyleLibraryPath
                    //will be pushed to 'Style Library' back to back

                    if (Directory.Exists(SpecsConsts.DefaultStyleLibraryPath))
                        ModuleFileUtils.LoadModuleFilesFromLocalFolder(list, SpecsConsts.DefaultStyleLibraryPath);
                });
            });

            return webModel;
        }
    }
}
