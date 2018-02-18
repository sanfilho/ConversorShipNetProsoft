using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public enum MessageType
    {
        Success = 1,
        Warning = 2,
        Error = 3,
        Info = 4,
    }

    /// <summary>
    /// Mensagem de validação relacionada a alguma resultado de processamento.
    /// <example>Ex.: { MessageType.Error, "Campo 'Nome' precisa estar preenchido." }</example>
    /// </summary>
    public class ValidationMessage
    {
        /// <summary>
        /// Construtor de mensagem de validação.
        /// </summary>
        /// <param name="messageType">Tipo da mensagem de validação <see cref="MessageType"/>.</param>
        /// <param name="message">Texto da mensagem de validação.</param>
        public ValidationMessage(MessageType messageType, string message)
        {
            MessageType = messageType;
            Message = message;
        }

        /// <summary>
        /// Atributo que armazena o Tipo da mensagem de validação <see cref="MessageType"/>.
        /// </summary>
        public MessageType MessageType;

        /// <summary>
        /// Atributo que armazena o texto da mensagem de validação.
        /// </summary>
        public string Message;
    }


    /// <summary>
    /// Resultado das validações realizadas.
    /// <para>Pode conter uma lista de <see cref="ValidationMessage"/> pertinentes.</para>
    /// </summary>
    public class BasicValidationResult
    {
        /// <summary>
        /// Lista de <see cref="ValidationMessage"/> pertinentes.
        /// </summary>
        public List<ValidationMessage> ValidationMessages = new List<ValidationMessage>();

        /// <summary>
        /// Adiciona uma mensagem de validação ao resultado de processamento da worksheet.
        /// </summary>
        /// <param name="messageType">Tipo da mensagem de validação <see cref="MessageType"/>.</param>
        /// <param name="message">Texto da mensagem de validação.</param>
        public void AddValidationMessage(MessageType messageType, string message)
        {
            var validationMessage = new ValidationMessage(messageType, message);
            ValidationMessages.Add(validationMessage);
        }

        /// <summary>
        /// Adiciona uma lista de mensagens de validação ao resultado de processamento da worksheet.
        /// </summary>
        /// <param name="validationMessages">Tipo da mensagem de validação <see cref="ValidationMessage"/></param>
        public void AddValidationMessages(List<ValidationMessage> validationMessages)
        {
            ValidationMessages.AddRange(validationMessages);
        }

        /// <summary>
        /// Informa se alguma das mensagens de validação armazenadas é do tipo MessageType.Error <see cref="MessageType"/>
        /// </summary>
        /// <returns>True se pelo menos uma das mensagens de validação for do tipo MessageType.Error</returns>
        public bool AnyError()
        {
            return ValidationMessages.Any(v => v.MessageType == MessageType.Error);
        }
    }
}
