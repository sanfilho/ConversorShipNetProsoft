using System;
using Aspose.Cells;
using Common;

namespace Importer
{
        /// <summary>
        /// Leitor de células de um worksheet Excel com validações dos seus conteúdos.
        /// </summary>
        /// <remarks>
        /// A classe é instanciada e usam-se os métodos de leitura de células. No final, recuperam-se as mensagens de validações
        /// através do atributo <c>ValidationResult</c>
        /// </remarks>
        public class ExcelReader
        {
            /// <summary>
            /// Resultado da validação do processamento da planilha <see cref="WorksheetValidationResult"/>
            /// </summary>
            public WorksheetValidationResult ValidationResult;

            /// <summary>
            /// Linha corrente da worksheet.
            /// </summary>
            public int Row;

            /// <summary>
            /// Worksheet sendo processada.
            /// </summary>
            private Worksheet sheet;

            /// <summary>
            /// Construtor.
            /// </summary>
            /// <param name="sheet">Worksheet a ser processada.</param>
            /// <param name="row">Linha corrente inicial da worksheet (Opcional). Se não for informado, assumirá que é a linha zero.</param>
            public ExcelReader(Worksheet sheet, int row = 0)
            {
                this.sheet = sheet;
                Row = row;
                ValidationResult = new WorksheetValidationResult();
            }

            /// <summary>
            /// Verifica na linha corrente <see cref="Row"/> se uma célula ou um range de células estão vazias.
            /// </summary>
            /// <param name="column">Coluna a verificar (ou a coluna inicial, quando a coluna final for especificada.</param>
            /// <param name="finalColumn">Coluna final a verificar, no caso de um range (Opcional).</param>
            /// <returns>True se a célula/todo o range estiver vazio. False se a célula/pelo menos uma célula do range estiver preenchida.</returns>
            public bool Empty(int column, int finalColumn = -1)
            {
                if (finalColumn >= 0 && finalColumn >= column)
                {
                    //É um range. Verifica cada celula.
                    for (int i = column; i <= finalColumn; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(sheet.Cells[Row, i].StringValue))
                            return false;
                    }
                    return true;
                }
                else
                {
                    //É apenas uma célula a ser verificada.
                    return string.IsNullOrWhiteSpace(sheet.Cells[Row, column].StringValue);
                }
            }

            /// <summary>
            /// Passa a referenciar a próxima linha <see cref="Row"/> da worksheet.
            /// </summary>
            public void MoveNextRow()
            {
                Row++;
            }
            /// <summary>
            /// Retornar o número de linhas que o documento possui.
            /// </summary>
            /// <param name="withTitle">define se o titulo entra na contagem.</param>
            /// <returns></returns>
            public int CountRows(bool withTitle)
            {
                var count = sheet.Cells.MaxDataRow;
                return withTitle ? count : count - 1;
            }

            /// <summary>
            /// Lê como string o valor de uma célula da worksheet, assumindo que a linha corrente será referenciada (somente a coluna é passada).
            ///   <para>
            ///   Uma série de parâmetros pode ser passado para validar o valor lido.
            ///   </para>
            /// </summary>
            /// <param name="column">Coluna da célula.</param>
            /// <param name="mandatory">Se é obrigatório haver um valor na célula (Opctional). o padrão é false.</param>
            /// <param name="minLength">O comprimento mínimo que o valor da célula deve possuir (Opctional). o padrão é zero.</param>
            /// <param name="maxLength">O comprimento máximo que o valor da célula deve possuir (Opctional). o padrão é -1.</param>
            /// <returns>O valor como string da célula solicitada.</returns>
            /// <remarks>
            /// O valor da célula solicitada é sempre retornado, mesmo se não passar nas validações solicitadas.
            /// Caso o valor da célula seja null ou string vazia (""), será retornado null.
            /// </remarks>
            public string ReadString(int column, bool mandatory = false, int minLength = 0, int maxLength = -1)
            {
                return ReadString(Row, column, mandatory, minLength, maxLength);
            }

            /// <summary>
            /// Lê como string o valor de uma célula da worksheet.
            ///   <para>
            ///   Uma série de parâmetros pode ser passado para validar o valor lido.
            ///   </para>
            /// </summary>
            /// <param name="row">Linha da célula.</param>
            /// <param name="column">Coluna da célula.</param>
            /// <param name="mandatory">Se é obrigatório haver um valor na célula (Opctional). o padrão é false.</param>
            /// <param name="minLength">O comprimento mínimo que o valor da célula deve possuir (Opctional). o padrão é zero.</param>
            /// <param name="maxLength">O comprimento máximo que o valor da célula deve possuir (Opctional). o padrão é -1.</param>
            /// <returns>O valor como string da célula solicitada.</returns>
            /// <remarks>
            /// O valor da célula solicitada é sempre retornado, mesmo se não passar nas validações solicitadas.
            /// Caso o valor da célula seja null ou string vazia (""), será retornado null.
            /// </remarks>
            public string ReadString(int row, int column, bool mandatory = false, int minLength = 0, int maxLength = -1)
            {
                sheet.Cells.PreserveString = true;
                //Recupera o valor da célula como string.
                var value = sheet.Cells[row, column].StringValue;

                if (string.IsNullOrWhiteSpace(value))
                {
                    //Se for obrigatório haver um valor na célula e não houver, adiciona um mensagem de validação.
                    if (mandatory)
                        AddNullValueMessage(row, column);
                    return null;
                }

                //Se o valor precisar ter um tamanho mínimo e não tiver, adiciona um mensagem de validação.
                if (minLength > 0 && value.Length < minLength)
                    AddInvalidValueMessage(row, column);

                //Se o valor precisar ter um tamanho máximo e não tiver, adiciona um mensagem de validação.
                if (maxLength > -1 && value.Length > maxLength)
                    AddInvalidValueMessage(row, column);

                return value;
            }

