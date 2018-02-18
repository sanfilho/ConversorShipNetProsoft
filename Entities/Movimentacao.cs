using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public enum CreditoDebito
    {
        Credito,
        Debito
    }

    public class Movimentacao
    {
        public string DataEscrituracao { get; set; }

        public string NumeroDocumento { get; set; }

        public string CodigoAcesso { get; set; }

        public string Terceiro { get; set; }

        public string Navio { get; set; }

        public string Departamento { get; set; }

        public string Valor { get; set; }

        public string Historico { get; set; }

        public CreditoDebito eCreditoDebito{
            get{
                try
                {
                    if (Convert.ToDecimal(Valor) > 0)
                    {
                        return CreditoDebito.Debito;
                    }

                    return CreditoDebito.Credito;
                }
                catch (Exception)
                {
                    Valor = "0";
                    return CreditoDebito.Credito;
                }

            } }
    }
}