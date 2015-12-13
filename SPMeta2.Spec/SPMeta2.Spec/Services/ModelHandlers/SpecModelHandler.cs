using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPMeta2.Common;
using SPMeta2.Definitions;
using SPMeta2.ModelHandlers;
using SPMeta2.Spec.ViewModels;

namespace SPMeta2.Spec.Services.ModelHandlers
{
    internal class SpecModelHandler : ModelHandlerBase
    {
        #region constructors
        public SpecModelHandler()
        {

        }
        #endregion

        #region properties

        public override Type TargetType
        {
            get { return typeof(DefinitionBase); }
        }

        public DefinitionViewModel DefinitionViewModel { get; set; }

        #endregion

        #region methods
        public override void WithResolvingModelHost(ModelHostResolveContext context)
        {
            var typedHost = context.ModelHost as SpecViewModelHostBase;

            if (typedHost != null && DefinitionViewModel != null)
                typedHost.CurrentViewModel = DefinitionViewModel;
            else
                typedHost.CurrentViewModel = null;

            base.WithResolvingModelHost(context);
        }

        public override void DeployModel(object modelHost, DefinitionBase model)
        {
            var typedHost = modelHost as SpecViewModelHostBase;

            var viewModel = typedHost.ViewModel;

            DefinitionViewModel = new DefinitionViewModel
            {
                Config = typedHost.Config,
                RawModelNode = typedHost.ModelNode,
                RawDefinition = model
            };

            if (typedHost.CurrentViewModel == null)
            {
                viewModel.Definitions.Add(DefinitionViewModel);
            }
            else
            {
                DefinitionViewModel.Parent = typedHost.CurrentViewModel;
                typedHost.CurrentViewModel.Definitions.Add(DefinitionViewModel);
            }
        }
        #endregion
    }
}
