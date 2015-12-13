using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPMeta2.Standard.Definitions.Taxonomy;

namespace SpecIntranet.Model.Definitions.Taxonomy
{
    public static class SpecsTaxonomyTermSets
    {
        public static TaxonomyTermSetDefinition OrderTypes = new TaxonomyTermSetDefinition
        {
            Name = "Order Types"
        };

        public static TaxonomyTermSetDefinition ClientTypes = new TaxonomyTermSetDefinition
        {
            Name = "Client Types"
        };

        public static TaxonomyTermSetDefinition BusinessTypes = new TaxonomyTermSetDefinition
        {
            Name = "Business Types"
        };
    }
}
