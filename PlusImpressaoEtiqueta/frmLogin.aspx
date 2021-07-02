<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="PlusImpressaoEtiqueta.frmLogin" %>

<link rel="stylesheet" href="boostrap.min.css" >
<link rel="stylesheet" href="main.css" >

<div class="form-group d-flex justify-content-center">
    <img src="img/logo2eletro.png" width="100px" alt="Fuzzy Cardigan"
        class="img-thumbnail img-responsive">
</div>

<div class="container ">

    <form id="form1" runat="server">

        <div class="form-group">
            <div class="col-md-6 offset-md-3">
                <label>Usuário</label>
                <asp:TextBox runat="server" ID="editLogin" CssClass="form-control" placeholder="Usuario"></asp:TextBox>
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-6 offset-md-3">
                <label>SENHA </label>
                <asp:TextBox runat="server" ID="editSenha" CssClass="form-control" placeholder="Senha" TextMode="Password"></asp:TextBox>
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-6 offset-md-3">
                <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary" Text="Login" OnClick="btnLogin_Click" />
            </div>
            <div class="col-md-6 offset-md-3">
                <asp:Label ID="lblStatus" runat="server" ForeColor="Red" Text=""></asp:Label>
            </div>
        </div>
    </form>
</div>
