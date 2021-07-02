<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmImprimir.aspx.cs" Inherits="PlusImpressaoEtiqueta.frmImprimir" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script src="zip.js"></script>
    <script src="zip-ext.js"></script>
    <script src="deflate.js"></script>
    <script src="JSPrintManager.js"></script>
    <script src="jquery-3.6.0.min.js"></script>

</head>
<script>
    ////WebSocket settings
    //JSPM.JSPrintManager.auto_reconnect = true;
    //JSPM.JSPrintManager.start();

    ////JSPM.JSPrintManager.WS.onStatusChanged = function () {
    ////    if (jspmWSStatus()) {
    ////        //get client installed printers
    ////        JSPM.JSPrintManager.getPrinters().then(function (myPrinters) {
    ////            var options = '';
    ////            for (var i = 0; i < myPrinters.length; i++) {
    ////                options += '<option>' + myPrinters[i] + '</option>';
    ////            }
    ////            $('#installedPrinterName').html(options);

    ////            var url_string = window.location.href;
    ////            var url = new URL(url_string);
    ////            var sParametroImpressora = url.searchParams.get("installedPrinterName");
    ////            if (sParametroImpressora != null) {
    ////                $('#installedPrinterName').val(sParametroImpressora)
    ////            }
    ////        });



    ////    }
    ////};

    ////Check JSPM WebSocket status
    //function jspmWSStatus() {
    //    if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Open)
    //        return true;
    //    else if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Closed) {
    //        alert('JSPrintManager (JSPM) is not installed or not running! Download JSPM Client App from https://neodynamic.com/downloads/jspm');
    //        return false;
    //    }
    //    else if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Blocked) {
    //        alert('JSPM has blocked this website!');
    //        return false;
    //    }
    //}

    ////Do printing...
    //function print(sImpressora, sEtiqueta) {
    //    if (jspmWSStatus()) {
    //        //Create a ClientPrintJob
    //        var cpj = new JSPM.ClientPrintJob();
    //        //Set Printer type (Refer to the help, there many of them!)
    //        cpj.clientPrinter = new JSPM.InstalledPrinter(sImpressora);
    //        //Set content to print...
    //        //Create Zebra ZPL commands for sample label
    //        var cmds = sEtiqueta;
    //        cpj.printerCommands = cmds;
    //        //Send print job to printer!
    //        cpj.sendToClient();
    //    }
    //}

</script>







<body>
    <form id="form1" runat="server">
               <div >
        </div>
    </form>
</body>
</html>
