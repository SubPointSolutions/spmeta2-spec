using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMeta2.Spec.ViewModels
{
    public class DefinitionPropertyMetadataViewModel
    {
        #region properties

        public string Title
        {
            get
            {
                if (!string.IsNullOrEmpty(Name))
                    return SpecServiceFactory.Istance.DisplayService.ToTitleCase(Name);

                return string.Empty;
            }
            set { }
        }
        public string Name { get; set; }

        public Type DefinitionType { get; set; }

        public string DefinitionTypeName
        {
            get
            {
                if (DefinitionType != null)
                    return DefinitionType.Name;

                return string.Empty;
            }
        }

        #endregion
    }
}
