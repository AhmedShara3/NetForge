namespace NetForge.Infrastructure.Services;

using Microsoft.EntityFrameworkCore;
using NetForge.Core.DTOs;
using NetForge.Core.Entities;
using NetForge.Core.Interfaces;
using NetForge.Infrastructure.Data;

/// <summary>
/// Implements IProjectService using EF Core.
/// This is where database operations happen — NEVER in the controller.
/// </summary>
public class ProjectService : IProjectService
{
    private readonly NetForgeDbContext _db;

    // Constructor injection: ASP.NET Core gives us the DbContext automatically
    public ProjectService(NetForgeDbContext db)
    {
        _db = db;
    }

    public async Task<List<ProjectResponse>> GetAllAsync()
    {
        var projects = await _db.Projects
            .OrderByDescending(p => p.CreatedAt)  // newest first
            .ToListAsync();

        return projects.Select(MapToResponse).ToList();
    }

    public async Task<ProjectResponse?> GetByIdAsync(int id)
    {
        var project = await _db.Projects.FindAsync(id);

        if (project is null)
            return null;

        return MapToResponse(project);
    }

    public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request)
    {
        var project = new Project
        {
            Name = request.Name.Trim(),
            SiteType = request.SiteType,
            NetworkSize = request.NetworkSize,
            CreatedAt = DateTime.UtcNow
        };

        _db.Projects.Add(project);
        await _db.SaveChangesAsync();

        return MapToResponse(project);
    }

    public async Task<ProjectResponse?> UpdateAsync(int id, UpdateProjectRequest request)
    {
        var project = await _db.Projects.FindAsync(id);

        if (project is null)
            return null;

        project.Name = request.Name.Trim();
        project.SiteType = request.SiteType;
        project.NetworkSize = request.NetworkSize;
        project.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return MapToResponse(project);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var project = await _db.Projects.FindAsync(id);

        if (project is null)
            return false;

        _db.Projects.Remove(project);
        await _db.SaveChangesAsync();

        return true;
    }

    // ----- PRIVATE HELPER -----

    /// <summary>
    /// Maps a Project entity to a ProjectResponse DTO.
    /// This is where we convert enum values (0, 1, 2) to readable strings ("Hospital", "Large").
    /// </summary>
    private static ProjectResponse MapToResponse(Project project)
    {
        return new ProjectResponse
        {
            Id = project.Id,
            Name = project.Name,
            SiteType = project.SiteType.ToString(),        // 0 → "Hospital"
            NetworkSize = project.NetworkSize.ToString(),   // 2 → "Large"
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt
        };
    }
}