            /// <summary>
            /// Lê como int o valor de uma célula da worksheet, assumindo que a linha corrente será referenciada (somente a coluna é passada).
            ///   <para>
            ///   Uma série de parâmetros pode ser passado para validar o valor lido.
            ///   </para>
            /// </summary>
            /// <param name="column">Coluna da célula.</param>
            /// <param name="mandatory">Se é obrigatório haver um valor na célula (Opctional). o padrão é false.</param>
            /// <param name="minValue">O valor mínimo que a célula deve possuir (Opctional). o padrão o valor mínimo do tipo int.</param>
            /// <param name="maxValue">O valro máximo que a célula deve possuir (Opctional). o padrão o valor máximo do tipo int.</param>
            /// <returns>O valor como int da célula solicitada.</returns>
            /// <remarks>
            /// O valor da célula solicitada é sempre retornado, mesmo se não passar nas validações solicitadas.
            /// Caso o valor da célula seja null, string vazia ("") ou um valor não inteiro, será retornado null.
            /// </remarks>
            public int? ReadInt(int column, bool mandatory = false, int minValue = int.MinValue, int maxValue = int.MaxValue)
            {
                return ReadInt(Row, column, mandatory, minValue, maxValue);
            }

            /// <summary>
            /// Lê como int o valor de uma célula da worksheet.
            ///   <para>
            ///   Uma série de parâmetros pode ser passado para validar o valor lido.
            ///   </para>
            /// </summary>
            /// <param name="row">Linha da célula.</param>
            /// <param name="column">Coluna da célula.</param>
            /// <param name="mandatory">Se é obrigatório haver um valor na célula (Opctional). o padrão é false.</param>
            /// <param name="minValue">O valor mínimo que a célula deve possuir (Opctional). o padrão o valor mínimo do tipo int.</param>
            /// <param name="maxValue">O valro máximo que a célula deve possuir (Opctional). o padrão o valor máximo do tipo int.</param>
            /// <returns>O valor como int da célula solicitada.</returns>
            /// <remarks>
            /// O valor da célula solicitada é sempre retornado, mesmo se não passar nas validações solicitadas.
            /// Caso o valor da célula seja null, string vazia ("") ou um valor não inteiro, será retornado null.
            /// </remarks>
            public int? ReadInt(int row, int column, bool mandatory = false, int minValue = int.MinValue, int maxValue = int.MaxValue)
            {
                if (string.IsNullOrWhiteSpace(sheet.Cells[row, column].StringValue))
                {
                    //Se for obrigatório haver um valor na célula e não houver, adiciona um mensagem de validação.
                    if (mandatory)
                        AddNullValueMessage(row, column);
                    return null;
                }

                int? value;
                try
                {
                    //Recupera o valor da célula como int.
                    value = sheet.Cells[row, column].IntValue;
                }
                catch
                {
                    //Se a célula contiver um valor não inteiro, , adiciona um mensagem de validação.
                    AddInvalidValueMessage(row, column);
                    return null;
                }

                //Se a cálula precisar ter um valor mínimo e não tiver, adiciona um mensagem de validação.
                if (minValue > int.MinValue && value < minValue)
                    AddInvalidValueMessage(row, column);

                //Se a cálula precisar ter um valor máximo e não tiver, adiciona um mensagem de validação.
                if (maxValue < int.MaxValue && value > maxValue)
                    AddInvalidValueMessage(row, column);

                return value;
            }


