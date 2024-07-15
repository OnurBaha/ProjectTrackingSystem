using Business.Dto.Request.Assignment;
using Business.Dto.Request.Project;
using Business.Dto.Response.Assignment;
using Business.Dto.Response.Project;
using Core.DataAccess.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IProjectService
    {
        Task<IPaginate<GetListProjectResponse>> GetListAsync(PageRequest pageRequest);
        Task<CreatedProjectResponse> Add(CreateProjectRequest createProjectRequest);
        Task<UpdatedProjectResponse> Update(UpdateProjectRequest updateProjectRequest);
        Task<DeletedProjectResponse> Delete(Guid id);
        Task<CreatedProjectResponse> GetById(Guid id);
    }
}
