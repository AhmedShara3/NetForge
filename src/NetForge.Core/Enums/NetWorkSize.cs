using System;
using System.Collections.Generic;
using System.Text;

namespace NetForge.Core.Enums
{
    /// <summary>
    /// The scale of the network, based on device count range.
    /// Small = 10-100 devices, Medium = 100-1000, Large = 1000+.
    /// </summary>
    public enum NetworkSize
    {
        Small = 0, // Small network size
        Medium = 1, // Medium network size
        Large = 2, // Large network size
        
    }
}
