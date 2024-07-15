using AutoMapper;
using Business.Abstracts;
using Business.Dto.Request.Assignment;
using Business.Dto.Response.Assignment;
using Core.DataAccess.Paging;
using DataAccess.Abstracts;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class AssignmentManager : IAssignmentService
    {

        IAssignmentDal _assignmentDal;
        IMapper _mapper;

        public AssignmentManager(IAssignmentDal assignmentDal, IMapper mapper)
        {
            _assignmentDal = assignmentDal;
            _mapper = mapper;
        }

        public async Task<CreatedAssignmentResponse> Add(CreateAssignmentRequest createAssignmentRequest)
        {
            Assignment assignment = _mapper.Map<Assignment>(createAssignmentRequest);
            Assignment createdAssignment = await _assignmentDal.AddAsync(assignment);
            CreatedAssignmentResponse createdAssignmentResponse = _mapper.Map<CreatedAssignmentResponse>(createdAssignment);
            return createdAssignmentResponse;
        }

        public async Task<DeletedAssignmentResponse> Delete(Guid id)
        {
            var data = await _assignmentDal.GetAsync(i => i.Id == id);
            var result = await _assignmentDal.DeleteAsync(data);
            var result2 = _mapper.Map<DeletedAssignmentResponse>(result);
            return result2;
        }

        public async Task<CreatedAssignmentResponse> GetById(Guid id)
        {
            var result = await _assignmentDal.GetAsync(c => c.Id == id);
            Assignment mappedAssignment = _mapper.Map<Assignment>(result);
            CreatedAssignmentResponse createdAssignmentResponse = _mapper.Map<CreatedAssignmentResponse>(mappedAssignment);
            return createdAssignmentResponse;
        }

        public async Task<IPaginate<GetListAssignmentResponse>> GetByProjectId(Guid projectId,PageRequest pageRequest)
        {
            var data = await _assignmentDal.GetListAsync(
                  index: pageRequest.PageIndex,
                  size: pageRequest.PageSize,
                  predicate:a=>a.ProjectId == projectId,
                  include:a=>a.Include(a=>a.Project)
              );
            var result = _mapper.Map<Paginate<GetListAssignmentResponse>>(data);
            return result;
        }

        public async Task<IPaginate<GetListAssignmentResponse>> GetListAsync(PageRequest pageRequest)
        {
            var data = await _assignmentDal.GetListAsync(
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize,
                include: a => a.Include(a => a.Project)
            );
            var result = _mapper.Map<Paginate<GetListAssignmentResponse>>(data);
            return result;
        }

        public async Task<UpdatedAssignmentResponse> Update(UpdateAssignmentRequest updateAssignmentRequest)
        {
            var data = await _assignmentDal.GetAsync(i => i.Id == updateAssignmentRequest.Id);
            _mapper.Map(updateAssignmentRequest, data);
            await _assignmentDal.UpdateAsync(data);
            var result = _mapper.Map<UpdatedAssignmentResponse>(data);
            return result;
        }
    }
}
