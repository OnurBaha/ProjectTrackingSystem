using AutoMapper;
using Business.Dtos.Responses.AuthResponses;
using Core.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class AuthProfile:Profile
    {
        public AuthProfile()
        {
            CreateMap<AccessToken, LoginResponse>().ReverseMap();
        }
    }
}