            /// <summary>
            /// Lê como data o valor de uma célula da worksheet, assumindo que a linha corrente será referenciada (somente a coluna é passada).
            ///   <para>
            ///   Uma série de parâmetros pode ser passado para validar o valor lido.
            ///   </para>
            /// </summary>
            /// <param name="column">Coluna da célula.</param>
            /// <param name="mandatory">Se é obrigatório haver um valor na célula (Opctional). o padrão é false.</param>
            /// <param name="minDate">O comprimento mínimo que o valor da célula deve possuir (Opctional). o padrão é zero.</param>
            /// <param name="maxDate">O comprimento máximo que o valor da célula deve possuir (Opctional). o padrão é -1.</param>
            /// <returns>O valor como string da célula solicitada.</returns>
            /// <remarks>
            /// O valor da célula solicitada é sempre retornado, mesmo se não passar nas validações solicitadas.
            /// Caso o valor da célula seja null ou string vazia (""), será retornado null.
            /// </remarks>
            public DateTime? ReadDateTime(int column, bool mandatory = false, DateTime? minDate = null, DateTime? maxDate = null)
            {
                minDate = minDate ?? DateTime.MinValue;
                maxDate = maxDate ?? DateTime.MaxValue;

                return ReadDateTime(Row, column, mandatory, minDate, maxDate);
            }

            /// <summary>
            /// Lê como int o valor de uma célula da worksheet.
            ///   <para>
            ///   Uma série de parâmetros pode ser passado para validar o valor lido.
            ///   </para>
            /// </summary>
            /// <param name="row">Linha da célula.</param>
            /// <param name="column">Coluna da célula.</param>
            /// <param name="mandatory">Se é obrigatório haver um valor na célula (Opctional). o padrão é false.</param>
            /// <param name="minDate">O valor mínimo que a célula deve possuir (Opctional). o padrão o valor mínimo do tipo int.</param>
            /// <param name="maxDate">O valro máximo que a célula deve possuir (Opctional). o padrão o valor máximo do tipo int.</param>
            /// <returns>O valor como int da célula solicitada.</returns>
            /// <remarks>
            /// O valor da célula solicitada é sempre retornado, mesmo se não passar nas validações solicitadas.
            /// Caso o valor da célula seja null, string vazia ("") ou um valor não inteiro, será retornado null.
            /// </remarks>
            public DateTime? ReadDateTime(int row, int column, bool mandatory = false, DateTime? minDate = null, DateTime? maxDate = null)
            {
                minDate = minDate ?? DateTime.MinValue;
                maxDate = maxDate ?? DateTime.MaxValue;


                if (string.IsNullOrWhiteSpace(sheet.Cells[row, column].StringValue))
                {
                    //Se for obrigatório haver um valor na célula e não houver, adiciona um mensagem de validação.
                    if (mandatory)
                        AddNullValueMessage(row, column);
                    return null;
                }

                DateTime? value;
                try
                {
                    //Recupera o valor da célula como date.
                    value = sheet.Cells[row, column].DateTimeValue;
                }
                catch
                {
                    //Se a célula contiver um valor não inteiro, , adiciona um mensagem de validação.
                    AddInvalidValueMessage(row, column);
                    return null;
                }

                //Se a cálula precisar ter um valor mínimo e não tiver, adiciona um mensagem de validação.
                if (minDate > DateTime.MinValue && value < minDate)
                    AddInvalidValueMessage(row, column);

                //Se a cálula precisar ter um valor máximo e não tiver, adiciona um mensagem de validação.
                if (maxDate < DateTime.MaxValue && value > maxDate)
                    AddInvalidValueMessage(row, column);

                return value;
            }

            
            
            /// <summary>
            /// Adiciona uma mensagem de validação à <c>ValidationResult</c> quando uma célula possuir valor vazio ou null.
            /// </summary>
            /// <param name="row">Linha da célula.</param>
            /// <param name="column">Coluna da célula.</param>
            private void AddNullValueMessage(int row, int column)
            {
                AddMessage(row, column, MessageType.Error, ImportWorksheetResources.ExcelReader_NullValueMessage);
            }

            /// <summary>
            /// Adiciona uma mensagem de validação à <c>ValidationResult</c> quando uma célula possuir valor inválido.
            /// </summary>
            /// <param name="row">Linha da célula.</param>
            /// <param name="column">Coluna da célula.</param>
            private void AddInvalidValueMessage(int row, int column)
            {
                AddMessage(row, column, MessageType.Error, ImportWorksheetResources.ExcelReader_InvalidValueMessage);
            }

            /// <summary>
            /// Adiciona uma mensagem de validação à <c>ValidationResult</c> quando o valor de uma célula não seguir alguma diretriz.
            /// </summary>
            /// <param name="row">Linha da célula.</param>
            /// <param name="column">Coluna da célula.</param>
            /// <param name="messageType">Tipo da mensagem de validação <see cref="MessageType"/>.</param>
            /// <param name="message">Texto da mensagem de validação.</param>
            private void AddMessage(int row, int column, MessageType messageType, string message)
            {
                //Converte a referência à célula do padrão (col,row) para o padrão LetraNúmero. Ex.: (0,2) -> A3.
                char charColumn = Convert.ToChar(Convert.ToInt32('A') + column);
                var cell = charColumn + (row + 1).ToString();

                //Substitui o placeholder {cell} da mensagem pela referência real da célula.
                var finalMessage = message.Replace("{cell}", cell);

                ValidationResult.AddValidationMessage(messageType, finalMessage);
            }
        }
    }