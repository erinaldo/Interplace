<%@ Register TagPrefix="dx" Namespace="DevExpress.DashboardWeb" Assembly="DevExpress.Dashboard.v20.2.Web.WebForms, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %><%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Default.aspx.cs" Inherits="Default" %>


<%@ Register Src="~/UserControls/Widget.ascx" TagPrefix="demo" TagName="Widget" %>
<%@ Register Src="~/UserControls/WidgetsContainer.ascx" TagPrefix="demo" TagName="WidgetsContainer" %>


<asp:Content runat="server" ContentPlaceHolderID="head">
    <asp:PlaceHolder runat="server">
        <script src='<%= Page.ResolveUrl("~/Content/dashboard.js")%>' defer></script>
    </asp:PlaceHolder>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="mainContent">
  


<dx:ASPxDashboard runat="server" ID="dashGeral" ColorScheme="dark" WorkingMode="Viewer" DashboardXmlPath="~/App_Data/CustoProduto.xml" OnDashboardLoading="dashGeral_DashboardLoading" OnLoad="dashGeral_Load"></dx:ASPxDashboard>






</asp:Content>