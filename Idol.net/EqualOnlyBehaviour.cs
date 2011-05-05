using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rbi.Search
{
    public enum EqualOnlyBehaviour
    {
        /// <summary>
        /// Only return documents that match the criteria AND documents that contain the specified field
        /// </summary>
        EnsureFieldExists,
        /// <summary>
        /// Return documents that match the criteria OR documents that don't contain the specified field
        /// </summary>
        SkipFieldCheck
    }
}
