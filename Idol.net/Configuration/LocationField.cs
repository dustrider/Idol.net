namespace Rbi.Search.Configuration
{
    public class LocationField : Field
    {
        public override string Name
        {
            get { return LatitudeField.Name + ":" + LongitudeField.Name; }
        }
        public NumericField LongitudeField { get; protected set; }
        public NumericField LatitudeField { get; protected set; }

        public LocationField(string latitudeFieldName, string longitudeFieldName)
        {
            LatitudeField = new NumericField(latitudeFieldName);
            LongitudeField = new NumericField(longitudeFieldName);
            FieldType = FieldType.Composite;
        }
    }
}
