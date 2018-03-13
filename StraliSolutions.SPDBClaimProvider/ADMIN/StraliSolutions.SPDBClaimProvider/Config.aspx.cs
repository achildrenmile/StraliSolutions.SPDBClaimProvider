using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Administration;
using System.IO;
using System.Globalization;
using System.Security.Policy;

namespace StraliSolutions.SPDBClaimProvider
{
    public partial class Config : LayoutsPageBase
    {
        private void setProperty(string value, string Property)
        {

            SPWebApplication webApp = this.Selector.CurrentItem;

            if (webApp == null)
            {
                string msg = string.Format(CultureInfo.InvariantCulture, "Cannot find WebApplication by id '{0}'", this.Selector.CurrentId);

                throw new FileNotFoundException(msg);
            }
            try
            {
                webApp.Properties[Property] =
                    value;
                webApp.Update();
                this.LabelMessage.Text += "Property " + Property + " has been successfully stored. ";
            }
            catch (Exception ex)
            {
                this.LabelMessage.Text += "Error while storing properties. Message: " + ex.Message + " ";
            }
        }
        protected void DataConnection_Click(object sender, EventArgs e)
        {
            setProperty(this.DataConnection.Text, Constants.DC);
        }

        protected void SPTrustedIdentityTokenIssuerName_Click(object sender, EventArgs e)
        {
            setProperty(this.SPTrustedIdentityTokenIssuerName.Text, Constants.SPTRUSTEDIDENTITYTOKENISSUERNAME);
        }

        public void OnContextChange(object sender, EventArgs e)
        {
            SPWebApplication webApp = Selector.CurrentItem;

            try
            {
                if (webApp.Properties[Constants.DC] != null)
                    this.DataConnection.Text = webApp.Properties[Constants.DC].ToString();

                if (webApp.Properties[Constants.SPTRUSTEDIDENTITYTOKENISSUERNAME] != null)
                    this.SPTrustedIdentityTokenIssuerName.Text = webApp.Properties[Constants.SPTRUSTEDIDENTITYTOKENISSUERNAME].ToString();

            }
            catch { }

        }
        /// <summary>
        /// Raises the <see cref="E:Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.LabelMessage.Text = string.Empty;

            if (!this.Page.IsPostBack) this.OnContextChange(this, e);
        }
    }
}
