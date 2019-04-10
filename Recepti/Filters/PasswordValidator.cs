using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.Filters
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class PasswordValidator : ValidationAttribute
    {
        private const int MIN_LENGTH = 8;
        private const int MAX_LENGTH = 25;

        public PasswordValidator()
        {
            ErrorMessage = $"Lozinka mora sadržavati od {MIN_LENGTH} do {MAX_LENGTH} karaktera, bar jedno malo i veliko slovo, bar jednu cifru i bar jedan od specijalnih karaktera -_#$";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value.ToString();

            bool meetsLengthRequirements = password.Length >= MIN_LENGTH && password.Length <= MAX_LENGTH;
            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            bool hasDecimalDigit = false;
            bool hasSpecialChar = false;

            if (meetsLengthRequirements)
            {
                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDecimalDigit = true;
                    else if (c == '-' || c == '_' || c == '#' || c == '$') hasSpecialChar = true;
                }
            }

            bool isValid = meetsLengthRequirements
                        && hasUpperCaseLetter
                        && hasLowerCaseLetter
                        && hasDecimalDigit
                        && hasSpecialChar;

            if (!isValid)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
