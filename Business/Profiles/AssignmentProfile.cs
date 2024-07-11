using AutoMapper;
using Business.Dto.Request.Assignment;
using Business.Dto.Response.Assignment;
using Core.DataAccess.Paging;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class AssignmentProfile : Profile
    {
        public AssignmentProfile()
        {
            CreateMap<CreateAssignmentRequest, Assignment>().ReverseMap();
            CreateMap<Assignment, CreatedAssignmentResponse>().ReverseMap();

            CreateMap<DeleteAssignmentRequest, Assignment>().ReverseMap();
            CreateMap<Assignment, DeletedAssignmentResponse>().ReverseMap();

            CreateMap<UpdateAssignmentRequest, Assignment>().ReverseMap();
            CreateMap<Assignment, UpdatedAssignmentResponse>().ReverseMap();

            CreateMap<Assignment, GetListAssignmentResponse>().ReverseMap();
            CreateMap<Paginate<Assignment>, Paginate<GetListAssignmentResponse>>().ReverseMap();
        }
    }
}
