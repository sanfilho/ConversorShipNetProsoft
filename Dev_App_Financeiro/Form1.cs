using Importer;
using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Dev_App_Financeiro
{
    public partial class Form1 : Form
    {
        private readonly ILog log = LogManager.GetLogger(typeof(Form1));
        public Form1()
        {
            XmlConfigurator.Configure();
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;

//            var userName = WindowsIdentity.GetCurrent().Name;

//          log.Info(string.Format("Usuário {0} iniciou a aplicação",userName));
        }

        private void btnBuscarPais_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtArquivoPais.Text = openFileDialog1.FileName;
            }
        }
        private void btnBuscaFornecedor_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtArquivoFornecedor.Text = openFileDialog1.FileName;
            }
        }

        private void btnBuscaCentroCusto_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtArquivoCentroCusto.Text = openFileDialog1.FileName;
            }
        }

        private void btnBuscaMovimentacao_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtArquivoMovimentacao.Text = openFileDialog1.FileName;
            }
        }

        private void btnBuscaFornecedor2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtArquivoFornecedor2.Text = openFileDialog1.FileName;
            }

        }

        private void btnCarregarMovimentacao_Click(object sender, EventArgs e)
        {
            var folder = new FolderBrowserDialog();

            if (folder.ShowDialog() == DialogResult.OK)
            {
                DisableEnableControls(false);
                backgroundWorker1.RunWorkerAsync(new string[] { "Movimentacao", folder.SelectedPath});// { "Movimentacao", ""}  );
            }            
        }

        private void btnCarregarFornecedor_Click(object sender, EventArgs e)
        {
            var folder = new FolderBrowserDialog();

            if (folder.ShowDialog() == DialogResult.OK)
            {
                DisableEnableControls(false);
                backgroundWorker1.RunWorkerAsync(new string[] { "Fornecedor", folder.SelectedPath });
            }
        }

        private void brnBuscaMovimentacao2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtArquivoMovimentacao2.Text = openFileDialog1.FileName;
            }

        }

        /// <summary>
        /// Processo assincrono para conversão dos arquivos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            backgroundWorker1.ReportProgress(1);

            if (((string[])e.Argument)[0] == "Fornecedor")
            {
                CarregarFornecedor(((string[])e.Argument)[1]);
            }
            else if (((string[])e.Argument)[0] == "Movimentacao")
            {
                CarregarMovimentacao(((string[])e.Argument)[1]);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lblNomeArquivo.Text = string.Format("Processado {0}%", e.ProgressPercentage);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                throw e.Error;
            }
            else
            {
                MessageBox.Show("Conversão finalizada.");
                log.Info(string.Format("Usuário {0} finalizou a conversão", WindowsIdentity.GetCurrent().Name));
                DisableEnableControls(true);
            }
        }

        private void DisableEnableControls(bool enabled)
        {
            txtArquivoPais.Enabled = enabled;
            btnBuscarPais.Enabled = enabled;
            txtArquivoMovimentacao.Enabled = enabled;
            btnBuscaMovimentacao.Enabled = enabled;
            txtArquivoFornecedor.Enabled = enabled;
            btnBuscaFornecedor.Enabled = enabled;
            btnCarregarFornecedor.Enabled = enabled;

            txtArquivoCentroCusto.Enabled = enabled;
            btnBuscaCentroCusto.Enabled = enabled;
            txtArquivoFornecedor2.Enabled = enabled;
            btnBuscaFornecedor2.Enabled = enabled;
            txtArquivoMovimentacao2.Enabled = enabled;
            brnBuscaMovimentacao2.Enabled = enabled;
            btnCarregarMovimentacao.Enabled = enabled;


        }

        private void CarregarFornecedor(string folderPath)
        {
            var settingsFornecedor = new XmlDocument();
            settingsFornecedor.Load("SettingsFornecedor.xml");

            var settingsPais = new XmlDocument();
            settingsPais.Load("SettingsPais.xml");

            var settingsMovimentacao = new XmlDocument();
            settingsMovimentacao.Load("SettingsMovimentacao.xml");

            var wkbFornecedor = new FornecedorWorksheetImporter(txtArquivoFornecedor.Text, Convert.ToInt32(settingsFornecedor.DocumentElement.Attributes["Sheet"].Value));

            var wkbPais = new PaisWorksheetImporter(txtArquivoPais.Text, Convert.ToInt32(settingsPais.DocumentElement.Attributes["Sheet"].Value));

            var wkbMovimentacao = new MovimentacaoWorksheetImporter(txtArquivoMovimentacao.Text, Convert.ToInt32(settingsMovimentacao.DocumentElement.Attributes["Sheet"].Value));

            var excelReader = wkbFornecedor.CarregarFornecedor(settingsFornecedor, settingsPais, settingsMovimentacao, wkbPais, wkbMovimentacao, backgroundWorker1);

            var dateTicks = DateTime.Now.Ticks;

            using (var outFile = new StreamWriter(folderPath + @"\FORNECEDOR_" + dateTicks + ".txt", true, Encoding.Default))
            {
                var total = 0;
                foreach (var item in excelReader)
                {
                    total++;
                    outFile.WriteLine(item);
                }

                lblNomeArquivo.Invoke(new Action(() => lblNomeArquivo.Text = "Convertidos " + total + " registros de fornecedores de um total de " + total + " fornecedores encontrados. \n\n  O arquivo de FORNECEDORES foi convertido em: \n\n" + ((FileStream)outFile.BaseStream).Name));
            }
        }

        private void CarregarMovimentacao(string folderPath) {
            var wkbMovimentacao = new MovimentacaoWorksheetImporter(txtArquivoMovimentacao2.Text, 0);

            var settingsFornecedor = new XmlDocument();
            settingsFornecedor.Load("SettingsFornecedor.xml");

            var settingsCentroCusto = new XmlDocument();
            settingsCentroCusto.Load("SettingsCentroCusto.xml");

            var settingsMovimentacao = new XmlDocument();
            settingsMovimentacao.Load("SettingsMovimentacao.xml");

            var wkbFornecedor = new FornecedorWorksheetImporter(txtArquivoFornecedor2.Text, Convert.ToInt32(settingsFornecedor.DocumentElement.Attributes["Sheet"].Value));
            var wkbCentroCusto = new CentroCustoWorksheetImporter(txtArquivoCentroCusto.Text, Convert.ToInt32(settingsCentroCusto.DocumentElement.Attributes["Sheet"].Value));

            var excelReader = wkbMovimentacao.CarregarMovimentacao(settingsMovimentacao, settingsFornecedor, settingsCentroCusto, wkbFornecedor, wkbCentroCusto, ref backgroundWorker1);

            using (var outFile = new StreamWriter(folderPath +  @"\CTBLCTOS0001_" + DateTime.Now.Ticks + ".txt", true))
            {
                var total = 0;
                foreach (var item in excelReader)
                {
                    total++;
                    outFile.WriteLine(item);
                }

                lblNomeArquivo.Invoke(new Action(() => lblNomeArquivo.Text = "Convertidos " + total + " registros de movimentações de um total de " + total + " movimentações encontradas. \n\n  O arquivo de MOVIMENTAÇÕES foi convertido em: \n\n " + ((FileStream)outFile.BaseStream).Name));
            }

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblNomeArquivo.Text = "";
        }

        private void icoHelpConverteFornecedor_Click(object sender, EventArgs e)
        {
            new HelpConverteFornecedor().ShowDialog();
        }

        private void icoHelpConverteFornecedor_MouseHover(object sender, EventArgs e)
        {
            var toolTip = new ToolTip();
            toolTip.SetToolTip(icoHelpConverteFornecedor, "Regras de Negócio");
        }

        private void Form1_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new AboutBox1().ShowDialog();
            e.Cancel = true;
        }

        private void icoHelpConverteMovimentacao_Click(object sender, EventArgs e)
        {
            new HelpConverteMovimentacao().ShowDialog();
        }

        private void icoHelpConverteMovimentacao_MouseHover(object sender, EventArgs e)
        {
            var toolTip = new ToolTip();
            toolTip.SetToolTip(icoHelpConverteMovimentacao, "Regras de Negócio");
        }
    }
}