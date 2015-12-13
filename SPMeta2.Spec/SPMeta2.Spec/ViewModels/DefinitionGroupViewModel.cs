using System.Collections.Generic;

namespace SPMeta2.Spec.ViewModels
{
    public class DefinitionGroupViewModel : ViewModelBase
    {
        #region constructors

        public DefinitionGroupViewModel()
        {
            Definitions = new List<DefinitionViewModel>();

            Headers = new List<DefinitionPropertyTitleViewModel>();
            Values = new Dictionary<DefinitionViewModel, List<DefinitionPropertyValueViewModel>>();
        }

        #endregion

        #region properties

        public string Title { get; set; }
        public string Description { get; set; }

        public List<DefinitionViewModel> Definitions { get; set; }

        public List<DefinitionPropertyTitleViewModel> Headers { get; set; }
        public Dictionary<DefinitionViewModel, List<DefinitionPropertyValueViewModel>> Values { get; set; }

        #endregion
    }
}
