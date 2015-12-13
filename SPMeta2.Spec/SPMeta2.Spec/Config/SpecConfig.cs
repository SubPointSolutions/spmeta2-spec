using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPMeta2.Definitions;
using SPMeta2.Definitions.ContentTypes;
using SPMeta2.Spec.Themes;
using SPMeta2.Spec.Services;
using SPMeta2.Spec.Services.Common;

namespace SPMeta2.Spec.Config
{
    public class SpecConfig
    {
        #region constructors
        public SpecConfig()
        {
            DataLookupService = new DataLookupService();
        }

        #endregion

        #region properties

        public ThemeBase Theme { get; set; }

        public DataLookupService DataLookupService { get; set; }

        public List<SpecTypedItemConfig> DefinitionColumnsConfig { get; set; }

        public List<string> DefaultDefinitionProperties { get; set; }

        public SpecConfig MapColumns<TDefinition>(Action<TypedItemConfig<TDefinition>> typeConfig)
        {
            var config = DefinitionColumnsConfig
                .FirstOrDefault(d => d.TargetType == typeof(TDefinition)) as TypedItemConfig<TDefinition>;

            if (config == null)
            {
                config = new TypedItemConfig<TDefinition>();
                DefinitionColumnsConfig.Add(config);
            }

            if (typeConfig != null)
                typeConfig(config);

            return this;
        }

        #endregion
    }
}
