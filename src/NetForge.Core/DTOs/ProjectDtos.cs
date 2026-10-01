using NetForge.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetForge.Core.DTOs
{
    public class  CreateProjecRequest
    {
        public string Name { get; set; } = string.Empty;
        public SiteType SiteType { get; set; }
        public NetWorkSize NetWorkSize { get; set; }

    }

    public class UpdateProjectRequest
    {
        public string Name { get; set; } = string.Empty;
        public SiteType SiteType { get; set; }
        public NetWorkSize NetWorkSize { get; set; }

    }

    public class ProjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public SiteType SiteType { get; set; }
        public NetWorkSize NetWorkSize { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
