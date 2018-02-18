using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class MovimentacaoLC2
    {
        public string Tipo = "LC2";
        public string CentroCusto { get; set; }
        public string CodigoAcesso { get; set; }
        public string ComPartidaNumero { get; set; }
        public string DebitoCredito { get; set; }
        public string Historico { get; set; }
        public string IndicadorConciliacao { get; set; }
        public int Ordem { get; set; }
        public string Terceiro { get; set; }
        public string Valor { get; set; }
        public string Filler = string.Empty.PadLeft(49,' ');
    }
}
