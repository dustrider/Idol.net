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
