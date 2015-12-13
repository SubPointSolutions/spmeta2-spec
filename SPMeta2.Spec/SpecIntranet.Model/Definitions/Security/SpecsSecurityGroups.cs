using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPMeta2.Definitions;

namespace SpecIntranet.Model.Definitions.Security
{
    public static class SpecsSecurityGroups
    {
        public static SecurityGroupDefinition OrderApprovers = new SecurityGroupDefinition
        {
            Name = "Order Approvers",
            Description = "People who can approve orders."
        };

        public static SecurityGroupDefinition OrderReviewers = new SecurityGroupDefinition
        {
            Name = "Order Reviewers",
            Description = "People who can review orders."
        };
    }
}
