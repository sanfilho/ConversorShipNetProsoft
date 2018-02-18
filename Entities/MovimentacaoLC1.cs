using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class MovimentacaoLC1
    {
        public string Tipo = "LC1";
        public int Ordem { get; set; }
        /// <summary>
        /// 3 Brancos
        /// </summary>
        public string Filler = string.Empty.PadLeft(3, ' ');
        public string ModoLancamento { get; set; }
        public DateTime DataEscrituracao { get; set; }
        public int NumeroDocumento { get; set; }
        public string NumeroLote { get; set; }
        public long OrigemLancamento { get; set; }
        public int QuantidadeContas { get; set; }
        public string ContaDebitoCodigoAcesso { get; set; }
        public string ContaDebitoTerceiro { get; set; }
        public string ContaDebitoCentroCusto { get; set; }
        public string ContaCreditoCodigoAcesso { get; set; }
        public string ContaCreditoTerceiro { get; set; }
        public string ContaCreditoCentroCusto { get; set; }
        public string ValorDocumento { get; set; }
        public string Historico { get; set; }
        public string IndicadorConciliacaoDeb { get; set; }
        public string IndicadorConciliacaoCred { get; set; }
        /// <summary>
        /// 74 Brancos
        /// </summary>
        public string Filler2 = string.Empty.PadLeft(74, ' ');
    }
}
