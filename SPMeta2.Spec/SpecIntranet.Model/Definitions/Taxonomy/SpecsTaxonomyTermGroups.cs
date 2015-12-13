using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPMeta2.Standard.Definitions.Taxonomy;

namespace SpecIntranet.Model.Definitions.Taxonomy
{
    public static class SpecsTaxonomyTermGroups
    {
        // Use IsSiteCollectionGroup to indicate default site scoped taxonomy group 

        public static TaxonomyTermGroupDefinition OrdersCRM = new TaxonomyTermGroupDefinition
        {
            Name = "Orders CRM"
        };
    }
}
