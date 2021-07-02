<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="frmSeparacaoProduto.aspx.cs" Inherits="PlusImpressaoEtiqueta.frmSeparacaoProduto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <html>
    <head>
        <link href="main.css" rel="stylesheet">


        <script src="https://cdnjs.cloudflare.com/ajax/libs/bluebird/3.3.5/bluebird.min.js"></script>
        <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>

        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title></title>
    </head>
    <body>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"></asp:UpdatePanel>
        <contenttemplate>
        <div class="tab-pane tabs-animation fade show active" id="tab-content-0" role="tabpanel">
            <div class="main-card mb-3 card">
                <div class="card-body">
                    <h5 class="card-title">Relatório de separação</h5>

                            <input type="hidden" id="pagina" name="pagina" value="ImpressaoEtiqueta">

                               <div class="col-md-2">
                                        <div class="position-relative form-group">
                                            <label for="CNPJ" class="">CNPJ</label>
                                        </div>
                                        <asp:TextBox ID="editCNPJ" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                            <asp:Button ID="btnImprimir" runat="server" CssClass="btn-shadow  btn btn-info"  Text="Imprimir Relatório" OnClick="btnImprimir_Click" />
                </div>
            </div>
        </div>


        <div>
            <asp:GridView ID="gridSeparacao" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="NOTA" HeaderText="Nota" />
                    <asp:BoundField DataField="CHAVENOTA" HeaderText="Chave">
                        <HeaderStyle Width="400px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CNPJ" HeaderText="CNPJ" />
                    <asp:TemplateField HeaderText="Separar?">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkRow" Height="40px" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>






        </div>

</contenttemplate>

    </body>
    </html>


</asp:Content>
