using NetForge.Core.DTOs;
using NetForge.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetForge.Core.Interfaces
{
    public interface INetworkRequirementService
    {
        Task<RequirementResponse?> GetByProjectId(int projectId);
        Task<RequirementResponse?> CreateAsync(int projectId, CreateRequirementRequest request);
        Task<RequirementResponse?> UpdateAsync(int projectId, UpdateRequirementRequest request);
        Task<bool> DeleteAsync(int id);

    }
}
