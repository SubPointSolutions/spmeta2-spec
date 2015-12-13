using SPMeta2.Spec.Config;

namespace SPMeta2.Spec.ViewModels
{
    public class ViewModelBase
    {
        #region properties

        public SpecConfig Config { get; set; }

        #endregion

        #region properties

        public string Hash
        {
            get
            {
                return GetHashCode().ToString();
            }
        }

        public string ClientId
        {
            get
            {


                return "id_" + Hash;
            }
        }

        public virtual bool IsExpandable
        {
            get
            {
                return false;
            }
        }

        #endregion
    }
}
