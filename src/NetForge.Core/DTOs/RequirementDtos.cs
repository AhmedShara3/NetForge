using System;
using System.Collections.Generic;
using System.Text;
using NetForge.Core.Enums;


namespace NetForge.Core.DTOs
{
    // ----- REQUEST DTOs -----
    /// <summary>
    /// Data payload for creating requirements for a project.
    /// </summary>

    public class CreateRequirementRequest
    {
        public decimal WirelssAreaSqMeters { get; set; }
        public int DeviceCountMin { get; set; }
        public int DeviceCountMax { get; set; }
        public decimal? BudgetMin { get; set; }
        public decimal BudgetMax { get; set; }
        public SpeedTier RequiredSpeed { get; set; }
        public decimal ExpectedCableMeters { get; set; }
        public string? ExistingInfrastructure { get; set; }


    }

}
/// <summary>
/// Data payload for updating existing requirements.
/// </summary>

public class UpdateRequirementRequest
{
    public decimal WirelssAreaSqMeters { get; set; }
    public int DeviceCountMin { get; set; }
    public int DeviceCountMax { get; set; }
    public decimal? BudgetMin { get; set; }
    public decimal BudgetMax { get; set; }
    public SpeedTier RequiredSpeed { get; set; }
    public decimal ExpectedCableMeters { get; set; }
    public string? ExistingInfrastructure { get; set; }

}
// ----- RESPONSE DTO -----
/// <summary>
/// What the API returns to the frontend.
/// Converts enums into readable strings and shields the raw database entity.
/// </summary>
public class RequirementResponse
{
    public int Id { get; set; }
    public decimal WirelssAreaSqMeters { get; set; }
    public int DeviceCountMin { get; set; }
    public int DeviceCountMax { get; set; }
    public decimal? BudgetMin { get; set; }
    public decimal BudgetMax { get; set; }
    public string RequiredSpeed { get; set; } = string.Empty; // Convert enum to string for frontend
    public decimal ExpectedCableMeters { get; set; }
    public string? ExistingInfrastructure { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}


