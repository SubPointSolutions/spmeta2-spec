using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMeta2.Spec.Config
{
    public class SpecTypedItemConfig
    {
        #region properties

        public Type TargetType { get; set; }

        public List<object> Headers { get; set; }
        public List<object> Values { get; set; }

        #endregion
    }
}
