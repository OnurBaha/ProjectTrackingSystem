using AutoMapper;
using Business.Abstracts;
using Business.Dto.Request.Project;
using Business.Dto.Request.Project;
using Business.Dto.Request.Project;
using Business.Dto.Request.Project;
using Business.Dto.Response.Project;
using Business.Dto.Response.Project;
using Business.Dto.Response.Project;
using Business.Dto.Response.Project;
using Business.Dto.Response.Project;
using Business.Dto.Response.Project;
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
    public class ProjectManager : IProjectService
    {
        IProjectDal _projectDal;
        IMapper _mapper;

        public ProjectManager(IProjectDal projectDal, IMapper mapper)
        {
            _projectDal = projectDal;
            _mapper = mapper;
        }

        public async Task<CreatedProjectResponse> Add(CreateProjectRequest createProjectRequest)
        {
            Project project = _mapper.Map<Project>(createProjectRequest);
            Project createdProject = await _projectDal.AddAsync(project);
            CreatedProjectResponse createdProjectResponse = _mapper.Map<CreatedProjectResponse>(createdProject);
            return createdProjectResponse;
        }


        public async Task<DeletedProjectResponse> Delete(DeleteProjectRequest deleteProjectRequest)
        {
            var data = await _projectDal.GetAsync(i => i.Id == deleteProjectRequest.Id);
            _mapper.Map(deleteProjectRequest, data);
            var result = await _projectDal.DeleteAsync(data);
            var result2 = _mapper.Map<DeletedProjectResponse>(result);
            return result2;
        }

        public async Task<CreatedProjectResponse> GetById(Guid id)
        {
            var result = await _projectDal.GetAsync(c => c.Id == id);
            Project mappedProject = _mapper.Map<Project>(result);
            CreatedProjectResponse createdProjectResponse = _mapper.Map<CreatedProjectResponse>(mappedProject);
            return createdProjectResponse;
        }

        public async Task<IPaginate<GetListProjectResponse>> GetListAsync(PageRequest pageRequest)
        {
            var data = await _projectDal.GetListAsync(
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
            );
            var result = _mapper.Map<Paginate<GetListProjectResponse>>(data);
            return result;
        }

        public async Task<UpdatedProjectResponse> Update(UpdateProjectRequest updateProjectRequest)
        {
            var data = await _projectDal.GetAsync(i => i.Id == updateProjectRequest.Id);
            _mapper.Map(updateProjectRequest, data);
            await _projectDal.UpdateAsync(data);
            var result = _mapper.Map<UpdatedProjectResponse>(data);
            return result;
        }
    }
}
