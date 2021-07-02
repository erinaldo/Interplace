<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmSeparacao.aspx.cs" Inherits="PlusImpressaoEtiqueta.frmSeparacao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="main.css" rel="stylesheet">


    <script src="https://cdnjs.cloudflare.com/ajax/libs/bluebird/3.3.5/bluebird.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="tab-pane tabs-animation fade show active" id="tab-content-0" role="tabpanel">
            <div class="main-card mb-3 card">
                <div class="card-body">
                    <h5 class="card-title">Relatório de separação</h5>

                            <input type="hidden" id="pagina" name="pagina" value="ImpressaoEtiqueta">
                            <asp:Button ID="btnImprimir" runat="server" CssClass="btn-shadow  btn btn-info" OnClick="btnImprimir_Click" Text="Imprimir Relatório" />
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
    </form>
</body>
</html>
