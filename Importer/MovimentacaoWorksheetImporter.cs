using System;
using System.Collections.Generic;
using Entities;
using System.IO;
using System.Xml;
using System.Text;
using System.Linq;
using System.Globalization;
using System.ComponentModel;

namespace Importer
{
    public class MovimentacaoWorksheetImporter : WorksheetImporter
    {
        private List<StringBuilder> lstStbRegistros = new List<StringBuilder>();

        private List<MovimentacaoLC1> lstMovimentacaoLC1 = new List<MovimentacaoLC1>();
        private List<MovimentacaoLC2> lstMovimentacaoLC2 = new List<MovimentacaoLC2>();

        private List<Fornecedor> lstFornecedor = new List<Fornecedor>();
        private List<CentroCusto> lstCentroCusto = new List<CentroCusto>();
        private List<Movimentacao> lstMovimentacao = new List<Movimentacao>();

        private string ContaCompensacao;
        private string HistoricoCompensacao;

        private string ContaGeralDebito;
        private string ContaGeralCredito;

        private string ContaDeParaOrigem;
        private string ContaDeParaDestino;


        private int intOrdemLC1 = 0;

        public MovimentacaoWorksheetImporter(string filePath, int currentWorksheet) : base(filePath, currentWorksheet)
        {
            MaxErrors = 50;
        }

        /// <summary>
        /// Método de Validação para importação de arquivo de Movimentação
        /// </summary>
        /// <param name="listManager">Parâmetro recebido que representa a lista de movimentações</param>
        /// <returns>Retorna para o método ValidateWorksheet para validar as informações da mesma</returns>
        public override WorksheetValidationResult ValidateWorksheet()
        {
            var result = new WorksheetValidationResult();

            var excelReader = GetExcelReader();

            return excelReader.ValidationResult;
        }

