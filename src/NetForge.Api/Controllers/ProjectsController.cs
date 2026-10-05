using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetForge.Core.DTOs;
using NetForge.Core.Interfaces;

namespace NetForge.Api.Controllers
{
    /// <summary>
    /// REST API endpoints for managing projects.
    /// The controller is THIN — it only routes HTTP requests to the service.
    /// Zero business logic here.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// GET /api/projects — Get all projects
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetAllAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project is null)
                return NotFound(new { message = "Project not found" });
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequest request)
        {
            var project = await _projectService.CreateAsync(request);


            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest(new { message = "Project name is required." });

            if (request.Name.Length > 200)
                return BadRequest(new { message = "Project name must be 200 characters or less." });


            return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProjectRequest request)
        {
            var project = await _projectService.UpdateAsync(id, request);

            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest(new { message = "Project name is required." });
            if (request.Name.Length > 200)
                return BadRequest(new { message = "Project name must be 200 characters or less." });
            
            return Ok(project);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _projectService.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Project not found" });

            return NoContent();
        }



    }
}
