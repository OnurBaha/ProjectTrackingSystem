using Business.Messages;
using Core.Business.Rules;
using Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails.Types;
using DataAccess.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Rules.BusinessRules
{
    public class UserBusinessRules : BaseBusinessRules
    {
        IUserDal _userDal;

        public UserBusinessRules(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task IsExistsUser(Guid userId)
        {
            var result = await _userDal.GetAsync(
                predicate: a => a.Id == userId,
                enableTracking: false);

            if (result == null)
            {
                throw new BusinessException(BusinessMessages.DataNotFound);
            }
        }
        public async Task IsExistsUserMail(string email)
        {
            var result = await _userDal.GetAsync(
                predicate: a => a.Email == email,
                enableTracking: false);

            if (result != null)
            {
                throw new BusinessException(BusinessMessages.DataAvailable);
            }
        }
    }
}
