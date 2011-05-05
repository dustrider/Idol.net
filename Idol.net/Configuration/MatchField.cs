using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rbi.Search.Configuration;

namespace Rbi.Search.Configuration
{
    public sealed class MatchField<T> : Field
    {
        public MatchField(string fieldName)
        {
            Name = fieldName;
            FieldType = FieldType.Match;
        }
    }
}
