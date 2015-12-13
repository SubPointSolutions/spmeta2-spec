using System.Linq;
using SPMeta2.Spec.Themes;

namespace SPMeta2.Spec.Defaults
{
    public class DefaultTheme : ThemeBase
    {
        #region constructors
        public DefaultTheme()
        {
            Name = "DefaultTheme";

            TemplateFiles = FromResourceFolder(GetType().Assembly, "SPMeta2.Spec.Templates.Default");
        }

        #endregion
    }
}
