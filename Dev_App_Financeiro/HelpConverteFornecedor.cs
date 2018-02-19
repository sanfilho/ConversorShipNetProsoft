using Dev_App_Financeiro.Properties;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace Dev_App_Financeiro
{
    partial class HelpConverteFornecedor : Form
    {
        public HelpConverteFornecedor()
        {
            InitializeComponent();
            textBoxDescription.Text = Resources.HelpConverteFornecedor;
        }
    }
}
