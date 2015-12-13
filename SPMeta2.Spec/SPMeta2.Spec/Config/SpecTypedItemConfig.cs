using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SPMeta2.Spec.Config
{
    public class TypedItemConfig<TDefinition> : SpecTypedItemConfig
    {
        #region constructors

        public TypedItemConfig()
        {
            TargetType = typeof(TDefinition);

            Headers = new List<object>();
            Values = new List<object>();
        }

        #endregion

        #region methods

        public TypedItemConfig<TDefinition> Column(
            Expression<Func<TDefinition, object>> header,
            Expression<Func<TDefinition, object>> value)
        {
            Headers.Add(header);
            Values.Add(value);

            return this;
        }

        #endregion
    }
}
