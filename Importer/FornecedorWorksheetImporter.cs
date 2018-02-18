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
    public class FornecedorWorksheetImporter : WorksheetImporter
    {
        public FornecedorWorksheetImporter(string filePath, int currentWorksheet) : base(filePath, currentWorksheet)
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
        public List<StringBuilder> CarregarFornecedor(XmlDocument settingsFornecedor, XmlDocument settingsPais, XmlDocument settingsMovimentacao, PaisWorksheetImporter wkbPais, MovimentacaoWorksheetImporter wkbMovimentacao, BackgroundWorker bgworker)
        {
            var lstFornecedor = ConverterWorksheet<Fornecedor>(settingsFornecedor);
            var lstPais = wkbPais.ConverterWorksheet<Pais>(settingsPais);
            var lstMovimentacao = wkbMovimentacao.ConverterWorksheet<Movimentacao>(settingsMovimentacao);

            var lstStbRegistros = new List<StringBuilder>();

            var filterLstFornecedor = from f in lstFornecedor
                                      join m in lstMovimentacao
                                        on f.Apelido equals m.Terceiro
                                      where f.CNPJCPFLivre != ""  
                                      select f;

            var count = 0;
            var maxCount = filterLstFornecedor.Distinct().Count();

            foreach (var item in filterLstFornecedor.Distinct())
            { 
                var stbRegister = new StringBuilder();

                if (!string.IsNullOrEmpty(item.CodigoPais) && item.CodigoPais != "BR")
                {
                    item.CNPJCPFLivre = item.Apelido.Length == 6 ? item.Apelido + "L" : item.Apelido;
                    item.UF = "EX";
                    item.InscricaoEstadual = "ISENTO";
                }

                stbRegister.Append(item.Tipo);
                stbRegister.Append(item.Ordem);
                stbRegister.Append(item.Filler);
                stbRegister.Append(item.Personalidade);
                stbRegister.Append(item.CNPJCPFLivre.Replace(" ","").Replace(".", "").Replace("/", "").Replace("-","").PadLeft(14,'0').Substring(0,14));
                stbRegister.Append(item.Nome.PadRight(60, ' ').Substring(0,60));
                stbRegister.Append(item.Apelido.PadRight(20, ' '));
                stbRegister.Append(item.TipoLogradouro.PadRight(10,' '));
                stbRegister.Append(item.Logradouro.PadRight(60, ' '));
                stbRegister.Append(item.Numero.PadLeft(10, ' '));
                stbRegister.Append(item.Complemento.PadRight(20, ' ').Substring(0,20));
                stbRegister.Append(item.CEP.PadLeft(09, ' '));
                stbRegister.Append(item.Bairro.PadLeft(30, ' '));
                stbRegister.Append(item.Municipio.PadRight(30, ' ').Substring(0,30));
                stbRegister.Append(item.UF.PadLeft(2, ' '));
                stbRegister.Append(item.DataNascInicioAtividades.PadLeft(8, ' '));
                stbRegister.Append(item.TelefoneDDD.PadLeft(5, ' '));
                stbRegister.Append(item.TelefoneNumero.PadLeft(10, ' '));
                stbRegister.Append(item.TelefaxDDD.PadLeft(5, ' '));
                stbRegister.Append(item.TelefaxNumero.PadLeft(10, ' '));
                stbRegister.Append(item.Email.PadLeft(50, ' '));
                stbRegister.Append(item.Homepage.PadLeft(60, ' '));

                if (string.IsNullOrEmpty(item.InscricaoEstadual))
                {
                    item.InscricaoEstadual = "ISENTO";
                }

                stbRegister.Append(item.InscricaoEstadual.PadRight(20, ' '));
                stbRegister.Append(item.InscricaoMunicipal.PadRight(20, ' '));
                stbRegister.Append(item.CNAE.PadLeft(10, ' '));
                stbRegister.Append(item.RGNumero.PadLeft(18, ' '));
                stbRegister.Append(item.RGOrgaoEmissor.PadLeft(5, ' '));
                stbRegister.Append(item.RGEstadoEmissor.PadLeft(2, ' '));
                stbRegister.Append(item.RGDataEmissao.PadLeft(8, ' '));
                stbRegister.Append(item.Sexo.PadLeft(1, ' '));

                if (!string.IsNullOrEmpty(item.CodigoPais))
                {
                    var pais = lstPais.FirstOrDefault(p => p.Sigla == item.CodigoPais);
                    if (pais != null)
                    {
                        item.CodigoPais = pais.Codigo;
                    }
                    else {
                        item.CodigoPais = "";
                    }
                }

                stbRegister.Append(item.CodigoPais.PadLeft(4, ' '));////
                stbRegister.Append(item.CodigoIBGE.PadLeft(5, ' '));
                stbRegister.Append(item.Filler2.PadLeft(86, ' '));
                stbRegister.Append(item.CodigoMunicipioEstadual.PadLeft(10, ' '));
                stbRegister.Append(item.EnderecoTipoBairro.PadLeft(10, ' '));
                stbRegister.Append(item.EnderecoPrincipal.PadLeft(1, 'S'));
                stbRegister.Append(item.ContribuinteICMS.PadLeft(1, 'N'));////

                lstStbRegistros.Add(stbRegister);

                var progress = Convert.ToInt32((Convert.ToDouble(++count) / Convert.ToDouble(maxCount)) * 100);

                bgworker.ReportProgress(progress);

            }

            return lstStbRegistros;
        }
    }
}
