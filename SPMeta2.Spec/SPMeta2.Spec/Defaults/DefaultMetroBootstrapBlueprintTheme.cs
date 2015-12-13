using System.Linq;
using SPMeta2.Spec.Themes;

namespace SPMeta2.Spec.Defaults
{
    public class DefaultMetroBootstrapBlueprintTheme : DefaultMetroBootstrapTheme
    {
        #region constructors
        public DefaultMetroBootstrapBlueprintTheme()
        {
            Name = "MetroBootstrapBlueprint";

            TemplateFiles.AddRange(FromResourceFolder(GetType().Assembly, "SPMeta2.Spec.Templates.MetroBootstrapBlueprint"));
        }

        #endregion
    }
}
