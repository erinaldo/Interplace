<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmImpressaoEtiqueta.aspx.cs" Inherits="PlusImpressaoEtiqueta.frmImpressaoEtiqueta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title></title>
    <link href="main.css" rel="stylesheet">


        <script src="jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#imprimir").on("click", function () {
                $("#contentimprimir").load("frmImprimir.aspx?cnpj=" + $('#ctl00_ContentPlaceHolder1_TextBox1').val() + "&produto=" + $('#ctl00_ContentPlaceHolder1_TextBox2').val());
            });
            //frmImprimir.aspx?cnpj=34036601000419&produto=123456789
        });
    </script>


    

</head>
<body>

                    <div class="app-main__inner">
                        <div class="app-page-title">
                            <div class="page-title-wrapper">
                                <div class="page-title-heading">
                                    <div class="page-title-icon">
                                        <i class="pe-7s-drawer icon-gradient bg-happy-itmeo">
                                        </i>
                                    </div>
                                    <div>Impressão de Etiquetas
                                        <div class="page-title-subheading">Imprima e veja as notas faturadas e etiquetas criadas, impressas e não impressas
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
                                                        <span>
                                                            Inbox
                                                        </span>
                                                        <div class="ml-auto badge badge-pill badge-secondary">86</div>
                                                    </a>
                                                </li>
                                                <li class="nav-item">
                                                    <a href="javascript:void(0);" class="nav-link">
                                                        <i class="nav-link-icon lnr-book"></i>
                                                        <span>
                                                            Book
                                                        </span>
                                                        <div class="ml-auto badge badge-pill badge-danger">5</div>
                                                    </a>
                                                </li>
                                                <li class="nav-item">
                                                    <a href="javascript:void(0);" class="nav-link">
                                                        <i class="nav-link-icon lnr-picture"></i>
                                                        <span>
                                                            Picture
                                                        </span>
                                                    </a>
                                                </li>
                                                <li class="nav-item">
                                                    <a disabled href="javascript:void(0);" class="nav-link disabled">
                                                        <i class="nav-link-icon lnr-file-empty"></i>
                                                        <span>
                                                            File Disabled
                                                        </span>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>    </div>
                        </div>            
						<div class="tab-pane tabs-animation fade show active" id="tab-content-0" role="tabpanel">
                                <div class="main-card mb-3 card">
                                    <div class="card-body"><h5 class="card-title"Filtro</h5>
                                        <form id="form1" runat="server">
											
                                            <div class="form-row">
											    <input type="hidden" id="pagina" name="pagina" value="ImpressaoEtiqueta">
                                                <div class="col-md-2">
                                                    <div class="position-relative form-group"><label for="CNPJ" class="">CNPJ</label> </div>
                                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="position-relative form-group"><label for="chavenota" class="">Produto</label> </div>
                                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>						
										                     <a href="#" id="imprimir" class="btn-shadow  btn btn-info">
                                    <i class="metismenu-icon pe-7s-rocket"></i>Imprimir
                                </a>
                                        </form>
                                    </div>
                                </div>
                            </div>
						
						<div class="row">
                            <div class="col-lg-12">
                                <div class="main-card mb-3 card">
                                    <div class="card-body"><h5 class="card-title">Relatório</h5>
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
