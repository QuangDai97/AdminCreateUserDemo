using System.ComponentModel.DataAnnotations;

namespace AdminCreateUserDemo.Validate
{
    public class CustomEmailDomainAttribute : ValidationAttribute
    {
        private readonly string[] _allowedDomains;
        public CustomEmailDomainAttribute(string[] allowedDomains)
        {
            _allowedDomains = allowedDomains;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var email = value.ToString();
                var emailDomain = email.Substring(email.LastIndexOf('@') + 1);

                if (!_allowedDomains.Contains(emailDomain))
                {
                    return new ValidationResult($"Email domain must be one of the following: {string.Join(", ", _allowedDomains)}");
                }
            }


            return ValidationResult.Success;
        }
    }
}
