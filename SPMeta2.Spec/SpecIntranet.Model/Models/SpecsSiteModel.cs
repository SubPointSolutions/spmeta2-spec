using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecIntranet.Model.Definitions.Features;
using SpecIntranet.Model.Definitions.IA;
using SpecIntranet.Model.Definitions.Security;
using SpecIntranet.Model.Definitions.Solutions;
using SpecIntranet.Model.Definitions.UI;
using SPMeta2.Extensions;
using SPMeta2.Models;
using SPMeta2.Syntax.Default;

namespace SpecIntranet.Model.Models
{
    /// <summary>
    /// Describes the site collection.
    /// 
    /// The main responcibility is to describe and separate what needs to be deployed on the site collection level:
    /// - sandbox solutions
    /// - site features
    /// - security group and roles
    /// - fields
    /// - content types
    /// - user custom actions
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
    public class SpecsSiteModel
    {
        public ModelNode GetSandboxSolutionsModel()
        {
            var model = SPMeta2Model.NewSiteModel(site =>
            {
                // either use AddXXX() or just import everything with .AddDefinitionsFromStaticClassType() 
                // !!! commented out as there is no *.wsp package out there !!!

                //site.AddSandboxSolution(SpecsSandboxSolutions.WebsiteBranding);
                //site.AddDefinitionsFromStaticClassType(typeof(SpecsSandboxSolutions));
            });

            return model;
        }

        public ModelNode GetSiteSecurityModel()
        {
            var model = SPMeta2Model.NewSiteModel(site =>
            {
                site.AddDefinitionsFromStaticClassType(typeof(SpecsSecurityGroups));
                site.AddDefinitionsFromStaticClassType(typeof(SpecsSecurityRoles));
            });

            return model;
        }

        public ModelNode GetSiteFeaturesModel()
        {
            var model = SPMeta2Model.NewSiteModel(site =>
            {
                // either use AddXXX() or just import everything with .AddDefinitionsFromStaticClassType() 

                site.AddSiteFeature(SpecsSiteFeatures.BasicWebParts);
                //site.AddDefinitionsFromStaticClassType(typeof(SpecsSiteFeatures));
            });

            return model;
        }

        public ModelNode GetFieldsAndContentTypesModel()
        {
            var model = SPMeta2Model.NewSiteModel(site =>
            {
                // adding all fields from SpecsFields class
                site.AddDefinitionsFromStaticClassType(typeof(SpecsFields));

                // building up content types
                site
                    .AddContentType(SpecsContentTypes.CompanyDocument, contentType =>
                    {
                        contentType
                            .AddContentTypeFieldLink(SpecsFields.DocumentHighlights)
                            .AddContentTypeFieldLink(SpecsFields.DocumentDescription);
                    })
                    .AddContentType(SpecsContentTypes.SalesProposal, contentType =>
                    {

                    })
                    .AddContentType(SpecsContentTypes.ProductDocument, contentType =>
                    {

                    })
                    .AddContentType(SpecsContentTypes.OrderDocument, contentType =>
                    {
                        contentType
                            .AddContentTypeFieldLink(SpecsFields.OrderDate)
                            .AddContentTypeFieldLink(SpecsFields.OrderAddressState)
                            .AddContentTypeFieldLink(SpecsFields.OrderPrice)
                            .AddContentTypeFieldLink(SpecsFields.OrderSalePercentage)
                            .AddContentTypeFieldLink(SpecsFields.OrderTrackingUrl);
                    })
                    .AddContentType(SpecsContentTypes.ProductOrService, contentType =>
                    {
                        contentType
                            .AddContentTypeFieldLink(SpecsFields.ProductDescription)
                            .AddContentTypeFieldLink(SpecsFields.ProductType)
                            .AddContentTypeFieldLink(SpecsFields.IsProductActive);
                    })
                    .AddContentType(SpecsContentTypes.SaleEvents, contentType =>
                    {

                    });
            });

            return model;
        }

        public ModelNode GetTaxonomyModel()
        {
            var model = SPMeta2Model.NewSiteModel(site =>
            {
                // skipped, it it might be a foundation project setup
                // refer to Taxonomy provision scenarios here:

                // http://docs.subpointsolutions.com/spmeta2/scenarios/
                // http://docs.subpointsolutions.com/spmeta2/definitions/sharepoint-standard/taxonomy/taxonomytermdefinition/
            });

            return model;
        }

        public ModelNode GetUserCustomActionModel()
        {
            var model = SPMeta2Model.NewSiteModel(site =>
            {
                site.AddUserCustomAction(SpecsUserCustomActions.jQuery);
                site.AddUserCustomAction(SpecsUserCustomActions.SpecsJs);
            });

            return model;
        }
    }
}
