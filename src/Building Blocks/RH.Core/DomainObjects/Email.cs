using System.Text.RegularExpressions;

namespace RH.Core.DomainObjects
{
    public class Email
    {
        public string Endereco { get; private set; }

        protected Email()
        {
        }

        public Email(string endereco)
        {
            if (!Validar(endereco))
                throw new DomainException("E-mail inválido!");

            Endereco = endereco;
        }

        public static bool Validar(string email)
        {
            var regexEmail = new Regex(@"^(([^<>()[\]\.,;:\s@\""]+ (\.[^<> ()[\]\.,;:\s@\""]+)*)|(\"".+\""))@(([^<>()[\]\.,;:\s@\""]+\.)+[^<>()[\]\.,;:\s@\""]{2,})$");
            return regexEmail.IsMatch(email);
        }
    }
}
