using NetForge.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetForge.Core.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectResponse>> GetAllAsync();
        Task<ProjectResponse?> GetByIdAsync(int id);
        Task<ProjectResponse> CreateAsync(CreateProjectRequest request);
        Task<ProjectResponse?> UpdateAsync(int id, UpdateProjectRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
