using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPMeta2.ModelHosts;
using SPMeta2.Models;
using SPMeta2.Spec.Config;
using SPMeta2.Spec.ViewModels;

namespace SPMeta2.Spec.Services.ModelHandlers
{
    internal class SpecViewModelHostBase : ModelHostBase
    {
        public SpecViewModelHostBase()
        {
            ViewModel = new SpecViewModel();
        }

        public SpecViewModel ViewModel { get; set; }
        public DefinitionViewModel CurrentViewModel { get; set; }

        public ModelNode ModelNode { get; set; }



        public SpecConfig Config { get; set; }
    }
}
