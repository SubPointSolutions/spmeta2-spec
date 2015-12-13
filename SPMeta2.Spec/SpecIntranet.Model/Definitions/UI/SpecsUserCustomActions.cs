using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPMeta2.Definitions;

namespace SpecIntranet.Model.Definitions.UI
{
    public static class SpecsUserCustomActions
    {
        // use ~sitecollection or ~site tokens, they are suppoerted by M2
        // use UrlUtility.CombineUrl() to construct URL in a safely manner

        public static UserCustomActionDefinition jQuery = new UserCustomActionDefinition
        {
            Name = "SpecsjQuery",
            Location = "ScriptLink",
            ScriptSrc = "~sitecollection/Style Library/Specs.Intranet/3rd part/jQuery/jquery-1.11.3.min.js",
            Sequence = 15000
        };

        public static UserCustomActionDefinition SpecsJs = new UserCustomActionDefinition
        {
            Name = "Specsjs",
            Location = "ScriptLink",
            ScriptSrc = "~sitecollection/Style Library/Specs.Intranet/js/Specs.Intranet.js",
            Sequence = 17000
        };
    }
}
