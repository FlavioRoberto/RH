using FluentValidation;
using RH.Core.Messages;
using System;

namespace RH.Clientes.API.Application.Commands.Registrar
{
    public class RegistrarClienteCommand : Command
    {
        public Guid Id { get; private set; }
        public string NomeRazaoSocial { get; private set; }
        public string Email { get; private set; }

        public RegistrarClienteCommand(Guid id, string nomeRazaoSocial, string email)
        {
            AggregateId = id;
            Id = id;
            NomeRazaoSocial = nomeRazaoSocial;
            Email = email;
        }

        public override bool EhValido()
        {
            ValidationResult = new RegistrarClienteCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RegistrarClienteCommandValidator : AbstractValidator<RegistrarClienteCommand>
    {
        public RegistrarClienteCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage("E-mail do cliente não informado")
                .EmailAddress()
                .WithMessage("E-mail do cliente inválido");

            RuleFor(c => c.NomeRazaoSocial)
                .NotEmpty()
                .WithMessage("Nome ou razão social do cliente não informado");
        }
    }
}
