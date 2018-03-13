using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StraliSolutions.SPDBClaimProvider
{
    public class PropertyBagHandler
    {
        private string _webUrl;

        public PropertyBagHandler()
        {
            _webUrl = SPContext.Current.Web.Url;
        }

        public string getPropertyBagValue(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new NullReferenceException("Property bag key cannot be null or empty.");

            string propertyvalue = "";

            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(_webUrl))
                {
                    SPWebApplication webApp = site.WebApplication;
                    if (webApp.Properties.Contains(key))
                        propertyvalue = webApp.Properties[key].ToString();
                    else
                        throw new KeyNotFoundException("Property bag key not found.");
                }
            });

            return propertyvalue;
        }
    }
}
