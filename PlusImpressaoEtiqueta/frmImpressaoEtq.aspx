<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="frmImpressaoEtq.aspx.cs" Inherits="PlusImpressaoEtiqueta.frmImpressaoEtq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!DOCTYPE html>

    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title></title>
        <link href="main.css" rel="stylesheet">
<%--        <script src="assets/scripts/JSPrintManager.js"></script>
        <script src="assets/scripts/zip.js"></script>
        <script src="assets/scripts/zip-ext.js"></script>
        <script src="assets/scripts/deflate.js"></script>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bluebird/3.3.5/bluebird.min.js"></script>
        <script src="assets/scripts/jquery-3.6.0.min.js"></script>--%>
        <script src="assets/scripts/JSPrintManager.js"></script>
        <script src="assets/scripts/zip.js"></script>
        <script src="assets/scripts/zip-ext.js"></script>
        <script src="assets/scripts/deflate.js"></script>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bluebird/3.3.5/bluebird.min.js"></script>

        <script type="text/javascript">
            JSPM.JSPrintManager.start();
            JSPM.JSPrintManager.auto_reconnect = true;
            JSPM.JSPrintManager.WS.onStatusChanged = function () {
                if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Open) {
                    alert("Sistema iniciado");
                };
            }
        </script>
        <script type="text/javascript">
            //JSPM.JSPrintManager.auto_reconnect = true;
            //JSPM.JSPrintManager.start();
            //JSPM.JSPrintManager.WS.onStatusChanged = function () {
            //    if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Open) {
            //        jQuery("body").css("visibility", "visible");
            //        jQuery("body").css("display", "none");
            //        jQuery("body").fadeIn(1200);
            //    };
                function jspmWSStatus() {
                    if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Open)
                        return true;
                    else if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Closed) {
                        alert('JSPrintManager (JSPM) is not installed or not running! Download JSPM Client App from https://neodynamic.com/downloads/jspm');
                        return false;
                    }
                    else if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Blocked) {
                        alert('JSPM has blocked this website!');
                        return false;
                    }
                }

                function print(sImpressora, sEtiqueta) {
                    if (jspmWSStatus()) {
                        var cpj = new JSPM.ClientPrintJob();
                        cpj.clientPrinter = new JSPM.InstalledPrinter(sImpressora);
                        var cmds = sEtiqueta;
                        cpj.printerCommands = cmds;
                        cpj.sendToClient();
                    }
                }
        </script>


        <script type="text/javascript">

                $('editEtiqueta').on('change', function () {
                    print($('#ctl00$ContentPlaceHolder1$editEtiqueta').val(), $('#ctl00$ContentPlaceHolder1$editImpressora').val());
                });


            $('ctl00$ContentPlaceHolder1$editProduto').on('keypress', function (e) {
                if (e.which == 13) {
                    document.getElementById('ctl00_ContentPlaceHolder1_btnIprime').click();
                }
            });

                //jQuery("body").css("display", "none");
        </script>
    </head>
    <body>

        <div class="app-main__inner">
            <div class="app-page-title">
                <div class="page-title-wrapper">
                    <div class="page-title-heading">
                        <div class="page-title-icon">
                            <i class="pe-7s-drawer icon-gradient bg-happy-itmeo"></i>
                        </div>
                        <div>
                            Impressão de Etiquetas
                                        <div class="page-title-subheading">
                                            Imprima e veja as notas faturadas e etiquetas criadas, impressas e não impressas
                                        </div>
                        </div>
                    </div>
                    <div class="page-title-actions">
                        <button type="button" data-toggle="tooltip" title="Example Tooltip" data-placement="bottom" class="btn-shadow mr-3 btn btn-dark">
                            <i class="fa fa-star"></i>
                        </button>
                        <div class="d-inline-block dropdown">
                            <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-shadow dropdown-toggle btn btn-info">
                                <span class="btn-icon-wrapper pr-2 opacity-7">
                                    <i class="fa fa-business-time fa-w-20"></i>
                                </span>
                                Buttons</button>
                            <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu dropdown-menu-right">
                                <ul class="nav flex-column">
                                    <li class="nav-item">
                                        <a href="javascript:void(0);" class="nav-link">
                                            <i class="nav-link-icon lnr-inbox"></i>
                                            <span>Inbox
                                            </span>
                                            <div class="ml-auto badge badge-pill badge-secondary">86</div>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a href="javascript:void(0);" class="nav-link">
                                            <i class="nav-link-icon lnr-book"></i>
                                            <span>Book
                                            </span>
                                            <div class="ml-auto badge badge-pill badge-danger">5</div>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a href="javascript:void(0);" class="nav-link">
                                            <i class="nav-link-icon lnr-picture"></i>
                                            <span>Picture
                                            </span>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a disabled href="javascript:void(0);" class="nav-link disabled">
                                            <i class="nav-link-icon lnr-file-empty"></i>
                                            <span>File Disabled
                                            </span>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="tab-pane tabs-animation fade show active" id="tab-content-0" role="tabpanel">

                        <div class="main-card mb-3 card">
                            <div class="card-body">
                                <h5 class="card-title">Filtro</h5>

                                <div class="form-row">
                                    <input type="hidden" id="pagina" name="pagina" value="ImpressaoEtiqueta">
                                    <div class="col-md-2">
                                        <div class="position-relative form-group">
                                            <label for="CNPJ" class="">CNPJ</label>
                                        </div>
                                        <asp:TextBox ID="editCNPJ" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="position-relative form-group">
                                            <label for="chavenota" class="">Produto</label>
                                        </div>
                                        <asp:TextBox ID="editProduto" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="position-relative form-group">
                                            <label for="chavenota" class="">Etiqueta</label>
                                        </div>
                                        <asp:TextBox ID="editEtiqueta" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="position-relative form-group">
                                            <label for="chavenota" class="">Impressora</label>
                                        </div>
                                        <asp:TextBox ID="editImpressora" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:Button ID="btnIprime" runat="server" Text="Imprimir" CssClass="btn-shadow  btn btn-info" OnClick="btnIprime_Click" />



                            </div>
                        </div>
                    </div>
                </ContentTemplate>



            </asp:UpdatePanel>

            <div class="row">
                <div class="col-lg-12">
                    <div class="main-card mb-3 card">
                        <div class="card-body">
                            <h5 class="card-title">Relatório</h5>
                            <table class="mb-0 table">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Nota</th>
                                        <th>Chave Nota</th>
                                        <th>Impresso</th>
                                        <th>Pedido</th>
                                        <th>Loja</th>
                                        <th>Data Impressão</th>
                                        <th>Criada</th>
                                        <th>Status</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div id="contentimprimir" class="app-main__outer">
                </div>

            </div>
        </div>

    </body>
    </html>
</asp:Content>
