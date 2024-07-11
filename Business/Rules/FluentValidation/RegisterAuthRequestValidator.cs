using Business.Dto.Request.User;
using Business.Dtos.Requests.AuthRequests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Rules.FluentValidation
{
    public class RegisterAuthRequestValidator : AbstractValidator<RegisterAuthRequest>
    {
        public RegisterAuthRequestValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.Password).NotEmpty();
            RuleFor(u => u.Password).MinimumLength(6);
        }
    }
}
