using Business.Dto.Request.Assignment;
using Business.Dto.Response.Assignment;
using Core.DataAccess.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IAssignmentService
    {
        Task<IPaginate<GetListAssignmentResponse>> GetListAsync(PageRequest pageRequest);
        Task<CreatedAssignmentResponse> Add(CreateAssignmentRequest createAssignmentRequest);
        Task<UpdatedAssignmentResponse> Update(UpdateAssignmentRequest updateAssignmentRequest);
        Task<DeletedAssignmentResponse> Delete(DeleteAssignmentRequest deleteAssignmentRequest);
        Task<CreatedAssignmentResponse> GetById(Guid id);
    }
}
