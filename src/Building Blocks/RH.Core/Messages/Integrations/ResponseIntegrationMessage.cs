using FluentValidation.Results;

namespace RH.Core.Messages.Integrations
{
    public class ResponseIntegrationMessage : Message
    {
        public ValidationResult ValidationResult { get; private set; }

        public ResponseIntegrationMessage(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}
