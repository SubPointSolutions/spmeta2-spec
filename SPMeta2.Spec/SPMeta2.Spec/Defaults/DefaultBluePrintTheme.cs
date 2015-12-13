using System.Linq;
using SPMeta2.Spec.Themes;

namespace SPMeta2.Spec.Defaults
{
    public class DefaultBlueprintTheme : DefaultTheme
    {
        #region constructors
        public DefaultBlueprintTheme()
        {
            Name = "DefaultBluePrintTheme";

            TemplateFiles.AddRange(FromResourceFolder(GetType().Assembly, "SPMeta2.Spec.Templates.BluePrint"));
        }

        #endregion
    }
}
