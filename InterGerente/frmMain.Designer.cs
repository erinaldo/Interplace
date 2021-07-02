namespace InterGerente
{
    partial class frmMain
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cadastroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clienteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.categoriasMLFPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.produtosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ferramentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelaProdutoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportaXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testeEtiquetaCorreioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geraRemessaBradescoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geralToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.relatóriosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iconApp = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRemessa = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.chkGNRE = new System.Windows.Forms.CheckBox();
            this.chkAtualizaProdutos = new System.Windows.Forms.CheckBox();
            this.chkEnviaProdutosNovos = new System.Windows.Forms.CheckBox();
            this.chkAtualizaProdutoB2W = new System.Windows.Forms.CheckBox();
            this.button6 = new System.Windows.Forms.Button();
            this.chkAvisoCancelado = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.chkEnviaNotaMAGALU = new System.Windows.Forms.CheckBox();
            this.chkPedidoMAGALU = new System.Windows.Forms.CheckBox();
            this.chkEnviaNotaB2W = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.chkDirect = new System.Windows.Forms.CheckBox();
            this.chkMensagem = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chkEnviaPedidoMLFortPlus = new System.Windows.Forms.CheckBox();
            this.chkGeraEtiquetaB2W = new System.Windows.Forms.CheckBox();
            this.chkGeraEtiquetaML = new System.Windows.Forms.CheckBox();
            this.chkBaixaXML = new System.Windows.Forms.CheckBox();
            this.chkEnviaProdutoML = new System.Windows.Forms.CheckBox();
            this.chkAtualizaProduto = new System.Windows.Forms.CheckBox();
            this.lblStatusMercadoLivre = new System.Windows.Forms.Label();
            this.lblStatusFortePlus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboFilial = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboEstacao = new System.Windows.Forms.ComboBox();
            this.barProgresso = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.editLog = new System.Windows.Forms.TextBox();
            this.btnRelatorioMargem = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastroToolStripMenuItem,
            this.ferramentasToolStripMenuItem,
            this.configurçõesToolStripMenuItem,
            this.relatóriosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(20, 60);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1355, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // cadastroToolStripMenuItem
            // 
            this.cadastroToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clienteToolStripMenuItem,
            this.categoriasMLFPToolStripMenuItem,
            this.produtosToolStripMenuItem});
            this.cadastroToolStripMenuItem.Name = "cadastroToolStripMenuItem";
            this.cadastroToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.cadastroToolStripMenuItem.Text = "Cadastro";
            this.cadastroToolStripMenuItem.Click += new System.EventHandler(this.CadastroToolStripMenuItem_Click);
            // 
            // clienteToolStripMenuItem
            // 
            this.clienteToolStripMenuItem.Name = "clienteToolStripMenuItem";
            this.clienteToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.clienteToolStripMenuItem.Text = "Cliente";
            this.clienteToolStripMenuItem.Click += new System.EventHandler(this.ClienteToolStripMenuItem_Click);
            // 
            // categoriasMLFPToolStripMenuItem
            // 
            this.categoriasMLFPToolStripMenuItem.Name = "categoriasMLFPToolStripMenuItem";
            this.categoriasMLFPToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.categoriasMLFPToolStripMenuItem.Text = "Categorias ML -> FP";
            this.categoriasMLFPToolStripMenuItem.Click += new System.EventHandler(this.CategoriasMLFPToolStripMenuItem_Click);
            // 
            // produtosToolStripMenuItem
            // 
            this.produtosToolStripMenuItem.Name = "produtosToolStripMenuItem";
            this.produtosToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.produtosToolStripMenuItem.Text = "Produtos";
            this.produtosToolStripMenuItem.Click += new System.EventHandler(this.ProdutosToolStripMenuItem_Click);
            // 
            // ferramentasToolStripMenuItem
            // 
            this.ferramentasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancelaProdutoToolStripMenuItem,
            this.exportaXMLToolStripMenuItem,
            this.testeEtiquetaCorreioToolStripMenuItem,
            this.geraRemessaBradescoToolStripMenuItem});
            this.ferramentasToolStripMenuItem.Name = "ferramentasToolStripMenuItem";
            this.ferramentasToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.ferramentasToolStripMenuItem.Text = "Ferramentas";
            // 
            // cancelaProdutoToolStripMenuItem
            // 
            this.cancelaProdutoToolStripMenuItem.Name = "cancelaProdutoToolStripMenuItem";
            this.cancelaProdutoToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.cancelaProdutoToolStripMenuItem.Text = "Cancela Produto";
            this.cancelaProdutoToolStripMenuItem.Click += new System.EventHandler(this.cancelaProdutoToolStripMenuItem_Click);
            // 
            // exportaXMLToolStripMenuItem
            // 
            this.exportaXMLToolStripMenuItem.Name = "exportaXMLToolStripMenuItem";
            this.exportaXMLToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.exportaXMLToolStripMenuItem.Text = "Exporta XML ";
            this.exportaXMLToolStripMenuItem.Click += new System.EventHandler(this.exportaXMLToolStripMenuItem_Click);
            // 
            // testeEtiquetaCorreioToolStripMenuItem
            // 
            this.testeEtiquetaCorreioToolStripMenuItem.Name = "testeEtiquetaCorreioToolStripMenuItem";
            this.testeEtiquetaCorreioToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.testeEtiquetaCorreioToolStripMenuItem.Text = "Teste Etiqueta Correio/jadlog";
            this.testeEtiquetaCorreioToolStripMenuItem.Click += new System.EventHandler(this.testeEtiquetaCorreioToolStripMenuItem_Click);
            // 
            // geraRemessaBradescoToolStripMenuItem
            // 
            this.geraRemessaBradescoToolStripMenuItem.Name = "geraRemessaBradescoToolStripMenuItem";
            this.geraRemessaBradescoToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.geraRemessaBradescoToolStripMenuItem.Text = "Gera Remessa Bradesco";
            this.geraRemessaBradescoToolStripMenuItem.Click += new System.EventHandler(this.geraRemessaBradescoToolStripMenuItem_Click);
            // 
            // configurçõesToolStripMenuItem
            // 
            this.configurçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.geralToolStripMenuItem});
            this.configurçõesToolStripMenuItem.Name = "configurçõesToolStripMenuItem";
            this.configurçõesToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.configurçõesToolStripMenuItem.Text = "Configurações";
            // 
            // geralToolStripMenuItem
            // 
            this.geralToolStripMenuItem.Name = "geralToolStripMenuItem";
            this.geralToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.geralToolStripMenuItem.Text = "Geral";
            this.geralToolStripMenuItem.Click += new System.EventHandler(this.GeralToolStripMenuItem_Click);
            // 
            // relatóriosToolStripMenuItem
            // 
            this.relatóriosToolStripMenuItem.Name = "relatóriosToolStripMenuItem";
            this.relatóriosToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.relatóriosToolStripMenuItem.Text = "Relatórios";
            this.relatóriosToolStripMenuItem.Click += new System.EventHandler(this.RelatóriosToolStripMenuItem_Click);
            // 
            // iconApp
            // 
            this.iconApp.Text = "iconApp";
            this.iconApp.Visible = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRelatorioMargem);
            this.panel1.Controls.Add(this.btnRemessa);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.chkGNRE);
            this.panel1.Controls.Add(this.chkAtualizaProdutos);
            this.panel1.Controls.Add(this.chkEnviaProdutosNovos);
            this.panel1.Controls.Add(this.chkAtualizaProdutoB2W);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.chkAvisoCancelado);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.chkEnviaNotaMAGALU);
            this.panel1.Controls.Add(this.chkPedidoMAGALU);
            this.panel1.Controls.Add(this.chkEnviaNotaB2W);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.chkDirect);
            this.panel1.Controls.Add(this.chkMensagem);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.chkEnviaPedidoMLFortPlus);
            this.panel1.Controls.Add(this.chkGeraEtiquetaB2W);
            this.panel1.Controls.Add(this.chkGeraEtiquetaML);
            this.panel1.Controls.Add(this.chkBaixaXML);
            this.panel1.Controls.Add(this.chkEnviaProdutoML);
            this.panel1.Controls.Add(this.chkAtualizaProduto);
            this.panel1.Controls.Add(this.lblStatusMercadoLivre);
            this.panel1.Controls.Add(this.lblStatusFortePlus);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboFilial);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.comboEstacao);
            this.panel1.Controls.Add(this.barProgresso);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 497);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1355, 129);
            this.panel1.TabIndex = 2;
            // 
            // btnRemessa
            // 
            this.btnRemessa.Location = new System.Drawing.Point(1167, 65);
            this.btnRemessa.Name = "btnRemessa";
            this.btnRemessa.Size = new System.Drawing.Size(152, 23);
            this.btnRemessa.TabIndex = 33;
            this.btnRemessa.Text = "Arquivo Remessa";
            this.btnRemessa.UseVisualStyleBackColor = true;
            this.btnRemessa.Click += new System.EventHandler(this.btnRemessa_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(843, 64);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(237, 23);
            this.button8.TabIndex = 32;
            this.button8.Text = "Relatorio de vendas por Produto";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(924, 36);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(156, 23);
            this.button7.TabIndex = 31;
            this.button7.Text = "Relatorio de vendas ML";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // chkGNRE
            // 
            this.chkGNRE.AutoSize = true;
            this.chkGNRE.Checked = true;
            this.chkGNRE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGNRE.Location = new System.Drawing.Point(630, 26);
            this.chkGNRE.Name = "chkGNRE";
            this.chkGNRE.Size = new System.Drawing.Size(83, 17);
            this.chkGNRE.TabIndex = 30;
            this.chkGNRE.Text = "Gera GNRE";
            this.chkGNRE.UseVisualStyleBackColor = true;
            // 
            // chkAtualizaProdutos
            // 
            this.chkAtualizaProdutos.AutoSize = true;
            this.chkAtualizaProdutos.Checked = true;
            this.chkAtualizaProdutos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtualizaProdutos.Location = new System.Drawing.Point(521, 26);
            this.chkAtualizaProdutos.Name = "chkAtualizaProdutos";
            this.chkAtualizaProdutos.Size = new System.Drawing.Size(103, 17);
            this.chkAtualizaProdutos.TabIndex = 29;
            this.chkAtualizaProdutos.Text = "Atualiza Produto";
            this.chkAtualizaProdutos.UseVisualStyleBackColor = true;
            // 
            // chkEnviaProdutosNovos
            // 
            this.chkEnviaProdutosNovos.AutoSize = true;
            this.chkEnviaProdutosNovos.Checked = true;
            this.chkEnviaProdutosNovos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnviaProdutosNovos.Location = new System.Drawing.Point(383, 26);
            this.chkEnviaProdutosNovos.Name = "chkEnviaProdutosNovos";
            this.chkEnviaProdutosNovos.Size = new System.Drawing.Size(132, 17);
            this.chkEnviaProdutosNovos.TabIndex = 28;
            this.chkEnviaProdutosNovos.Text = "Envia Novos Produtos";
            this.chkEnviaProdutosNovos.UseVisualStyleBackColor = true;
            // 
            // chkAtualizaProdutoB2W
            // 
            this.chkAtualizaProdutoB2W.AutoSize = true;
            this.chkAtualizaProdutoB2W.Checked = true;
            this.chkAtualizaProdutoB2W.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtualizaProdutoB2W.Location = new System.Drawing.Point(227, 26);
            this.chkAtualizaProdutoB2W.Name = "chkAtualizaProdutoB2W";
            this.chkAtualizaProdutoB2W.Size = new System.Drawing.Size(130, 17);
            this.chkAtualizaProdutoB2W.TabIndex = 27;
            this.chkAtualizaProdutoB2W.Text = "Atualiza Produto B2W";
            this.chkAtualizaProdutoB2W.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(843, 36);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 26;
            this.button6.Text = "JADLOG";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // chkAvisoCancelado
            // 
            this.chkAvisoCancelado.AutoSize = true;
            this.chkAvisoCancelado.Checked = true;
            this.chkAvisoCancelado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAvisoCancelado.Location = new System.Drawing.Point(23, 26);
            this.chkAvisoCancelado.Name = "chkAvisoCancelado";
            this.chkAvisoCancelado.Size = new System.Drawing.Size(198, 17);
            this.chkAvisoCancelado.TabIndex = 25;
            this.chkAvisoCancelado.Text = "Envia Mensagem Pedido Cancelado";
            this.chkAvisoCancelado.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1167, 94);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(152, 23);
            this.button5.TabIndex = 24;
            this.button5.Text = "Corrige chave primaria";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1086, 93);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 23;
            this.button4.Text = "Relatorios";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // chkEnviaNotaMAGALU
            // 
            this.chkEnviaNotaMAGALU.AutoSize = true;
            this.chkEnviaNotaMAGALU.Checked = true;
            this.chkEnviaNotaMAGALU.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnviaNotaMAGALU.Location = new System.Drawing.Point(1101, 3);
            this.chkEnviaNotaMAGALU.Name = "chkEnviaNotaMAGALU";
            this.chkEnviaNotaMAGALU.Size = new System.Drawing.Size(171, 17);
            this.chkEnviaNotaMAGALU.TabIndex = 22;
            this.chkEnviaNotaMAGALU.Text = "Envia Nota/Etiqueta MAGALU";
            this.chkEnviaNotaMAGALU.UseVisualStyleBackColor = true;
            // 
            // chkPedidoMAGALU
            // 
            this.chkPedidoMAGALU.AutoSize = true;
            this.chkPedidoMAGALU.Checked = true;
            this.chkPedidoMAGALU.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPedidoMAGALU.Location = new System.Drawing.Point(996, 3);
            this.chkPedidoMAGALU.Name = "chkPedidoMAGALU";
            this.chkPedidoMAGALU.Size = new System.Drawing.Size(99, 17);
            this.chkPedidoMAGALU.TabIndex = 21;
            this.chkPedidoMAGALU.Text = "MAGALU -> FP";
            this.chkPedidoMAGALU.UseVisualStyleBackColor = true;
            // 
            // chkEnviaNotaB2W
            // 
            this.chkEnviaNotaB2W.AutoSize = true;
            this.chkEnviaNotaB2W.Checked = true;
            this.chkEnviaNotaB2W.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnviaNotaB2W.Location = new System.Drawing.Point(884, 3);
            this.chkEnviaNotaB2W.Name = "chkEnviaNotaB2W";
            this.chkEnviaNotaB2W.Size = new System.Drawing.Size(106, 17);
            this.chkEnviaNotaB2W.TabIndex = 20;
            this.chkEnviaNotaB2W.Text = "Envia Nota B2W";
            this.chkEnviaNotaB2W.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1005, 93);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 19;
            this.button3.Text = "Teste Geral";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(924, 93);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "DIFAL";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chkDirect
            // 
            this.chkDirect.AutoSize = true;
            this.chkDirect.Checked = true;
            this.chkDirect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDirect.Location = new System.Drawing.Point(622, 3);
            this.chkDirect.Name = "chkDirect";
            this.chkDirect.Size = new System.Drawing.Size(94, 17);
            this.chkDirect.TabIndex = 16;
            this.chkDirect.Text = "Solicitar Direct";
            this.chkDirect.UseVisualStyleBackColor = true;
            // 
            // chkMensagem
            // 
            this.chkMensagem.AutoSize = true;
            this.chkMensagem.Checked = true;
            this.chkMensagem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMensagem.Location = new System.Drawing.Point(797, 3);
            this.chkMensagem.Name = "chkMensagem";
            this.chkMensagem.Size = new System.Drawing.Size(81, 17);
            this.chkMensagem.TabIndex = 15;
            this.chkMensagem.Text = "Mensagens";
            this.chkMensagem.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(843, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Comparar Estoque";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // chkEnviaPedidoMLFortPlus
            // 
            this.chkEnviaPedidoMLFortPlus.AutoSize = true;
            this.chkEnviaPedidoMLFortPlus.Checked = true;
            this.chkEnviaPedidoMLFortPlus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnviaPedidoMLFortPlus.Location = new System.Drawing.Point(722, 3);
            this.chkEnviaPedidoMLFortPlus.Name = "chkEnviaPedidoMLFortPlus";
            this.chkEnviaPedidoMLFortPlus.Size = new System.Drawing.Size(69, 17);
            this.chkEnviaPedidoMLFortPlus.TabIndex = 13;
            this.chkEnviaPedidoMLFortPlus.Text = "ML -> FP";
            this.chkEnviaPedidoMLFortPlus.UseVisualStyleBackColor = true;
            // 
            // chkGeraEtiquetaB2W
            // 
            this.chkGeraEtiquetaB2W.AutoSize = true;
            this.chkGeraEtiquetaB2W.Checked = true;
            this.chkGeraEtiquetaB2W.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGeraEtiquetaB2W.Location = new System.Drawing.Point(498, 3);
            this.chkGeraEtiquetaB2W.Name = "chkGeraEtiquetaB2W";
            this.chkGeraEtiquetaB2W.Size = new System.Drawing.Size(118, 17);
            this.chkGeraEtiquetaB2W.TabIndex = 12;
            this.chkGeraEtiquetaB2W.Text = "Gera Etiqueta B2W";
            this.chkGeraEtiquetaB2W.UseVisualStyleBackColor = true;
            // 
            // chkGeraEtiquetaML
            // 
            this.chkGeraEtiquetaML.AutoSize = true;
            this.chkGeraEtiquetaML.Checked = true;
            this.chkGeraEtiquetaML.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGeraEtiquetaML.Location = new System.Drawing.Point(383, 3);
            this.chkGeraEtiquetaML.Name = "chkGeraEtiquetaML";
            this.chkGeraEtiquetaML.Size = new System.Drawing.Size(109, 17);
            this.chkGeraEtiquetaML.TabIndex = 11;
            this.chkGeraEtiquetaML.Text = "Gera Etiqueta ML";
            this.chkGeraEtiquetaML.UseVisualStyleBackColor = true;
            // 
            // chkBaixaXML
            // 
            this.chkBaixaXML.AutoSize = true;
            this.chkBaixaXML.Checked = true;
            this.chkBaixaXML.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBaixaXML.Location = new System.Drawing.Point(123, 3);
            this.chkBaixaXML.Name = "chkBaixaXML";
            this.chkBaixaXML.Size = new System.Drawing.Size(77, 17);
            this.chkBaixaXML.TabIndex = 10;
            this.chkBaixaXML.Text = "Baixa XML";
            this.chkBaixaXML.UseVisualStyleBackColor = true;
            // 
            // chkEnviaProdutoML
            // 
            this.chkEnviaProdutoML.AutoSize = true;
            this.chkEnviaProdutoML.Checked = true;
            this.chkEnviaProdutoML.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnviaProdutoML.Location = new System.Drawing.Point(206, 3);
            this.chkEnviaProdutoML.Name = "chkEnviaProdutoML";
            this.chkEnviaProdutoML.Size = new System.Drawing.Size(180, 17);
            this.chkEnviaProdutoML.TabIndex = 9;
            this.chkEnviaProdutoML.Text = "Envia Produto/ Atualiza Estoque";
            this.chkEnviaProdutoML.UseVisualStyleBackColor = true;
            // 
            // chkAtualizaProduto
            // 
            this.chkAtualizaProduto.AutoSize = true;
            this.chkAtualizaProduto.Checked = true;
            this.chkAtualizaProduto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtualizaProduto.Location = new System.Drawing.Point(23, 3);
            this.chkAtualizaProduto.Name = "chkAtualizaProduto";
            this.chkAtualizaProduto.Size = new System.Drawing.Size(103, 17);
            this.chkAtualizaProduto.TabIndex = 8;
            this.chkAtualizaProduto.Text = "Atualiza Produto";
            this.chkAtualizaProduto.UseVisualStyleBackColor = true;
            // 
            // lblStatusMercadoLivre
            // 
            this.lblStatusMercadoLivre.AutoSize = true;
            this.lblStatusMercadoLivre.Location = new System.Drawing.Point(120, 79);
            this.lblStatusMercadoLivre.Name = "lblStatusMercadoLivre";
            this.lblStatusMercadoLivre.Size = new System.Drawing.Size(35, 13);
            this.lblStatusMercadoLivre.TabIndex = 7;
            this.lblStatusMercadoLivre.Text = "label4";
            // 
            // lblStatusFortePlus
            // 
            this.lblStatusFortePlus.AutoSize = true;
            this.lblStatusFortePlus.Location = new System.Drawing.Point(20, 79);
            this.lblStatusFortePlus.Name = "lblStatusFortePlus";
            this.lblStatusFortePlus.Size = new System.Drawing.Size(35, 13);
            this.lblStatusFortePlus.TabIndex = 6;
            this.lblStatusFortePlus.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(604, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Filial:";
            // 
            // comboFilial
            // 
            this.comboFilial.FormattingEnabled = true;
            this.comboFilial.Items.AddRange(new object[] {
            "34.036.601/0001-76 - \t2ELETRO MATRIZ\t",
            "34.036.601/0002-57 - 2ELETRO ATACADISTA\t",
            "34.036.601/0003-38 - 2ELETRO VAREJISTA"});
            this.comboFilial.Location = new System.Drawing.Point(640, 95);
            this.comboFilial.Name = "comboFilial";
            this.comboFilial.Size = new System.Drawing.Size(178, 21);
            this.comboFilial.TabIndex = 4;
            this.comboFilial.SelectedIndexChanged += new System.EventHandler(this.ComboFilial_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(310, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tipo de Estação:";
            // 
            // comboEstacao
            // 
            this.comboEstacao.FormattingEnabled = true;
            this.comboEstacao.Items.AddRange(new object[] {
            "Servidor",
            "Terminal"});
            this.comboEstacao.Location = new System.Drawing.Point(404, 96);
            this.comboEstacao.Name = "comboEstacao";
            this.comboEstacao.Size = new System.Drawing.Size(178, 21);
            this.comboEstacao.TabIndex = 2;
            this.comboEstacao.SelectedIndexChanged += new System.EventHandler(this.ComboEstacao_SelectedIndexChanged);
            // 
            // barProgresso
            // 
            this.barProgresso.Location = new System.Drawing.Point(82, 95);
            this.barProgresso.Name = "barProgresso";
            this.barProgresso.Size = new System.Drawing.Size(222, 23);
            this.barProgresso.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Progresso:";
            // 
            // editLog
            // 
            this.editLog.Dock = System.Windows.Forms.DockStyle.Right;
            this.editLog.Location = new System.Drawing.Point(1026, 84);
            this.editLog.Multiline = true;
            this.editLog.Name = "editLog";
            this.editLog.Size = new System.Drawing.Size(349, 413);
            this.editLog.TabIndex = 4;
            // 
            // btnRelatorioMargem
            // 
            this.btnRelatorioMargem.Location = new System.Drawing.Point(1167, 36);
            this.btnRelatorioMargem.Name = "btnRelatorioMargem";
            this.btnRelatorioMargem.Size = new System.Drawing.Size(152, 23);
            this.btnRelatorioMargem.TabIndex = 34;
            this.btnRelatorioMargem.Text = "Relatório Margem";
            this.btnRelatorioMargem.UseVisualStyleBackColor = true;
            this.btnRelatorioMargem.Click += new System.EventHandler(this.btnRelatorioMargem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1395, 646);
            this.Controls.Add(this.editLog);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Interplace - Integrador MarketPlace";
            this.TransparencyKey = System.Drawing.Color.Empty;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cadastroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clienteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geralToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon iconApp;
        private System.Windows.Forms.ToolStripMenuItem categoriasMLFPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem produtosToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboEstacao;
        private System.Windows.Forms.ProgressBar barProgresso;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboFilial;
        private System.Windows.Forms.Label lblStatusMercadoLivre;
        private System.Windows.Forms.Label lblStatusFortePlus;
        private System.Windows.Forms.CheckBox chkEnviaProdutoML;
        private System.Windows.Forms.CheckBox chkAtualizaProduto;
        private System.Windows.Forms.CheckBox chkGeraEtiquetaML;
        private System.Windows.Forms.CheckBox chkBaixaXML;
        private System.Windows.Forms.CheckBox chkGeraEtiquetaB2W;
        private System.Windows.Forms.CheckBox chkEnviaPedidoMLFortPlus;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkMensagem;
        private System.Windows.Forms.ToolStripMenuItem relatóriosToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkDirect;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox editLog;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox chkEnviaNotaB2W;
        private System.Windows.Forms.CheckBox chkPedidoMAGALU;
        private System.Windows.Forms.CheckBox chkEnviaNotaMAGALU;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.CheckBox chkAvisoCancelado;
        private System.Windows.Forms.ToolStripMenuItem ferramentasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelaProdutoToolStripMenuItem;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.CheckBox chkAtualizaProdutoB2W;
        private System.Windows.Forms.CheckBox chkEnviaProdutosNovos;
        private System.Windows.Forms.CheckBox chkAtualizaProdutos;
        private System.Windows.Forms.CheckBox chkGNRE;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.ToolStripMenuItem exportaXMLToolStripMenuItem;
        private System.Windows.Forms.Button btnRemessa;
        private System.Windows.Forms.ToolStripMenuItem testeEtiquetaCorreioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geraRemessaBradescoToolStripMenuItem;
        private System.Windows.Forms.Button btnRelatorioMargem;
    }
}

