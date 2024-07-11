using AutoMapper;
using Business.Dto.Request.User;
using Business.Dto.Response.User;
using Business.Dtos.Requests.AuthRequests;
using Business.Dtos.Responses.AuthResponses;
using Core.DataAccess.Paging;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserRequest, User>().ReverseMap();
            CreateMap<User, CreatedUserResponse>().ReverseMap();

            CreateMap<DeleteUserRequest, User>().ReverseMap();
            CreateMap<User, DeletedUserResponse>().ReverseMap();

            CreateMap<UpdateUserRequest, User>().ReverseMap();
            CreateMap<User, UpdatedUserResponse>().ReverseMap();

            CreateMap<User, GetListUserResponse>().ReverseMap();
            CreateMap<Paginate<User>, Paginate<GetListUserResponse>>().ReverseMap();

            CreateMap<CreateUserRequest, GetUserResponse>().ReverseMap();
            CreateMap<UpdateUserRequest, GetUserResponse>().ReverseMap();

            CreateMap<User, LoginAuthRequest>().ReverseMap();
            CreateMap<User, RegisterAuthRequest>().ReverseMap();
            CreateMap<User, GetUserResponse>().ReverseMap();

        }
    }
}
