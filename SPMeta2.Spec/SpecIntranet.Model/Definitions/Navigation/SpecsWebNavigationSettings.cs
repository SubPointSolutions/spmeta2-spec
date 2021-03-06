﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPMeta2.Enumerations;
using SPMeta2.Standard.Definitions;

namespace SpecIntranet.Model.Definitions.Navigation
{
    public static class SpecsWebNavigationSettings
    {
        // they require Standard+ (publishing infrastructure) and deployed under the web
        // correspond to everything you see on Look and Feel -> Navigation' page ( _layouts/15/AreaNavigationSettings.aspx)

        #region samples

        public static WebNavigationSettingsDefinition PortalProviderShowSubsites = new WebNavigationSettingsDefinition
        {
            GlobalNavigationSource = BuiltInStandardNavigationSources.PortalProvider,

            GlobalNavigationShowPages = false,
            GlobalNavigationShowSubsites = true,

            CurrentNavigationSource = BuiltInStandardNavigationSources.PortalProvider,

            CurrentNavigationShowPages = false,
            CurrentNavigationShowSubsites = true,
        };

        #endregion
    }
}
