using SPMeta2.Spec.Services;
using SPMeta2.Spec.Services.Common;

namespace SPMeta2.Spec
{
    internal class SpecServiceFactory
    {
        #region constructors

        internal SpecServiceFactory()
        {
            DisplayService = new DisplayService();
        }

        #endregion

        #region static

        public static SpecServiceFactory Istance = new SpecServiceFactory();

        #endregion

        #region properties
        public DisplayService DisplayService { get; set; }

        #endregion
    }
}
