using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace Rbi.Search.Configuration
{
    public interface IConfigurationFactory
    {
        IConfiguration GetConfiguration(Uri idolActionUri, Uri idolAdminUri, Uri idolIndexUri);
    }
}
