using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rbi.Search.Configuration
{
    public sealed class TextField: Field
    {
        public TextField(string fieldName)
        {
            Name = fieldName;
            FieldType = FieldType.Text;
        }
    }
}
