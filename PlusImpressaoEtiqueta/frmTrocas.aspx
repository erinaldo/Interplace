<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="frmTrocas.aspx.cs" Inherits="PlusImpressaoEtiqueta.frmTrocas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


                    <div class="app-main__inner">
                        <div class="app-page-title">
                            <div class="page-title-wrapper">
                                <div class="page-title-heading">
                                    <div class="page-title-icon">
                                        <i class="pe-7s-drawer icon-gradient bg-happy-itmeo">
                                        </i>
                                    </div>
                                    <div>Relatório de trocas B2W
                                        <div class="page-title-subheading">Veja as notas de trocas da B2W</div>
                                    </div>
                                </div>
                                  </div>
                        </div>            
						<div class="tab-pane tabs-animation fade show active" id="tab-content-0" role="tabpanel">
                                <div class="main-card mb-3 card">
                                    <div class="card-body"><h5 class="card-title"Filtro</h5>
                                        
											
                                            <div class="form-row">
											    <input type="hidden" id="pagina" name="pagina" value="ImpressaoEtiqueta">
                                                <div class="col-md-2">
                                                    <div class="position-relative form-group"><label for="CNPJ" class="">Data Inicial</label> </div>
                                                    
                                                    <asp:TextBox ID="dInicial" runat="server" TextMode="Date"  CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="position-relative form-group"><label for="chavenota" class="">Data Final</label> </div>
                                                    <asp:TextBox ID="dFinal" runat="server" TextMode="Date"  CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>	
                                        <asp:Button ID="btnVisualizar" runat="server" Text="Visualizar" CssClass="btn-shadow  btn btn-info" OnClick="btnVisualizar_Click" />					
                                    </div>
                                </div>
                            </div>
						
						<div class="row">
                            <div class="col-lg-12">
                                <div class="main-card mb-3 card">
                                    <div class="card-body"><h5 class="card-title">Relatório</h5>
                                        <asp:GridView ID="gridTrocas" runat="server" CssClass="mb-0 table" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundField DataField="PEDIDO" HeaderText="Pedido" />
                                                <asp:BoundField DataField="DATAPEDIDO" HeaderText="Data" />
                                                <asp:BoundField DataField="CPF" HeaderText="CPF" />
                                            </Columns>
                                        </asp:GridView>  
                                        
                                        

                                    </div>
                                </div>
                            </div>
                                  <div id="contentimprimir" class="app-main__outer">
        </div>
						
						</div>
                    </div>




</asp:Content>
