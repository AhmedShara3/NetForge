using System;
using System.Collections.Generic;
using System.Text;

namespace NetForge.Core.Enums
{
    /// <summary>
    /// SiteType enum represents different types of sites.
    /// 
    public enum SiteType
    {
        Hospital = 0, // Reliablity + POE + Redundancy
        School = 1,  // 
        Factory = 2, // Durability + PoE
        Company = 3,  //Security + Performance
        Bank = 4,  // Security + Performance + POE

    }

}
