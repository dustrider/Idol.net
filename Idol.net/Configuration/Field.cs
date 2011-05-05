using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rbi.Search.Configuration
{
    public class Field
    {
        public virtual string Name { get; protected internal set; }
        internal FieldType FieldType { get; set; }

        internal Field()
        {

        }

        //internal Field(IConfiguration configuration, string fieldName)
        //{
            //Name = fieldName;
            //FieldType = configuration.GetFieldType(fieldName);
        //}

        /*public static Term operator ==(Field<T> field, string value)
        {
            switch (field.FieldType)
            {
                case FieldType.Match:
                    return new Term() { TermType = TermType.Match, Field = field, Value = value };
                case FieldType.Numeric:
                    return new Term() { TermType = TermType.Equal, Field = field, Value = value };
                default:
                    return new Term() { TermType = TermType.Equal, Field = field, Value = value };
            }
        }

        public static Term operator >(Field<T> field, string value)
        {
            switch (field.FieldType)
            {
                case FieldType.Match:
                case FieldType.Numeric:
                    return new Term() { TermType = TermType.Greater, Field = field, Value = value };
                default:
                    return new Term() { TermType = TermType.Greater, Field = field, Value = value };
            }
        }

        public static Term operator <(Field<T> field, string value)
        {
            switch (field.FieldType)
            {
                case FieldType.Match:
                case FieldType.Numeric:
                    return new Term() { TermType = TermType.Less, Field = field, Value = value };
                default:
                    return new Term() { TermType = TermType.Less, Field = field, Value = value };
            }
        }

        public static Term operator !=(Field<T> field, string value)
        {
            switch (field.FieldType)
            {
                case FieldType.Match:
                    return new Term() { TermType = TermType.Match, Field = field, Value = value };
                case FieldType.Numeric:
                    return new Term() { TermType = TermType.Equal, Field = field, Value = value };
                default:
                    return new Term() { TermType = TermType.Equal, Field = field, Value = value };
            }
        }*/
    }
}
