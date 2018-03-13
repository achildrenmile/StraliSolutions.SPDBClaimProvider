using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Administration.Claims;

namespace StraliSolutions.SPDBClaimProvider
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("c6b0f430-bab3-4245-9af4-16087b95a40e")]
    public class FarmEventReceiver : SPClaimProviderFeatureReceiver
    {
      
            public override string ClaimProviderAssembly
            {
                get { return typeof(DBClaimProvider).Assembly.FullName; }
            }

            public override string ClaimProviderDescription
            {
                get { return "DB claim provider"; }
            }

            public override string ClaimProviderDisplayName
            {
                get { return DBClaimProvider.ProviderDisplayName; }
            }

            public override string ClaimProviderType
            {
                get { return typeof(DBClaimProvider).FullName; }
            }

        
        // Uncomment the method below to handle the event raised after a feature has been activated.

        //public override void FeatureActivated(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
