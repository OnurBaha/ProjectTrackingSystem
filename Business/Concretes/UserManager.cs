using AutoMapper;
using Business.Abstracts;
using Business.Dto.Request.User;
using Business.Dto.Request.User;
using Business.Dto.Request.User;
using Business.Dto.Request.User;
using Business.Dto.Response.User;
using Business.Dto.Response.User;
using Business.Dto.Response.User;
using Business.Dto.Response.User;
using Business.Dto.Response.User;
using Business.Dto.Response.User;
using Core.DataAccess.Paging;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class UserManager : IUserService
    {

        IUserDal _userDal;
        IMapper _mapper;

        public UserManager(IUserDal userDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
        }
        public async Task<CreatedUserResponse> Add(CreateUserRequest createUserRequest)
        {
            User user = _mapper.Map<User>(createUserRequest);
            User createdUser = await _userDal.AddAsync(user);
            CreatedUserResponse createdUserResponse = _mapper.Map<CreatedUserResponse>(createdUser);
            return createdUserResponse;
        }

        public async Task<DeletedUserResponse> Delete(DeleteUserRequest deleteUserRequest)
        {
            var data = await _userDal.GetAsync(i => i.Id == deleteUserRequest.Id);
            _mapper.Map(deleteUserRequest, data);
            var result = await _userDal.DeleteAsync(data);
            var result2 = _mapper.Map<DeletedUserResponse>(result);
            return result2;
        }

        public async Task<CreatedUserResponse> GetById(Guid id)
        {
            var result = await _userDal.GetAsync(c => c.Id == id);
            User mappedUser = _mapper.Map<User>(result);
            CreatedUserResponse createdUserResponse = _mapper.Map<CreatedUserResponse>(mappedUser);
            return createdUserResponse;
        }

        public async Task<GetUserResponse> GetByMailAsync(string email)
        {
            var user = await _userDal.GetAsync(u => u.Email == email);
            var mappedUser = _mapper.Map<GetUserResponse>(user);
            return mappedUser;
        }

        public async Task<IPaginate<GetListUserResponse>> GetListAsync(PageRequest pageRequest)
        {
            var data = await _userDal.GetListAsync(
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
            );
            var result = _mapper.Map<Paginate<GetListUserResponse>>(data);
            return result;
        }

        public async Task<UpdatedUserResponse> Update(UpdateUserRequest updateUserRequest)
        {
            var data = await _userDal.GetAsync(i => i.Id == updateUserRequest.Id);
            _mapper.Map(updateUserRequest, data);
            await _userDal.UpdateAsync(data);
            var result = _mapper.Map<UpdatedUserResponse>(data);
            return result;
        }
    }
}