        /// <summary>
        /// Converte a planilha de movimentação em string obedecendo a regra de layout
        /// </summary>
        /// <returns>Retorna a string no formato deo layout</returns>
        public List<StringBuilder> CarregarMovimentacao(XmlDocument settingsMovimentacao, XmlDocument settingsFornecedor, XmlDocument settingsCentroCusto, FornecedorWorksheetImporter wkbFornecedor, CentroCustoWorksheetImporter wkbCentroCusto, ref BackgroundWorker bgworker)
        {
            lstFornecedor = wkbFornecedor.ConverterWorksheet<Fornecedor>(settingsFornecedor);
            lstCentroCusto = wkbCentroCusto.ConverterWorksheet<CentroCusto>(settingsCentroCusto);
            lstMovimentacao = ConverterWorksheet<Movimentacao>(settingsMovimentacao);

            ContaCompensacao = settingsMovimentacao.DocumentElement.Attributes["ContaCompensacao"].Value;
            HistoricoCompensacao = settingsMovimentacao.DocumentElement.Attributes["HistoricoCompensacao"].Value;

            ContaGeralDebito = settingsMovimentacao.DocumentElement.Attributes["ContaGeralDebito"].Value;
            ContaGeralCredito = settingsMovimentacao.DocumentElement.Attributes["ContaGeralCredito"].Value;
            ContaDeParaOrigem = settingsMovimentacao.DocumentElement.Attributes["ContaDeParaOrigem"].Value;
            ContaDeParaDestino = settingsMovimentacao.DocumentElement.Attributes["ContaDeParaDestino"].Value;

            var lstDocumentosProcessados = new List<string>();

            var progress = 0;

            var lstMovimentacaoSemZerados = lstMovimentacao.Where(p => !string.IsNullOrEmpty(p.Valor) && (Convert.ToDecimal(p.Valor).ToString(CultureInfo.CreateSpecificCulture("en-US")) != "0.00" && Convert.ToDecimal(p.Valor).ToString(CultureInfo.CreateSpecificCulture("en-US")) != "0")).ToList();
            var countMovimentosSemZerados = lstMovimentacaoSemZerados.Count;

            for (int i = 0; i < countMovimentosSemZerados; i++)
            {
                progress = Convert.ToInt32((Convert.ToDouble(i) / Convert.ToDouble(countMovimentosSemZerados)) * 100);
                bgworker.ReportProgress(progress);

                var item = lstMovimentacaoSemZerados[i];

                if (string.IsNullOrEmpty(item.NumeroDocumento))
                {
                    continue;
                }

                if (lstDocumentosProcessados.Where(p => p == item.NumeroDocumento).Count() > 0)
                {
                    continue;
                }

                var stbRegistro = new StringBuilder();

                var lstDefineTipo = lstMovimentacaoSemZerados.Where(p => p.NumeroDocumento == item.NumeroDocumento);

                if (lstDefineTipo.Count() <= 2)
                {
                    //Gera movimentos de LC1
                    GerarMovimentosLC1(lstDefineTipo);
                }
                else if (lstDefineTipo.Count() > 200)
                {
                    //REALIZAR O PROCESSO DE TRATAMENTO DE REGISTROS COM MAIS DE 200 LANÇAMENTOS POR movimentação
                    GerarMovimentosCompensacao(lstDefineTipo);
                }
                else
                {
                    //Gera movimentos de LC1
                    GerarMovimentosLC2(lstDefineTipo);
                }

                lstDocumentosProcessados.Add(item.NumeroDocumento);

            }

            foreach (var item in lstMovimentacaoLC1)
            {
                var QtdeZerados = 0;

                if (item.ModoLancamento == "2")
                {
                    foreach (var item2 in lstMovimentacaoLC2.Where(p => p.Ordem == item.Ordem))
                        if (item2.Valor == "0.00" || item2.Valor == "0")
                        {
                            QtdeZerados++;
                        }
                }

                item.QuantidadeContas = item.QuantidadeContas - QtdeZerados;

                if (item.QuantidadeContas > 0 || item.ModoLancamento == "1")
                {

                    var stbRegister = new StringBuilder();

                    //Preenche a primeira linha com LC1
                    stbRegister.Append(item.Tipo);
                    stbRegister.Append(string.Format("{0:00000}", item.Ordem));
                    stbRegister.Append(item.Filler);
                    stbRegister.Append(item.ModoLancamento);
                    stbRegister.Append(item.DataEscrituracao.ToString("ddMMyyyy"));
                    stbRegister.Append(string.Format("{0:0000000000}", item.NumeroDocumento));
                    stbRegister.Append(item.NumeroLote);
                    stbRegister.Append(string.Format("{0:000000000000000000000000000000}", item.OrigemLancamento));
                    stbRegister.Append(string.Format("{0:000}", item.QuantidadeContas));
                    stbRegister.Append(item.ContaDebitoCodigoAcesso ?? "     ");
                    stbRegister.Append(item.ContaDebitoTerceiro ?? "              ");
                    stbRegister.Append(item.ContaDebitoCentroCusto ?? "     ");
                    stbRegister.Append(item.ContaCreditoCodigoAcesso ?? "     ");
                    stbRegister.Append(string.IsNullOrEmpty(item.ContaCreditoTerceiro) ? string.Empty.PadLeft(14, ' ') : item.ContaCreditoTerceiro.PadRight(14, ' '));
                    stbRegister.Append(item.ContaCreditoCentroCusto ?? "     ");
                    stbRegister.Append(string.IsNullOrEmpty(item.ValorDocumento) ? string.Empty.PadLeft(16,' ') : item.ValorDocumento.PadLeft(16, '0'));
                    stbRegister.Append(item.Historico.PadRight(240, ' '));

                    stbRegister.Append(item.IndicadorConciliacaoDeb ?? " ");
                    stbRegister.Append(item.IndicadorConciliacaoCred ?? " ");
                    stbRegister.Append(item.Filler2);


                    lstStbRegistros.Add(stbRegister);

                    if (item.ModoLancamento == "2")
                    {
                        var Sequencial = 0;
                        foreach (var item2 in lstMovimentacaoLC2.Where(p => p.Ordem == item.Ordem))
                        {
                            if (item2.Valor != "0.00")
                            {
                                var stbRegister2 = new StringBuilder();

                                stbRegister2.Append(item2.Tipo);
                                stbRegister2.Append(string.Format("{0:00000}", item2.Ordem));
                                stbRegister2.Append(string.Format("{0:000}", ++Sequencial));
                                stbRegister2.Append(item2.DebitoCredito);
                                stbRegister2.Append(item2.CodigoAcesso);
                                stbRegister2.Append(string.IsNullOrEmpty(item2.Terceiro) ? "              " : item2.Terceiro.PadRight(14, ' '));
                                stbRegister2.Append(string.IsNullOrEmpty(item2.CentroCusto) ? "     " : item2.CentroCusto);
                                stbRegister2.Append(item2.Valor.PadLeft(16, '0'));
                                stbRegister2.Append(item2.Historico.PadRight(240, ' '));
                                stbRegister2.Append(item2.IndicadorConciliacao);

                                stbRegister2.Append(item2.Filler);

                                lstStbRegistros.Add(stbRegister2);
                            }
                        }
                    }
                }
            }

            return lstStbRegistros;
        }

