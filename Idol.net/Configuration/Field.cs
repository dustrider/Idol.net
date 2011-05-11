namespace Rbi.Search.Configuration
{
    public class Field
    {
        public virtual string Name { get; protected internal set; }
        internal FieldType FieldType { get; set; }

        internal Field()
        {

        }

        /*public static Term operator ==(Field<T> field, string value)
        {

        }

        public static Term operator >(Field<T> field, string value)
        {

        }

        public static Term operator <(Field<T> field, string value)
        {

        }

        public static Term operator !=(Field<T> field, string value)
        {

        }*/
    }
}
