<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" Src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" Src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" Src="~/_controltemplates/ButtonSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Config.aspx.cs" Inherits="StraliSolutions.SPDBClaimProvider.Config" DynamicMasterPageFile="~masterurl/default.master" %>


<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
<table width="100%" class="propertysheet" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td class="ms-descriptionText">
                <asp:Label ID="LabelMessage" runat="server" EnableViewState="False" class="ms-descriptionText" />
            </td>
        </tr>
        <tr>
            <td class="ms-error">
                <asp:Label ID="LabelErrorMessage" runat="server" EnableViewState="False" />
            </td>
        </tr>
    </table>
    <wssuc:ToolBar runat="server" ID="ToolBar" CssClass="ms-toolbar">
        <Template_RightButtons>
            <SharePoint:WebApplicationSelector ID="Selector" runat="server" TypeLabelCssClass="ms-listheaderlabel"
                OnContextChange="OnContextChange" />
        </Template_RightButtons>
    </wssuc:ToolBar>
    <table border="0" cellspacing="0" cellpadding="0" class="ms-propertysheet" width="98%">
        <wssuc:InputFormSection runat="server" title="DataConnection" description="Data connection to ClaimProivder DB.">
            <template_inputformcontrols>
			    <tr>
                    <td class="ms-authoringcontrols">
                        <table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="ms-authoringcontrols" nowrap="nowrap" colspan="2"><span>DataConnection:</span></td>
                                </tr>
                                <tr>
                                    <td class="ms-authoringcontrols" width="99%">
                                        <asp:TextBox runat="server" CssClass="ms-input"  
                                         ID="DataConnection" ToolTip="DataConnection" maxLength="512" size="80" />
                                    </td>
                              
                                    <td>
                                        <asp:Button UseSubmitBehavior="false" runat="server" class="ms-ButtonHeightWidth"
                                            OnClick="DataConnection_Click" Text="Set Data Connection"
                                            ID="DataConnectionBtnSubmit" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
			        </td>
                </tr>
		    </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:InputFormSection runat="server" title="SPTrustedIdentityTokenIssuerName" description="This is the same value returned from: Get-SPTrustedIdentityTokenIssuer | select Name.">
            <template_inputformcontrols>
			    <tr>
                    <td class="ms-authoringcontrols">
                        <table class="ms-authoringcontrols" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="ms-authoringcontrols" nowrap="nowrap" colspan="2"><span> SPTrustedIdentityTokenIssuerName:</span></td>
                                </tr>
                                <tr>
                                    <td class="ms-authoringcontrols" width="99%">
                                        <asp:TextBox runat="server" CssClass="ms-input"  
                                         ID="SPTrustedIdentityTokenIssuerName" ToolTip=" SPTrustedIdentityTokenIssuerName" maxLength="512" size="80" />
                                    </td>
                              
                                    <td>
                                        <asp:Button UseSubmitBehavior="false" runat="server" class="ms-ButtonHeightWidth"
                                            OnClick="SPTrustedIdentityTokenIssuerName_Click" Text="Set Token"
                                            ID="SPTrustedIdentityTokenIssuerNameBtnSubmit" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
			        </td>
                </tr>
		    </template_inputformcontrols>
        </wssuc:InputFormSection>
        
    </table>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Database Claim Provider Configuration
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Database Claim Provider Configuration
</asp:Content>
