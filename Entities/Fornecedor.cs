using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Fornecedor
    {

        public Fornecedor()
        {
            CNPJCPFLivre = string.Empty;
            Nome = string.Empty;
            Apelido = string.Empty;
            TipoLogradouro = string.Empty;
            Logradouro = string.Empty;
            Numero = string.Empty;
            Complemento = string.Empty;
            CEP = string.Empty;
            Bairro = string.Empty;
            Municipio = string.Empty;
            UF = string.Empty;
            DataNascInicioAtividades = string.Empty;
            TelefoneDDD = string.Empty;
            TelefoneNumero = string.Empty;
            TelefaxDDD = string.Empty;
            TelefaxNumero = string.Empty;
            Email = string.Empty;
            Homepage = string.Empty;
            InscricaoEstadual = string.Empty;
            InscricaoMunicipal = string.Empty;
            CNAE = string.Empty;
            RGNumero = string.Empty;
            RGOrgaoEmissor = string.Empty;
            RGEstadoEmissor = string.Empty;
            RGDataEmissao = string.Empty;
            Sexo = string.Empty;
            CodigoPais = string.Empty;
            CodigoIBGE = string.Empty;
            Filler2 = string.Empty;
            CodigoMunicipioEstadual = string.Empty;
            EnderecoTipoBairro = string.Empty;
            EnderecoPrincipal = string.Empty;
            ContribuinteICMS = string.Empty;
        }

        public string Tipo = "TRC";
        public string Ordem = string.Empty.PadLeft(5, ' ');
        public string Filler = string.Empty.PadLeft(2, ' ');

        /// <summary>
        /// 0-Jurídica /1-Física/2-Livre
        /// </summary>
        public string Personalidade
        {
            get
            {
                if (CNPJCPFLivre.Replace(".", "").Replace("/","").Replace("-", "").Length == 14 && CodigoPais == "BR")
                {
                    return "0"; //Jurídica
                }
                else if (CNPJCPFLivre.Replace(".", "").Replace("/", "").Replace("-", "").Length == 11 && CodigoPais == "BR")
                {
                    return "1"; //Física
                }
                else
                {
                    return "2";
                }
            }
        }

        public string CNPJCPFLivre { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string TipoLogradouro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string UF { get; set; }
        public string DataNascInicioAtividades { get; set; }
        public string TelefoneDDD { get; set; }
        public string TelefoneNumero { get; set; }
        public string TelefaxDDD { get; set; }
        public string TelefaxNumero { get; set; }
        public string Email { get; set; }
        public string Homepage { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string CNAE { get; set; }
        public string RGNumero { get; set; }
        public string RGOrgaoEmissor { get; set; }
        public string RGEstadoEmissor { get; set; }
        public string RGDataEmissao { get; set; }
        public string Sexo { get; set; }
        public string CodigoPais { get; set; }
        public string CodigoIBGE { get; set; }
        public string Filler2 { get; set; }
        public string CodigoMunicipioEstadual { get; set; }
        public string EnderecoTipoBairro { get; set; }
        public string EnderecoPrincipal { get; set; }
        public string ContribuinteICMS { get; set; }
    }
}
