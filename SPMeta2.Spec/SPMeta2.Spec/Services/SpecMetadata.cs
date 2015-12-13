using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMeta2.Spec.Services
{
    /// <summary>
    /// Metadata for the tgenerated models specfication
    /// Follows Nuspec Reference - https://docs.nuget.org/create/nuspec-reference
    /// </summary>
    public class SpecMetadata
    {
        #region properties

        public string Id { get; set; }
        public string Version { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Description { get; set; }
        public string ReleaseNotes { get; set; }
        public string Summary { get; set; }
        public string LicenseUrl { get; set; }
        public string Copyright { get; set; }

        #endregion
    }
}
