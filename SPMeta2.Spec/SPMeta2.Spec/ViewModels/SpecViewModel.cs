using System.Collections.Generic;
using SPMeta2.Spec.Config;
using SPMeta2.Spec.Services;
using SPMeta2.Spec.Themes;

namespace SPMeta2.Spec.ViewModels
{
    public class SpecViewModel
    {
        #region constructors

        public SpecViewModel()
        {
            Definitions = new List<DefinitionViewModel>();
        }

        #endregion

        #region properties
        public SpecMetadata Metadata { get; set; }

        public List<DefinitionViewModel> Definitions { get; set; }

        public ThemeBase Theme { get; set; }
        public SpecConfig Config { get; set; }
        #endregion
    }
}
