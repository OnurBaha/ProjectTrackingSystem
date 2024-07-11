using Business.Abstracts;
using Business.Dto.Request.Assignment;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        IAssignmentService _assignmentService;
        public AssignmentsController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }


        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateAssignmentRequest createAssignmentRequest)
        {
            var result = await _assignmentService.Add(createAssignmentRequest);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] DeleteAssignmentRequest deleteAssignmentRequest)
        {
            var result = await _assignmentService.Delete(deleteAssignmentRequest);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateAssignmentRequest updateAssignmentRequest)
        {
            var result = await _assignmentService.Update(updateAssignmentRequest);
            return Ok(result);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            var result = await _assignmentService.GetListAsync(pageRequest);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            var result = await _assignmentService.GetById(id);
            return Ok(result);
        }
    }
}
