using AutoMapper;
using Business.Dto.Request.Project;
using Business.Dto.Response.Project;
using Core.DataAccess.Paging;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class ProjectProfile: Profile
    {
        public ProjectProfile()
        {
            CreateMap<CreateProjectRequest, Project>().ReverseMap();
            CreateMap<Project, CreatedProjectResponse>().ReverseMap();

            CreateMap<DeleteProjectRequest, Project>().ReverseMap();
            CreateMap<Project, DeletedProjectResponse>().ReverseMap();

            CreateMap<UpdateProjectRequest, Project>().ReverseMap();
            CreateMap<Project, UpdatedProjectResponse>().ReverseMap();

            CreateMap<Project, GetListProjectResponse>().ReverseMap();
            CreateMap<Paginate<Project>, Paginate<GetListProjectResponse>>().ReverseMap();
        }
    }
}
