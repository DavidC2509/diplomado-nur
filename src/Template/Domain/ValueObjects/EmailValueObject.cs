using System.Text.RegularExpressions;

namespace Template.Domain.ValueObjects
{
    public class EmailValueObject
    {
        public string Value { get; init; }

        public EmailValueObject(string value)
        {
            Value = value;
        }
        public EmailValueObject()
        {
            Value = string.Empty;
        }


        public static EmailValueObject Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El correo electrónico no puede estar vacío.");

            if (!IsValidEmail(email))
                throw new ArgumentException("El formato del correo electrónico no es válido.");

            return new EmailValueObject(email);
        }

        private static bool IsValidEmail(string email)
        {
            // Expresión regular estándar para validar un correo
            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }
    }
}