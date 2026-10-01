using System;
using System.Collections.Generic;
using System.Text;
using NetForge.Core.Enums;

namespace NetForge.Core.Entities
{
    public class Project
    {
        public int Id { get; set; }

        // UserId here !!!!
        public string Name { get; set; }
        public SiteType SiteType { get; set; }
        public NetWorkSize NetWorkSize { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
