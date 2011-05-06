using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rbi.Search.Configuration
{
    public sealed class NumericField : Field
    {
        public NumericField(string fieldName)
        {
            Name = fieldName;
            FieldType = FieldType.Numeric;
        }
    }
}
