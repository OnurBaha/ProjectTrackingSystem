﻿using Business.Abstracts;
using Business.Dto.Request.Project;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        IProjectService _projectService;
        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }


        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateProjectRequest createProjectRequest)
        {
            var result = await _projectService.Add(createProjectRequest);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] DeleteProjectRequest deleteProjectRequest)
        {
            var result = await _projectService.Delete(deleteProjectRequest);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateProjectRequest updateProjectRequest)
        {
            var result = await _projectService.Update(updateProjectRequest);
            return Ok(result);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            var result = await _projectService.GetListAsync(pageRequest);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            var result = await _projectService.GetById(id);
            return Ok(result);
        }
    }
}
