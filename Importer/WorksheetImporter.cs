using System.Collections.Generic;
using System.IO;
using Aspose.Cells;
using Common;
using System.Xml;
using System.Text;
using System;

namespace Importer
{
    /// <summary>
    /// Classe abstrata que deve ser herdada para contrololar os arquivos no formato excel
    /// <para>
    /// Lê as worksheets do arquivo Excel e grava as informações
    /// </para>
    /// </summary>
    public abstract class WorksheetImporter
    {
        /// <summary>
        /// Workbook que contém as worksheets a serem importadas.
        /// </summary>
        private readonly Workbook _workbook;
        private readonly Stream _stream;
        protected string WorksheetName;
        protected int MaxErrors;
        private readonly int _currentWorksheet;



        public Workbook Workbook { get { return _workbook; } }

        public Stream Stream { get { return _stream; } }

        /// <summary>
        /// Construtor que recebe o caminho do arquivo a ser importado.
        /// </summary>
        /// <param name="filePath">Caminho do arquivo Excel que será importado em arquivos XML.</param>
        protected WorksheetImporter(string filePath, int currentWorksheet)
        {
            try
            {
                _currentWorksheet = currentWorksheet;
                _workbook = new Workbook(filePath, new LoadOptions(LoadFormat.Auto));
                WorksheetName = _workbook.Worksheets[currentWorksheet].Name;
            }
            catch (Exception ex)
            {
                 throw ex;
            }
        }

        protected WorksheetImporter(string filePath, string separator)
        {
            _workbook = new Workbook(filePath, new TxtLoadOptions {SeparatorString = separator});
            WorksheetName = _workbook.Worksheets[_currentWorksheet].Name;
        }

        protected WorksheetImporter(Stream stream)
        {
            _stream = stream;
            _workbook = new Workbook(_stream);
            WorksheetName = _workbook.Worksheets[_currentWorksheet].Name;
        }

        /// <summary>
        /// Reformata as mensagens de validação de forma a concatenar seu tipo (<see cref="MessageType"/>), a worksheet onde ocorreu
        /// e a mensagem original.
        /// </summary>
        /// <param name="result">Lista de menagens de validações.</param>
        protected void ContextualizeMessages(WorksheetValidationResult result)
        {
            foreach (var item in result.ValidationMessages)
            {
                //Recuperar a string do tipo de mensagem ("ERRO", "Informação", ...).
                var strType = string.Empty;
                switch (item.MessageType)
                {
                    case MessageType.Info:
                        strType = ImportWorksheetResources.MessageType_Info;
                        break;
                    case MessageType.Warning:
                        strType = ImportWorksheetResources.MessageType_Warning;
                        break;
                    case MessageType.Error:
                        strType = ImportWorksheetResources.MessageType_Error;
                        break;
                }

                //Substitui a mensagem original pela concatenação do tipo de mensagem, nome da worksheet e mensagem original.
                var message = string.Format(ImportWorksheetResources.ContextualizationMessage, strType, item.Message);
                item.Message = message;
            }
        }

        public abstract WorksheetValidationResult ValidateWorksheet();

        public virtual ExcelReader GetExcelReader()
        {
            ExcelReader excelReader = null;
            if (Workbook.Worksheets[_currentWorksheet] != null)
            {
                var sheet = Workbook.Worksheets[_currentWorksheet];
                excelReader = new ExcelReader(sheet);
            }
            return excelReader;
        }

        public virtual List<T> ConverterWorksheet<T>(XmlDocument doc)
        {
            var lstObj = new List<T>();

            var excelReader = GetExcelReader();

            if (doc.DocumentElement != null)
            {
                var childNOdesSettings = doc.DocumentElement.ChildNodes;

                for (excelReader.Row = Convert.ToInt32(doc.DocumentElement.Attributes["InitialRow"].Value); excelReader.Row <= excelReader.CountRows(true); excelReader.Row++)
                {
                    if (excelReader.Empty(0, Convert.ToInt32(doc.DocumentElement.Attributes["LastColumn"].Value)))
                    {
                        continue;
                    }
                    var obj = (T)Activator.CreateInstance(typeof(T));

                    foreach (XmlNode item in childNOdesSettings)
                    {
                        if (item.Attributes != null)
                        {
                            var columnValue = excelReader.ReadString(Convert.ToInt32(item.Attributes["Column"].Value)) ?? "";
                            (obj.GetType()).GetProperty(item.Name).SetValue(obj, columnValue);
                        }
                    }

                    lstObj.Add(obj);

                }
            }

            return lstObj;
        }
    }
}