        private void GerarMovimentosLC1(IEnumerable<Movimentacao> lstDefineTipo)
        {
            var movimentacaoLC1 = new MovimentacaoLC1();
            movimentacaoLC1.Ordem = ++intOrdemLC1;

            foreach (var item in lstDefineTipo)
            {
                movimentacaoLC1.ModoLancamento = "1";
                movimentacaoLC1.DataEscrituracao = Convert.ToDateTime(item.DataEscrituracao);
                movimentacaoLC1.NumeroDocumento = Convert.ToInt32(item.NumeroDocumento);
                movimentacaoLC1.NumeroLote = "00001";
                movimentacaoLC1.OrigemLancamento = DateTime.Now.Ticks;
                movimentacaoLC1.Historico = string.IsNullOrEmpty(item.Historico) ? "" : item.Historico.Replace("\r\n", " ");

                movimentacaoLC1.ValorDocumento = Convert.ToDecimal(item.Valor).ToString(CultureInfo.CreateSpecificCulture("en-US"));
                movimentacaoLC1.IndicadorConciliacaoDeb = " ";
                movimentacaoLC1.IndicadorConciliacaoCred = " ";
                //movimentacaoLC1.ValorDocumento = item.Valor;

                switch (item.eCreditoDebito)
                {
                    case CreditoDebito.Credito:
                        movimentacaoLC1.ContaCreditoCodigoAcesso = item.CodigoAcesso;

                        var findTerceiro = lstFornecedor.Find(p => p.Apelido == item.Terceiro);

                        if (findTerceiro != null)
                        {
                            if (!string.IsNullOrEmpty(findTerceiro.CodigoPais) && findTerceiro.CodigoPais != "BR")
                            {
                                item.Terceiro = findTerceiro.Apelido.Replace(" ", "");

                                item.Terceiro = item.Terceiro.Length == 6 ? item.Terceiro + "L" : item.Terceiro;
                                item.Terceiro = item.Terceiro.PadLeft(14, ' ').Substring(0, 14);
                            }
                            else
                            {
                                if (findTerceiro.Personalidade == "0") //Juridica
                                {
                                    item.Terceiro = findTerceiro.CNPJCPFLivre.Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "").PadLeft(14, '0').Substring(0, 14);
                                }
                                else //FISICA
                                {
                                    item.Terceiro = findTerceiro.CNPJCPFLivre.Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "").PadLeft(14, ' ').Substring(0, 14);
                                }
                            }
                        }
                        else
                        {
                            item.Terceiro = "";
                        }

                        if (string.IsNullOrEmpty(movimentacaoLC1.ContaCreditoCodigoAcesso))
                        {
                            movimentacaoLC1.ContaCreditoCodigoAcesso = ContaGeralCredito;
                        }
                        //Se o centro de custo for um dos Centro de Custos que deve ser substituidos
                        else if (ContaDeParaOrigem.Contains(movimentacaoLC1.ContaCreditoCodigoAcesso))
                        {
                            movimentacaoLC1.ContaCreditoCodigoAcesso = ContaDeParaDestino;
                        }

                        movimentacaoLC1.ContaCreditoTerceiro = item.Terceiro;
                        //movimentacaoLC1.ContaCreditoCentroCusto = item.Navio + item.Departamento;
                        movimentacaoLC1.ValorDocumento = (Convert.ToDecimal(item.Valor) * (-1)).ToString(CultureInfo.CreateSpecificCulture("en-US"));
                        break;
                    case CreditoDebito.Debito:
                        movimentacaoLC1.ContaDebitoCodigoAcesso = item.CodigoAcesso;
                        //movimentacaoLC1.ContaDebitoTerceiro = item.Terceiro;

                        if (!string.IsNullOrEmpty(item.Navio) && !string.IsNullOrEmpty(item.Departamento))
                        {
                            try
                            {
                                var Navio = lstCentroCusto.FirstOrDefault(p => p.Navio == item.Navio);
                                var Departamento = lstCentroCusto.FirstOrDefault(p => p.Departamento.Contains(item.Departamento));

                                if (Navio != null && Departamento != null)
                                {
                                    movimentacaoLC1.ContaDebitoCentroCusto = Navio.CCNavio + Departamento.CCDepartamento;
                                    movimentacaoLC1.ContaDebitoCentroCusto = movimentacaoLC1.ContaDebitoCentroCusto.Replace("`", "");
                                }
                                else
                                {
                                    movimentacaoLC1.ContaDebitoCentroCusto = null;
                                }
                            }
                            catch (Exception)
                            {
                                movimentacaoLC1.ContaDebitoCentroCusto = null;
                            }
                        }

                        if (string.IsNullOrEmpty(movimentacaoLC1.ContaDebitoCodigoAcesso))
                        {
                            movimentacaoLC1.ContaDebitoCodigoAcesso = ContaGeralDebito;
                        }
                        //Se o centro de custo for um dos Centro de Custos que deve ser substituidos
                        else if (ContaDeParaOrigem.Contains(movimentacaoLC1.ContaDebitoCodigoAcesso))
                        {
                            movimentacaoLC1.ContaDebitoCodigoAcesso = ContaDeParaDestino;
                        }

                        break;
                    default:
                        break;
                }
            }

