using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rbi.Search.Configuration
{
    internal enum FieldType
    {
        Composite,
        Text,
        Match,
        Numeric,
        Date,
        NumericDate,
        Sort,
        FieldCheck,
        Parametric
    }
}
