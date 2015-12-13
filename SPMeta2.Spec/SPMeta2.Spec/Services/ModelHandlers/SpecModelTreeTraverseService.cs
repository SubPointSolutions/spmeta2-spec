using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPMeta2.Common;
using SPMeta2.Definitions;
using SPMeta2.Extensions;
using SPMeta2.Models;
using SPMeta2.Services;

namespace SPMeta2.Spec.Services.ModelHandlers
{
    internal class SpecModelTreeTraverseService : ModelTreeTraverseServiceBase
    {
        #region constructors

        #endregion

        #region properties

        private ModelWeighServiceBase _modelWeighService;

        public ModelWeighServiceBase ModelWeighService
        {
            get
            {
                if (_modelWeighService == null)
                    _modelWeighService = ServiceContainer.Instance.GetService<ModelWeighServiceBase>();

                return _modelWeighService;
            }
            set { _modelWeighService = value; }
        }

        #endregion

        #region methods

        public override void Traverse(object modelHost, ModelNode modelNode)
        {
            (modelHost as SpecViewModelHostBase).ModelNode = modelNode;

            var modelDefinition = modelNode.Value as DefinitionBase;
            var modelHandler = OnModelHandlerResolve(modelNode);

            if (OnModelFullyProcessing != null)
                OnModelFullyProcessing(modelNode);

            if (OnModelProcessing != null)
                OnModelProcessing(modelNode);

            // always true for specs
            var requireselfProcessing = true;

            modelHandler.DeployModel(modelHost, modelNode.Value);

            if (OnModelProcessed != null)
                OnModelProcessed(modelNode);

            var childModelTypes = GetSortedChildModelTypes(modelNode);

            foreach (var childModelType in childModelTypes)
            {
                var childModels = modelNode.GetChildModels(childModelType.Key).ToList();
                ModelWeighService.SortChildModelNodes(modelNode, childModels);

                if (OnChildModelsProcessing != null)
                    OnChildModelsProcessing(modelNode, childModelType.Key, childModels);

                foreach (var childModel in childModels)
                {
                    modelHandler.WithResolvingModelHost(new ModelHostResolveContext
                    {
                        ModelHost = modelHost,
                        Model = modelDefinition,
                        ChildModelType = childModelType.Key,
                        ModelNode = modelNode,
                        Action = childModelHost =>
                        {
                            Traverse(childModelHost, childModel);
                        }
                    });
                }

                if (OnChildModelsProcessed != null)
                    OnChildModelsProcessed(modelNode, childModelType.Key, childModels);
            }

            if (OnModelFullyProcessed != null)
                OnModelFullyProcessed(modelNode);

        }

        #endregion

        #region utils

        private IEnumerable<IGrouping<Type, Type>> GetSortedChildModelTypes(ModelNode modelNode)
        {
            var modelDefinition = modelNode.Value;
            var modelWeights = ModelWeighService.GetModelWeighs();

            var childModelTypes = modelNode.ChildModels
                .Select(m => m.Value.GetType())
                .GroupBy(t => t)
                .ToList();

            var currentModelWeights = modelWeights.FirstOrDefault(w => w.Model == modelDefinition.GetType());

            if (currentModelWeights != null)
            {
                childModelTypes.Sort(delegate(IGrouping<Type, Type> p1, IGrouping<Type, Type> p2)
                {
                    var srcW = int.MaxValue;
                    var dstW = int.MaxValue;

                    // resolve model wight by current class or subclasses
                    // subclasses lookup is required for all XXX_FieldDefinitions 

                    // this to be extracted later as service 
                    // stongly by type, and then by casting
                    var p1Type = currentModelWeights.ChildModels
                                                    .Keys.FirstOrDefault(k => k == p1.Key);

                    if (p1Type == null)
                    {
                        p1Type = currentModelWeights.ChildModels
                                                    .Keys.FirstOrDefault(k => k.IsAssignableFrom(p1.Key));
                    }

                    var p2Type = currentModelWeights.ChildModels
                                                    .Keys.FirstOrDefault(k => k == p2.Key);

                    if (p2Type == null)
                    {
                        p2Type = currentModelWeights.ChildModels
                                                    .Keys.FirstOrDefault(k => k.IsAssignableFrom(p2.Key));
                    }

                    if (p1Type != null)
                        srcW = currentModelWeights.ChildModels[p1Type];

                    if (p2Type != null)
                        dstW = currentModelWeights.ChildModels[p2Type];

                    return srcW.CompareTo(dstW);
                });
            }

            return childModelTypes;
        }

        #endregion
    }
}
