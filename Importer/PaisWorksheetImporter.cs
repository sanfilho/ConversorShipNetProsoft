using System;
using System.Collections.Generic;
using Entities;
using System.IO;
using System.Xml;
using System.Text;
using System.Linq;
using System.Globalization;

namespace Importer
{
    public class PaisWorksheetImporter : WorksheetImporter
    {
        public PaisWorksheetImporter(string filePath, int currentWorksheet) : base(filePath, currentWorksheet)
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
    }
}
