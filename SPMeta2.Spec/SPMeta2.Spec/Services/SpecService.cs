using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using RazorEngine;
using RazorEngine.Templating;
using SPMeta2.Common;
using SPMeta2.Definitions;
using SPMeta2.Definitions.ContentTypes;
using SPMeta2.Extensions;
using SPMeta2.ModelHandlers;
using SPMeta2.ModelHosts;
using SPMeta2.Models;
using SPMeta2.Services;
using SPMeta2.Spec.Config;
using SPMeta2.Spec.Defaults;
using SPMeta2.Spec.Services.ModelHandlers;
using SPMeta2.Spec.ViewModels;
using SPMeta2.Spec.Themes;

namespace SPMeta2.Spec.Services
{
    /// <summary>
    /// A service to generate models specs
    /// Use GenerateSpecs() methods to create new spec
    /// </summary>
    public class SpecService
    {
        #region constructors

        public SpecService()
        {
            Config = new DefaultSpecServiceConfig();
        }

        #endregion

        #region properties

        public SpecConfig Config { get; set; }

        #endregion

        #region configuration

        public void Configure(Action<SpecConfig> action)
        {
            action(Config);
        }

        #endregion

        #region methods

        public string GenerateSpecs(SpecMetadata metadata, IEnumerable<ModelNode> models)
        {
            return GenerateSpecs(metadata, models, Config.Theme);
        }

        public string GenerateSpecs(SpecMetadata metadata, IEnumerable<ModelNode> models, ThemeBase theme)
        {
            if (metadata == null)
                throw new ArgumentException("metadata is null");

            if (models == null || !models.Any())
                throw new ArgumentException("models collection is null or empty");

            var currentTheme = theme ?? Config.Theme;

            var modelService = new SpecModelService();
            var modelHost = new SpecViewModelHostBase();

            modelHost.ViewModel.Metadata = metadata;
            modelHost.ViewModel.Theme = currentTheme;

            modelHost.ViewModel.Config = Config;

            modelHost.Config = Config;

            // generate view model
            foreach (var model in models)
                modelService.DeployModel(modelHost, model);

            Config.DataLookupService.Models = models.ToList();

            // process theme, pre-load templates
            var themeRenderTemplateContent = currentTheme.MainTemplate.Content;

            foreach (var template in currentTheme.Templates)
                Engine.Razor.AddTemplate(template.Name, template.Content);

            // process view bag
            var viewBag = new DynamicViewBag();
            viewBag.AddValue("Config", Config);

            // render specs
            var result = Engine.Razor.RunCompile(
                themeRenderTemplateContent,
                currentTheme.MainTemplate.Name,
                null,
                modelHost.ViewModel,
                viewBag);

            return result;
        }

        #endregion
    }
}
