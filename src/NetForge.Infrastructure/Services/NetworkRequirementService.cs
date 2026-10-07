using System;
using System.Collections.Generic;
using System.Text;
using NetForge.Core.Interfaces;
using NetForge.Core.DTOs;
using NetForge.Core.Entities;
using NetForge.Infrastructure.Data;
using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;

namespace NetForge.Infrastructure.Services
{
    public class NetworkRequirementService : INetworkRequirementService
    {
        private NetForgeDbContext _db;

        public NetworkRequirementService(NetForgeDbContext db)
        {
            _db = db;
        }

        public async Task<RequirementResponse?> GetByProjectId(int projectId)
        {
            var requirement = await _db.NetworkRequirements.FirstOrDefaultAsync(r => r.ProjectId == projectId);
            if(requirement == null)
            {
                return null;
            }
            return MapToResponse(requirement);
        }

        public async Task<RequirementResponse?> CreateAsync(int projectId , CreateRequirementRequest request)
        {
           var projectExists = await _db.Projects.AnyAsync(p => p.Id == projectId);
            if(!projectExists)
            {
                return null;
            }
            // 2. Enforce 1:1 rule: Verify project does NOT already have requirements
            var alreadyExists = await _db.NetworkRequirements.AnyAsync(r => r.ProjectId == projectId);
            if (alreadyExists)
                throw new InvalidOperationException($"Project {projectId} already has network requirements defined.");

            var requirement = new NetworkRequirement
            {
                ProjectId = projectId,
                WirelssAreaSqMeters = request.WirelssAreaSqMeters,
                DeviceCountMin = request.DeviceCountMin,
                DeviceCountMax = request.DeviceCountMax,
                BudgetMin = request.BudgetMin,
                BudgetMax = request.BudgetMax,
                RequiredSpeed = request.RequiredSpeed,
                ExpectedCableMeters = request.ExpectedCableMeters,
                ExistingInfrastructure = request.ExistingInfrastructure,
                CreatedAt = DateTime.UtcNow
            };
            
            _db.NetworkRequirements.Add(requirement);
            await _db.SaveChangesAsync();

            return MapToResponse(requirement);
        }

        public async Task<RequirementResponse?> UpdateAsync(int projectId, UpdateRequirementRequest request)
        {
            var requirement = await _db.NetworkRequirements.FirstOrDefaultAsync(r => r.ProjectId == projectId);
            if (requirement == null)
            {
                return null;
            }
            requirement.WirelssAreaSqMeters = request.WirelssAreaSqMeters;
            requirement.DeviceCountMin = request.DeviceCountMin;
            requirement.DeviceCountMax = request.DeviceCountMax;
            requirement.BudgetMin = request.BudgetMin;
            requirement.BudgetMax = request.BudgetMax;
            requirement.RequiredSpeed = request.RequiredSpeed;
            requirement.ExpectedCableMeters = request.ExpectedCableMeters;
            requirement.ExistingInfrastructure = request.ExistingInfrastructure;
            requirement.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return MapToResponse(requirement);
        }

        public async Task<bool> DeleteAsync(int projectId)
        {
            var requirement = await _db.NetworkRequirements.FirstOrDefaultAsync(r => r.ProjectId == projectId);
            if (requirement == null)
            {
                return false;
            }
            _db.NetworkRequirements.Remove(requirement);
            await _db.SaveChangesAsync();
            return true;
        }

        public static RequirementResponse MapToResponse(NetworkRequirement requirement)
        {
            return new RequirementResponse
            {
                Id = requirement.Id,
                WirelssAreaSqMeters = requirement.WirelssAreaSqMeters,
                DeviceCountMin = requirement.DeviceCountMin,
                DeviceCountMax = requirement.DeviceCountMax,
                BudgetMin = requirement.BudgetMin,
                BudgetMax = requirement.BudgetMax,
                RequiredSpeed = requirement.RequiredSpeed.ToString(), // Convert enum to string
                ExpectedCableMeters = requirement.ExpectedCableMeters,
                ExistingInfrastructure = requirement.ExistingInfrastructure,
                CreatedAt = requirement.CreatedAt,
                UpdatedAt = requirement.UpdatedAt
            };
        }
    }
}
