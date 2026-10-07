using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetForge.Core.Interfaces;
using NetForge.Core.DTOs;
using Microsoft.EntityFrameworkCore.Query;


namespace NetForge.Api.Controllers
{
    /// <summary>
    /// Endpoints for managing Network Requirements for a specific project.
    /// Nested under /api/projects/{projectId}/requirements
    /// </summary>
    [ApiController]
    [Route("api/projects/{projectId}/requirements")]
    
    public class NetworkRequirementsController : ControllerBase
    {
        private readonly INetworkRequirementService _requirementService;
        private readonly IProjectService _projectService;
        public NetworkRequirementsController(INetworkRequirementService requirementService
            ,IProjectService projectService )
        {
            _requirementService = requirementService;
            _projectService = projectService;
        }

        /// <summary>
        /// GET /api/projects/{projectId}/requirements
        /// Fetches the requirements for the given project.
        /// </summary>
        
        [HttpGet]
        public async Task<ActionResult<RequirementResponse>> GetByProjectId(int projectId)
        {
            var project = await _projectService.GetByIdAsync(projectId);
            if(project == null)
            {
                return NotFound($"Project with ID {projectId} not found.");
            }

            var requirements = await _requirementService.GetByProjectId(projectId);
            if(requirements == null)
            {
                return NotFound($"No requirements found for project with ID {projectId}.");
            }

            return Ok(requirements);

        }
        /// <summary>
        /// POST /api/projects/{projectId}/requirements
        /// Creates the requirements for the given project (Step 3: Take Data).
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<RequirementResponse>> Create(int projectId, [FromBody] CreateRequirementRequest request)
        {
            if(request.WirelssAreaSqMeters<=0)
            {
                return BadRequest(new {message = "Wireless area must be a positive value."});
            }

            if (request.DeviceCountMin < 0 || request.DeviceCountMax < 0)
            {
                return BadRequest(new { message = "Invalid device count range: DeviceCountMax must be >= DeviceCountMin" });
            }
            if (request.BudgetMax < 0)
            {
                return BadRequest(new { message = "BudgetMax must be a positive value." });
            }
            if (request.BudgetMin.HasValue && request.BudgetMin.Value > request.BudgetMax)
                return BadRequest(new { message = "BudgetMin cannot exceed BudgetMax." });

            if (request.ExpectedCableMeters < 0)
                return BadRequest(new { message = "ExpectedCableMeters cannot be negative." });

            try
            {
                var created = await _requirementService.CreateAsync(projectId, request);
                if(created == null)
                {
                    return NotFound($"Project with ID {projectId} not found.");
                }
                return CreatedAtAction(nameof(GetByProjectId), new { projectId = projectId }, created);

            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        /// <summary>
        /// PUT /api/projects/{projectId}/requirements
        /// Updates the requirements for the project.
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<RequirementResponse>> Update(int projectId, [FromBody] UpdateRequirementRequest request)
        {
            if (request.WirelssAreaSqMeters <= 0)
            {
                return BadRequest(new { message = "Wireless area must be a positive value." });
            }
            if (request.DeviceCountMin < 0 || request.DeviceCountMax < 0)
            {
                return BadRequest(new { message = "Invalid device count range: DeviceCountMax must be >= DeviceCountMin" });
            }
            if (request.BudgetMax < 0)
            {
                return BadRequest(new { message = "BudgetMax must be a positive value." });
            }
            if (request.BudgetMin.HasValue && request.BudgetMin.Value > request.BudgetMax)
                return BadRequest(new { message = "BudgetMin cannot exceed BudgetMax." });
            if (request.ExpectedCableMeters < 0)
                return BadRequest(new { message = "ExpectedCableMeters cannot be negative." });
            var updated = await _requirementService.UpdateAsync(projectId, request);
            if (updated == null)
            {
                return NotFound($"No requirements found for project with ID {projectId}.");
            }
            return Ok(updated);
        }
        /// <summary>
        /// DELETE /api/projects/{projectId}/requirements
        /// Deletes the requirements for the project.
        /// </summary>
        [HttpDelete]
        public async Task<ActionResult> Delete(int projectId)
        {
            var deleted = await _requirementService.DeleteAsync(projectId);
            if (!deleted)
            {
                return NotFound($"No requirements found for project with ID {projectId}.");
            }
            return NoContent();
        }
    }
}