            lstMovimentacaoLC1.Add(movimentacaoLC1);
        }

        private void GerarMovimentosLC2(IEnumerable<Movimentacao> lstDefineTipo)
        {
            GerarMovimentosLC2(lstDefineTipo, false);
        }

        private void GerarMovimentosLC2(IEnumerable<Movimentacao> lstDefineTipo, bool compensacao)
        {
            var criarLC1 = true;

            foreach (var item in lstDefineTipo)
            {


                var movimentacaoLC2 = new MovimentacaoLC2();
                var findMovimentacaoLC1 = lstMovimentacaoLC1.Where(p => p.NumeroDocumento == Convert.ToInt32(item.NumeroDocumento)).FirstOrDefault();

                if (findMovimentacaoLC1 == null || (compensacao && criarLC1))
                {
                    var movimentacaoLC1 = new MovimentacaoLC1();

                    movimentacaoLC1.Ordem = ++intOrdemLC1;
                    movimentacaoLC1.ModoLancamento = "2";
                    movimentacaoLC1.DataEscrituracao = Convert.ToDateTime(item.DataEscrituracao);
                    movimentacaoLC1.NumeroDocumento = Convert.ToInt32(item.NumeroDocumento);
                    movimentacaoLC1.NumeroLote = "00001";
                    movimentacaoLC1.OrigemLancamento = DateTime.Now.Ticks;
                    movimentacaoLC1.Historico = "";
                    movimentacaoLC1.QuantidadeContas = lstDefineTipo.Count();

                    lstMovimentacaoLC1.Add(movimentacaoLC1);

                    movimentacaoLC2.Ordem = intOrdemLC1;

                    criarLC1 = false;
                }
                else
                {
                    //quantidade de contas
                    //findMovimentacaoLC1.QuantidadeContas++;
                    movimentacaoLC2.Ordem = intOrdemLC1;
                }

                switch (item.eCreditoDebito)
                {
                    //Regra 1
                    case CreditoDebito.Credito:
                        movimentacaoLC2.DebitoCredito = "C";
                        if (!string.IsNullOrEmpty(item.Terceiro))
                        {
                            var findTerceiro = lstFornecedor.Find(p => p.Apelido == item.Terceiro);

                            if (findTerceiro != null)
                            {
                                if (!string.IsNullOrEmpty(findTerceiro.CodigoPais) && findTerceiro.CodigoPais != "BR")
                                {
                                    item.Terceiro = findTerceiro.Apelido.Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "");

                                    item.Terceiro = item.Terceiro.Length == 6 ? item.Terceiro + "L" : item.Terceiro;
                                    item.Terceiro = item.Terceiro.PadLeft(14, ' ').Substring(0, 14);
                                }
                                else
                                {
                                    if (findTerceiro.Personalidade == "0")//Juridica
                                    {
                                        item.Terceiro = findTerceiro.CNPJCPFLivre.Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "").PadLeft(14, '0').Substring(0, 14);
                                    }
                                    else //Fisica
                                    {
                                        item.Terceiro = findTerceiro.CNPJCPFLivre.Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "").PadLeft(14, ' ').Substring(0, 14);
                                    }

                                }

                            }
                            else
                            {
                                item.Terceiro = "";
                            }
                        }

                        if (string.IsNullOrEmpty(movimentacaoLC2.CodigoAcesso))
                        {
                            movimentacaoLC2.CodigoAcesso = ContaGeralCredito;
                        }
                        //Se o centro de custo for um dos Centro de Custos que deve ser substituidos
                        else if (ContaDeParaOrigem.Contains(movimentacaoLC2.CodigoAcesso))
                        {
                            movimentacaoLC2.CodigoAcesso = ContaDeParaDestino;
                        }

                        movimentacaoLC2.Terceiro = item.Terceiro;
                        movimentacaoLC2.Valor = (Convert.ToDecimal(item.Valor) * (-1)).ToString(CultureInfo.CreateSpecificCulture("en-US"));
                        break;
                    //Regra 2
                    case CreditoDebito.Debito:
                        movimentacaoLC2.DebitoCredito = "D";
                        if (!string.IsNullOrEmpty(item.Navio) && !string.IsNullOrEmpty(item.Departamento))
                        {
                            try
                            {

                                var Navio = lstCentroCusto.FirstOrDefault(p => p.Navio == item.Navio);
                                var Departamento = lstCentroCusto.FirstOrDefault(p => p.Departamento.Contains(item.Departamento));

                                if (Navio != null && Departamento != null)
                                {
                                    movimentacaoLC2.CentroCusto = Navio.CCNavio + Departamento.CCDepartamento;
                                    movimentacaoLC2.CentroCusto = movimentacaoLC2.CentroCusto.Replace("`", "");
                                }
                                else
                                {
                                    movimentacaoLC2.CentroCusto = null;
                                }
                            }
                            catch (Exception)
                            {
                                movimentacaoLC2.CentroCusto = null;
                            }

                        }


                        movimentacaoLC2.Valor = Convert.ToDecimal(item.Valor).ToString(CultureInfo.CreateSpecificCulture("en-US"));
                        break;
                    default:
                        break;
                }

                movimentacaoLC2.CodigoAcesso = item.CodigoAcesso;

                if (string.IsNullOrEmpty(movimentacaoLC2.CodigoAcesso))
                {
                    movimentacaoLC2.CodigoAcesso = ContaGeralDebito;
                }
                //Se o centro de custo for um dos Centro de Custos que deve ser substituidos
                else if (ContaDeParaOrigem.Contains(movimentacaoLC2.CodigoAcesso))
                {
                    movimentacaoLC2.CodigoAcesso = ContaDeParaDestino;
                }

                movimentacaoLC2.Historico = string.IsNullOrEmpty(item.Historico) ? "" : item.Historico.Replace("\r\n", " ");

                movimentacaoLC2.IndicadorConciliacao = " ";

                lstMovimentacaoLC2.Add(movimentacaoLC2);
            }
        }

        private void GerarMovimentosCompensacao(IEnumerable<Movimentacao> lstDefineTipo)
        {
            var countMax= 1;
            var lstMaxMovimentos = new List<Movimentacao>();
            decimal valorACompensar = 0;
            var numDocumento = "";

            var count = 0;

            foreach (var item in lstDefineTipo)
            {
                count++;

                if (countMax < 200)
                {
                    lstMaxMovimentos.Add(item);

                }
                else
                {
                    if (valorACompensar != 0)
                    {
                        var compensacaoMovimentacao = new Movimentacao();
                        compensacaoMovimentacao.NumeroDocumento = item.NumeroDocumento;
                        compensacaoMovimentacao.CodigoAcesso = ContaCompensacao;
                        compensacaoMovimentacao.Valor = (Convert.ToDecimal(valorACompensar) * (-1)).ToString();
                        compensacaoMovimentacao.Historico = HistoricoCompensacao;
                        lstMaxMovimentos.Add(compensacaoMovimentacao);
                    }

                    GerarMovimentosLC2(lstMaxMovimentos, true);

                    lstMaxMovimentos.Clear();
                    valorACompensar = 0;
                    countMax = 1;

                    lstMaxMovimentos.Add(item);
                }

                valorACompensar = valorACompensar + Convert.ToDecimal(item.Valor);
                numDocumento = item.NumeroDocumento;
                countMax++;
            }

            //restante
            if (countMax < 200) {
                if (valorACompensar != 0)
                {
                    var compensacaoMovimentacao = new Movimentacao();
                    compensacaoMovimentacao.NumeroDocumento = numDocumento;
                    compensacaoMovimentacao.CodigoAcesso = ContaCompensacao;
                    compensacaoMovimentacao.Valor = (Convert.ToDecimal(valorACompensar) * (-1)).ToString();
                    compensacaoMovimentacao.Historico = HistoricoCompensacao;
                    lstMaxMovimentos.Add(compensacaoMovimentacao);
                }

                GerarMovimentosLC2(lstMaxMovimentos, true);
            }
        }
    }
}
