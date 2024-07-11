using Business.Dto.Request.Project;
using Business.Dto.Request.User;
using Business.Dto.Response.Project;
using Business.Dto.Response.User;
using Core.DataAccess.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IUserService
    {
        Task<IPaginate<GetListUserResponse>> GetListAsync(PageRequest pageRequest);
        Task<CreatedUserResponse> Add(CreateUserRequest createUserRequest);
        Task<UpdatedUserResponse> Update(UpdateUserRequest updateUserRequest);
        Task<DeletedUserResponse> Delete(DeleteUserRequest deleteUserRequest);
        Task<CreatedUserResponse> GetById(Guid id);
        Task<GetUserResponse> GetByMailAsync(string email);
    }
}
