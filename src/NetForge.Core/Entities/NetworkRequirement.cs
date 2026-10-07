using NetForge.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetForge.Core.Entities
{
    public class NetworkRequirement
    {
        public int Id { get; set; }

        public int ProjectId { get; set; } // Foreign key to the Project entity
        public decimal WirelssAreaSqMeters { get; set; } 
        public int DeviceCountMin { get; set; } 
        public int DeviceCountMax { get; set; } 
        public decimal? BudgetMin { get; set; } 
        public decimal BudgetMax { get; set; } 
        public SpeedTier RequiredSpeed { get; set; }
        public decimal ExpectedCableMeters { get; set; }
        public string? ExistingInfrastructure { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }

        public Project? Project { get; set; } // Navigation property to the Project entity


    }
}
