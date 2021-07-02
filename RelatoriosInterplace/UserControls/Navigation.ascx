<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Navigation.ascx.cs" Inherits="Navigation" %>

<dx:BootstrapTreeView runat="server" OnNodeClick="Unnamed1_NodeClick">
    <CssClasses
        IconExpandNode="demo-icon demo-icon-col m-0"
        IconCollapseNode="demo-icon demo-icon-ex m-0"
        NodeList="demo-treeview-nodes m-0" Node="demo-treeview-node" Control="demo-treeview" />
    <Nodes>
        <dx:BootstrapTreeViewNode Text="Custo" NavigateUrl="../Default.aspx?dash=Custo"></dx:BootstrapTreeViewNode>
    </Nodes>
</dx:BootstrapTreeView>