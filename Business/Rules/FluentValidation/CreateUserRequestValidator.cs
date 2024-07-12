using Business.Dto.Request.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Rules.ValidationRules.FluentValidation.UserValidators
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).EmailAddress().WithMessage("Geçersiz e-posta formatı.");
            RuleFor(u => u.Password).NotEmpty();
            RuleFor(u => u.Password).MinimumLength(6);
            RuleFor(u => u.Password).Matches(@"[A-Z]").WithMessage("Parola en az bir büyük harf içermelidir.");
            RuleFor(u => u.Password).Matches(@"[a-z]").WithMessage("Parola en az bir küçük harf içermelidir.");
            RuleFor(u => u.Password).Matches(@"\d").WithMessage("Parola en az bir rakam içermelidir.");
        }
    }
}
