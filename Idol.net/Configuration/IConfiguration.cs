using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rbi.Search.Configuration
{
    public interface IConfiguration
    {
        Uri IdolActionUri { get; }
        Uri IdolAdminUri { get; }
        Uri IdolIndexUri { get; }
        Component ComponentType { get; }
        Field GetFieldType(string fieldName);
    }
}
