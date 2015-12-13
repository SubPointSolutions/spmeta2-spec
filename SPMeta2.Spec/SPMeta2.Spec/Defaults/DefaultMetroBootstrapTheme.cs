using System.Linq;
using SPMeta2.Spec.Themes;

namespace SPMeta2.Spec.Defaults
{
    public class DefaultMetroBootstrapTheme : DefaultTheme
    {
        #region constructors
        public DefaultMetroBootstrapTheme()
        {
            Name = "DefaultMetroBootstrapTheme";

            TemplateFiles.AddRange(FromResourceFolder(GetType().Assembly, "SPMeta2.Spec.Templates.MetroBootstrap"));
        }

        #endregion
    }
}
