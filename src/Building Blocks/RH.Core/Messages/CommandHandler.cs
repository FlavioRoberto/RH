using FluentValidation.Results;
using RH.Core.Data;
using System.Threading.Tasks;

namespace RH.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        public CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AdicionarErro(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected async Task<ValidationResult> PersistirDados(IUnitOfWork unitOfWork)
        {
            if (!await unitOfWork.Commit())
                AdicionarErro("Houve um erro ao persistir o cliente");

            return ValidationResult;
        }
    }
}
