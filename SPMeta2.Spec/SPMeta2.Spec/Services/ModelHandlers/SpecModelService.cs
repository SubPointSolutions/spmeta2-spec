using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPMeta2.Common;
using SPMeta2.Definitions;
using SPMeta2.Extensions;
using SPMeta2.ModelHosts;
using SPMeta2.Models;
using SPMeta2.Services;
using SPMeta2.ModelHandlers;

namespace SPMeta2.Spec.Services.ModelHandlers
{
    internal class SpecModelService : DeploymentServiceBase
    {
        #region constructors

        public SpecModelService()
        {

        }

        #endregion

        #region methods

        public override void DeployModel(ModelHostBase modelHost, ModelNode model)
        {
            ModelTraverseService = new SpecModelTreeTraverseService();
            ModelTraverseService.OnModelHandlerResolve += ResolveDefaultModelHandler;

            (modelHost as SpecViewModelHostBase).CurrentViewModel = null;

            ModelTraverseService.Traverse(modelHost, model);
        }

        private ModelHandlerBase ResolveDefaultModelHandler(ModelNode arg)
        {
            return new SpecModelHandler();
        }

        #endregion
    }
}
