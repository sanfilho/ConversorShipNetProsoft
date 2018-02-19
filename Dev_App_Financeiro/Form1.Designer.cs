namespace Dev_App_Financeiro
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnCarregarMovimentacao = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnBuscaCentroCusto = new System.Windows.Forms.Button();
            this.txtArquivoCentroCusto = new System.Windows.Forms.TextBox();
            this.lblCentroCusto = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.icoHelpConverteFornecedor = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtArquivoPais = new System.Windows.Forms.TextBox();
            this.lblPais = new System.Windows.Forms.Label();
            this.btnBuscaMovimentacao = new System.Windows.Forms.Button();
            this.txtArquivoMovimentacao = new System.Windows.Forms.TextBox();
            this.btnBuscarPais = new System.Windows.Forms.Button();
            this.lblMovimentacao = new System.Windows.Forms.Label();
            this.btnBuscaFornecedor = new System.Windows.Forms.Button();
            this.btnCarregarFornecedor = new System.Windows.Forms.Button();
            this.lblFornecedor = new System.Windows.Forms.Label();
            this.txtArquivoFornecedor = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.icoHelpConverteMovimentacao = new System.Windows.Forms.PictureBox();
            this.brnBuscaMovimentacao2 = new System.Windows.Forms.Button();
            this.txtArquivoMovimentacao2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBuscaFornecedor2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtArquivoFornecedor2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNomeArquivo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icoHelpConverteFornecedor)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icoHelpConverteMovimentacao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnCarregarMovimentacao
            // 
            this.btnCarregarMovimentacao.AutoSize = true;
            this.btnCarregarMovimentacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCarregarMovimentacao.Location = new System.Drawing.Point(333, 313);
            this.btnCarregarMovimentacao.Name = "btnCarregarMovimentacao";
            this.btnCarregarMovimentacao.Size = new System.Drawing.Size(184, 26);
            this.btnCarregarMovimentacao.TabIndex = 2;
            this.btnCarregarMovimentacao.Text = "Carregar Movimentação";
            this.btnCarregarMovimentacao.UseVisualStyleBackColor = true;
            this.btnCarregarMovimentacao.Click += new System.EventHandler(this.btnCarregarMovimentacao_Click);
            // 
            // btnBuscaCentroCusto
            // 
            this.btnBuscaCentroCusto.Location = new System.Drawing.Point(468, 169);
            this.btnBuscaCentroCusto.Name = "btnBuscaCentroCusto";
            this.btnBuscaCentroCusto.Size = new System.Drawing.Size(46, 23);
            this.btnBuscaCentroCusto.TabIndex = 5;
            this.btnBuscaCentroCusto.Text = "...";
            this.btnBuscaCentroCusto.UseVisualStyleBackColor = true;
            this.btnBuscaCentroCusto.Click += new System.EventHandler(this.btnBuscaCentroCusto_Click);
            // 
            // txtArquivoCentroCusto
            // 
            this.txtArquivoCentroCusto.Location = new System.Drawing.Point(6, 169);
            this.txtArquivoCentroCusto.Name = "txtArquivoCentroCusto";
            this.txtArquivoCentroCusto.Size = new System.Drawing.Size(456, 22);
            this.txtArquivoCentroCusto.TabIndex = 4;
            // 
            // lblCentroCusto
            // 
            this.lblCentroCusto.AutoSize = true;
            this.lblCentroCusto.Location = new System.Drawing.Point(6, 150);
            this.lblCentroCusto.Name = "lblCentroCusto";
            this.lblCentroCusto.Size = new System.Drawing.Size(110, 16);
            this.lblCentroCusto.TabIndex = 9;
            this.lblCentroCusto.Text = "Centros de Custo";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 506);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(534, 23);
            this.progressBar1.TabIndex = 11;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(108, 35);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(394, 31);
            this.lblTitulo.TabIndex = 16;
            this.lblTitulo.Text = "Conversor Shipnet para Prosoft";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 112);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(538, 388);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.icoHelpConverteFornecedor);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtArquivoPais);
            this.tabPage1.Controls.Add(this.lblPais);
            this.tabPage1.Controls.Add(this.btnBuscaMovimentacao);
            this.tabPage1.Controls.Add(this.txtArquivoMovimentacao);
            this.tabPage1.Controls.Add(this.btnBuscarPais);
            this.tabPage1.Controls.Add(this.lblMovimentacao);
            this.tabPage1.Controls.Add(this.btnBuscaFornecedor);
            this.tabPage1.Controls.Add(this.btnCarregarFornecedor);
            this.tabPage1.Controls.Add(this.lblFornecedor);
            this.tabPage1.Controls.Add(this.txtArquivoFornecedor);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(530, 359);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Fornecedores";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // icoHelpConverteFornecedor
            // 
            this.icoHelpConverteFornecedor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.icoHelpConverteFornecedor.Image = global::Dev_App_Financeiro.Properties.Resources.ico_help;
            this.icoHelpConverteFornecedor.Location = new System.Drawing.Point(510, 6);
            this.icoHelpConverteFornecedor.Name = "icoHelpConverteFornecedor";
            this.icoHelpConverteFornecedor.Size = new System.Drawing.Size(14, 14);
            this.icoHelpConverteFornecedor.TabIndex = 16;
            this.icoHelpConverteFornecedor.TabStop = false;
            this.icoHelpConverteFornecedor.Click += new System.EventHandler(this.icoHelpConverteFornecedor_Click);
            this.icoHelpConverteFornecedor.MouseHover += new System.EventHandler(this.icoHelpConverteFornecedor_MouseHover);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(457, 96);
            this.label1.TabIndex = 15;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // txtArquivoPais
            // 
            this.txtArquivoPais.Location = new System.Drawing.Point(6, 169);
            this.txtArquivoPais.Name = "txtArquivoPais";
            this.txtArquivoPais.Size = new System.Drawing.Size(456, 22);
            this.txtArquivoPais.TabIndex = 13;
            // 
            // lblPais
            // 
            this.lblPais.AutoSize = true;
            this.lblPais.Location = new System.Drawing.Point(6, 150);
            this.lblPais.Name = "lblPais";
            this.lblPais.Size = new System.Drawing.Size(35, 16);
            this.lblPais.TabIndex = 12;
            this.lblPais.Text = "País";
            // 
            // btnBuscaMovimentacao
            // 
            this.btnBuscaMovimentacao.Location = new System.Drawing.Point(468, 215);
            this.btnBuscaMovimentacao.Name = "btnBuscaMovimentacao";
            this.btnBuscaMovimentacao.Size = new System.Drawing.Size(46, 23);
            this.btnBuscaMovimentacao.TabIndex = 1;
            this.btnBuscaMovimentacao.Text = "...";
            this.btnBuscaMovimentacao.UseVisualStyleBackColor = true;
            this.btnBuscaMovimentacao.Click += new System.EventHandler(this.btnBuscaMovimentacao_Click);
            // 
            // txtArquivoMovimentacao
            // 
            this.txtArquivoMovimentacao.Location = new System.Drawing.Point(6, 215);
            this.txtArquivoMovimentacao.Name = "txtArquivoMovimentacao";
            this.txtArquivoMovimentacao.Size = new System.Drawing.Size(456, 22);
            this.txtArquivoMovimentacao.TabIndex = 0;
            // 
            // btnBuscarPais
            // 
            this.btnBuscarPais.Location = new System.Drawing.Point(468, 169);
            this.btnBuscarPais.Name = "btnBuscarPais";
            this.btnBuscarPais.Size = new System.Drawing.Size(46, 23);
            this.btnBuscarPais.TabIndex = 14;
            this.btnBuscarPais.Text = "...";
            this.btnBuscarPais.UseVisualStyleBackColor = true;
            this.btnBuscarPais.Click += new System.EventHandler(this.btnBuscarPais_Click);
            // 
            // lblMovimentacao
            // 
            this.lblMovimentacao.AutoSize = true;
            this.lblMovimentacao.Location = new System.Drawing.Point(3, 199);
            this.lblMovimentacao.Name = "lblMovimentacao";
            this.lblMovimentacao.Size = new System.Drawing.Size(104, 16);
            this.lblMovimentacao.TabIndex = 3;
            this.lblMovimentacao.Text = "Movimentações";
            // 
            // btnBuscaFornecedor
            // 
            this.btnBuscaFornecedor.Location = new System.Drawing.Point(468, 256);
            this.btnBuscaFornecedor.Name = "btnBuscaFornecedor";
            this.btnBuscaFornecedor.Size = new System.Drawing.Size(46, 23);
            this.btnBuscaFornecedor.TabIndex = 8;
            this.btnBuscaFornecedor.Text = "...";
            this.btnBuscaFornecedor.UseVisualStyleBackColor = true;
            this.btnBuscaFornecedor.Click += new System.EventHandler(this.btnBuscaFornecedor_Click);
            // 
            // btnCarregarFornecedor
            // 
            this.btnCarregarFornecedor.AutoSize = true;
            this.btnCarregarFornecedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCarregarFornecedor.Location = new System.Drawing.Point(333, 313);
            this.btnCarregarFornecedor.Name = "btnCarregarFornecedor";
            this.btnCarregarFornecedor.Size = new System.Drawing.Size(181, 26);
            this.btnCarregarFornecedor.TabIndex = 10;
            this.btnCarregarFornecedor.Text = "Carregar Fornecedor";
            this.btnCarregarFornecedor.UseVisualStyleBackColor = true;
            this.btnCarregarFornecedor.Click += new System.EventHandler(this.btnCarregarFornecedor_Click);
            // 
            // lblFornecedor
            // 
            this.lblFornecedor.AutoSize = true;
            this.lblFornecedor.Location = new System.Drawing.Point(6, 240);
            this.lblFornecedor.Name = "lblFornecedor";
            this.lblFornecedor.Size = new System.Drawing.Size(93, 16);
            this.lblFornecedor.TabIndex = 6;
            this.lblFornecedor.Text = "Fornecedores";
            // 
            // txtArquivoFornecedor
            // 
            this.txtArquivoFornecedor.Location = new System.Drawing.Point(6, 256);
            this.txtArquivoFornecedor.Name = "txtArquivoFornecedor";
            this.txtArquivoFornecedor.Size = new System.Drawing.Size(456, 22);
            this.txtArquivoFornecedor.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.icoHelpConverteMovimentacao);
            this.tabPage2.Controls.Add(this.brnBuscaMovimentacao2);
            this.tabPage2.Controls.Add(this.txtArquivoMovimentacao2);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.btnBuscaFornecedor2);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.txtArquivoFornecedor2);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.lblCentroCusto);
            this.tabPage2.Controls.Add(this.btnCarregarMovimentacao);
            this.tabPage2.Controls.Add(this.txtArquivoCentroCusto);
            this.tabPage2.Controls.Add(this.btnBuscaCentroCusto);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(530, 359);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Movimentações";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // icoHelpConverteMovimentacao
            // 
            this.icoHelpConverteMovimentacao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.icoHelpConverteMovimentacao.Image = global::Dev_App_Financeiro.Properties.Resources.ico_help;
            this.icoHelpConverteMovimentacao.Location = new System.Drawing.Point(510, 6);
            this.icoHelpConverteMovimentacao.Name = "icoHelpConverteMovimentacao";
            this.icoHelpConverteMovimentacao.Size = new System.Drawing.Size(14, 14);
            this.icoHelpConverteMovimentacao.TabIndex = 23;
            this.icoHelpConverteMovimentacao.TabStop = false;
            this.icoHelpConverteMovimentacao.Click += new System.EventHandler(this.icoHelpConverteMovimentacao_Click);
            this.icoHelpConverteMovimentacao.MouseHover += new System.EventHandler(this.icoHelpConverteMovimentacao_MouseHover);
            // 
            // brnBuscaMovimentacao2
            // 
            this.brnBuscaMovimentacao2.Location = new System.Drawing.Point(468, 255);
            this.brnBuscaMovimentacao2.Name = "brnBuscaMovimentacao2";
            this.brnBuscaMovimentacao2.Size = new System.Drawing.Size(46, 23);
            this.brnBuscaMovimentacao2.TabIndex = 18;
            this.brnBuscaMovimentacao2.Text = "...";
            this.brnBuscaMovimentacao2.UseVisualStyleBackColor = true;
            this.brnBuscaMovimentacao2.Click += new System.EventHandler(this.brnBuscaMovimentacao2_Click);
            // 
            // txtArquivoMovimentacao2
            // 
            this.txtArquivoMovimentacao2.Location = new System.Drawing.Point(6, 256);
            this.txtArquivoMovimentacao2.Name = "txtArquivoMovimentacao2";
            this.txtArquivoMovimentacao2.Size = new System.Drawing.Size(456, 22);
            this.txtArquivoMovimentacao2.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 19;
            this.label3.Text = "Movimentações";
            // 
            // btnBuscaFornecedor2
            // 
            this.btnBuscaFornecedor2.Location = new System.Drawing.Point(468, 215);
            this.btnBuscaFornecedor2.Name = "btnBuscaFornecedor2";
            this.btnBuscaFornecedor2.Size = new System.Drawing.Size(46, 23);
            this.btnBuscaFornecedor2.TabIndex = 22;
            this.btnBuscaFornecedor2.Text = "...";
            this.btnBuscaFornecedor2.UseVisualStyleBackColor = true;
            this.btnBuscaFornecedor2.Click += new System.EventHandler(this.btnBuscaFornecedor2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 16);
            this.label4.TabIndex = 20;
            this.label4.Text = "Fornecedores";
            // 
            // txtArquivoFornecedor2
            // 
            this.txtArquivoFornecedor2.Location = new System.Drawing.Point(6, 215);
            this.txtArquivoFornecedor2.Name = "txtArquivoFornecedor2";
            this.txtArquivoFornecedor2.Size = new System.Drawing.Size(456, 22);
            this.txtArquivoFornecedor2.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(468, 96);
            this.label2.TabIndex = 16;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // lblNomeArquivo
            // 
            this.lblNomeArquivo.AutoSize = true;
            this.lblNomeArquivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeArquivo.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblNomeArquivo.Location = new System.Drawing.Point(13, 538);
            this.lblNomeArquivo.Name = "lblNomeArquivo";
            this.lblNomeArquivo.Size = new System.Drawing.Size(119, 16);
            this.lblNomeArquivo.TabIndex = 20;
            this.lblNomeArquivo.Text = "lblNomeArquivo";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Dev_App_Financeiro.Properties.Resources.LOGO_El_Cano_solo_banderas_;
            this.pictureBox1.Location = new System.Drawing.Point(12, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 61);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(559, 621);
            this.Controls.Add(this.lblNomeArquivo);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.progressBar1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Conversor ShipNet para Prosoft";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form1_HelpButtonClicked);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icoHelpConverteFornecedor)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icoHelpConverteMovimentacao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnCarregarMovimentacao;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnBuscaCentroCusto;
        private System.Windows.Forms.TextBox txtArquivoCentroCusto;
        private System.Windows.Forms.Label lblCentroCusto;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtArquivoPais;
        private System.Windows.Forms.Label lblPais;
        private System.Windows.Forms.Button btnBuscaMovimentacao;
        private System.Windows.Forms.TextBox txtArquivoMovimentacao;
        private System.Windows.Forms.Button btnBuscarPais;
        private System.Windows.Forms.Label lblMovimentacao;
        private System.Windows.Forms.Button btnBuscaFornecedor;
        private System.Windows.Forms.Button btnCarregarFornecedor;
        private System.Windows.Forms.Label lblFornecedor;
        private System.Windows.Forms.TextBox txtArquivoFornecedor;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button brnBuscaMovimentacao2;
        private System.Windows.Forms.TextBox txtArquivoMovimentacao2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBuscaFornecedor2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtArquivoFornecedor2;
        private System.Windows.Forms.Label lblNomeArquivo;
        private System.Windows.Forms.PictureBox icoHelpConverteFornecedor;
        private System.Windows.Forms.PictureBox icoHelpConverteMovimentacao;
    }
}

