<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="frmConfiguracao.aspx.cs" Inherits="PlusImpressaoEtiqueta.frmConfiguracao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<html>
    <head>
<link href="main.css" rel="stylesheet">



<script src="assets/scripts/zip.js"></script>
<!--<script src="assets/zip-ext.js"></script>-->
<script src="assets/scripts/deflate.js"></script>
<script src="assets/scripts/JSPrintManager.js"></script>
 
<script src="https://cdnjs.cloudflare.com/ajax/libs/bluebird/3.3.5/bluebird.min.js"></script>
<script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>



<script>

    //WebSocket settings
    JSPM.JSPrintManager.auto_reconnect = true;
    JSPM.JSPrintManager.start();

    JSPM.JSPrintManager.WS.onStatusChanged = function () {
        if (jspmWSStatus()) {
            //get client installed printers
            JSPM.JSPrintManager.getPrinters().then(function (myPrinters) {
                var options = '';
                for (var i = 0; i < myPrinters.length; i++) {
                    options += '<option>' + myPrinters[i] + '</option>';
                }
                $('#installedPrinterName').html(options);

                var url_string = window.location.href;
                var url = new URL(url_string);
                var sParametroImpressora = url.searchParams.get("installedPrinterName");
                if (sParametroImpressora != null) {
                    $('#installedPrinterName').val(sParametroImpressora)
                }
            });



        }
    };

    //Check JSPM WebSocket status
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

    //Do printing...
    function print(o) {
        if (jspmWSStatus()) {
            //Create a ClientPrintJob
            var cpj = new JSPM.ClientPrintJob();
            //Set Printer type (Refer to the help, there many of them!)
            if ($('#useDefaultPrinter').prop('checked')) {
                cpj.clientPrinter = new JSPM.DefaultPrinter();
            } else {
                cpj.clientPrinter = new JSPM.InstalledPrinter($('#installedPrinterName').val());
            }
            //Set content to print...
            //Create Zebra ZPL commands for sample label

            var sX = 50;

            var cmds = "^XA";
            cmds += "^FO" + (20 + sX).toString() + ",30^GB750,1100,4^FS";
            cmds += "^FO" + (20 + sX).toString() + ",30^GB750,200,4^FS";
            cmds += "^FO" + (20 + sX).toString() + ",30^GB750,400,4^FS";
            cmds += "^FO" + (20 + sX).toString() + ",30^GB750,700,4^FS";
            cmds += "^FO" + (20 + sX).toString() + ",226^GB325,204,4^FS";
            cmds += "^FO" + (30 + sX).toString() + ",40^ADN,36,20^FDShip to:^FS";
            cmds += "^FO" + (30 + sX).toString() + ",260^ADN,18,10^FDPart number #^FS";
            cmds += "^FO" + (360 + sX).toString() + ",260^ADN,18,10^FDDescription:^FS";
            cmds += "^FO" + (30 + sX).toString() + ",750^ADN,36,20^FDFrom:^FS";
            cmds += "^FO" + (150 + sX).toString() + ",125^ADN,36,20^FDAcme Printing^FS";
            cmds += "^FO" + (60 + sX).toString() + ",330^ADN,36,20^FD14042^FS";
            cmds += "^FO" + (400 + sX).toString() + ",330^ADN,36,20^FDScrew^FS";
            cmds += "^FO" + (70 + sX).toString() + ",480^BY4^B3N,,200^FD12345678^FS";
            cmds += "^FO" + (150 + sX).toString() + ",800^ADN,36,20^FDMacks Fabricating^FS";
            cmds += "^XZ";
            cpj.printerCommands = cmds;
            //Send print job to printer!
            cpj.sendToClient();
        }
    }

</script>

</head>
<body>
        <div>
            						<div class="tab-pane tabs-animation fade show active" id="tab-content-0" role="tabpanel">
                                <div class="main-card mb-3 card">
                                    <div class="card-body"><h5 class="card-title">Filtro</h5>
                                        </div>
											
											<div class="form-row">
											    <input type="hidden" id="pagina" name="pagina" value="Configuracao">
                                                <div class="col-md-6">
													  <div class="position-relative form-group">
													  <label for="installedPrinterName" class =" " />
                                                          <select name="installedPrinterName" id="installedPrinterName"></select>
                                                          <%--<asp:DropDownList ID="installedPrinterName" runat="server"></asp:DropDownList>--%>
                                                      </div>
                                                </div>
                                            </div>			
                                            <asp:Button ID="btnSalvar" runat="server" Text="Salvar"  CssClass="mt-2 btn btn-primary" OnClick="btnSalvar_Click"  />
                                    </div>
                                </div>
                            </div>

    <button onclick="print();" formaction="#">Teste Impressão</button>




       
</body>
</html>



    </label>



</asp:Content>


