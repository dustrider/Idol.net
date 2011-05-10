using System.Globalization;
using Rbi.Search.Configuration;

namespace Rbi.Search
{
    public class Sort
    {
        public Sort NextSort { get; private set; }
        internal SortOrder? Order { get; private set; }
        internal SortMethod? Method { get; private set; }
        public Field SortField { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        internal Sort(SortOrder order)
        {
            Order = order;
        }

        internal Sort(Field field, SortMethod sortMethod)
        {
            SortField = field;
            Order = null;
            Method = sortMethod;
        }

        internal Sort(LocationField field, SortOrder order, double latitude, double longitude)
        {
            SortField = field;
            Order = order;
            Latitude = latitude;
            Longitude = longitude;
        }

        public Sort And(Sort sort)
        {
            NextSort = sort;
            return this;
        }

        public static Sort Unsorted = new Sort(SortOrder.Off);
        public static Sort ByRank = new Sort(SortOrder.AutnRank);
        public static Sort ByCluster = new Sort(SortOrder.Cluster);
        public static Sort ByDatabase = new Sort(SortOrder.Database);
        public static Sort ByDate = new Sort(SortOrder.Date);
        public static Sort ByCartesian(LocationField field, double x, double y)
        {
            return new Sort(field, SortOrder.Distcartesian, x, y);
        }
        public static Sort ByLocation(LocationField field, double latitude, double longitude)
        {
            return new Sort(field, SortOrder.Distcartesian, latitude, longitude);
        }        
        public static Sort ByInternalIdDecreasing = new Sort(SortOrder.DocIdDecreasing);
        public static Sort ByInternalIdIncreasing = new Sort(SortOrder.DocIdIncreasing);
        public static Sort Random = new Sort(SortOrder.Random);
        public static Sort ByRelevance = new Sort(SortOrder.Relevance);
        public static Sort ByReverseDate = new Sort(SortOrder.ReverseDate);
        public static Sort ByReverseRelevance = new Sort(SortOrder.ReverseRelevance);
        public static Sort ByFieldIncreasing(Field field)
        {
            return new Sort(field,
                            ((field.FieldType == FieldType.Numeric) || (field.FieldType == FieldType.NumericDate)
                                 ? SortMethod.NumberIncreasing
                                 : SortMethod.Alphabetical));
        }

        public static Sort ByFieldDecreasing(Field field)
        {
            return new Sort(field,
                            ((field.FieldType == FieldType.Numeric) || (field.FieldType == FieldType.NumericDate)
                                 ? SortMethod.NumberDecreasing
                                 : SortMethod.ReverseAlphabetical));
        }

        public override string ToString()
        {
            string thisSort;

            //basic sort or location sort
            if (Order.HasValue)
            {
                if (SortField == null)
                {
                    thisSort = Order.Value.ToString();
                }
                else
                {
                    //Distspherical{lat,long}:Latfield:Longfield
                    //Distcartesian{coordX,coordY}:X:Y
                    var locationField = (LocationField) SortField;
                    thisSort = string.Format(CultureInfo.InvariantCulture, "{0}{{{1},{2}}}:{3}:{4}", Order.Value,
                                             Latitude, Longitude, locationField.LatitudeField,
                                             locationField.LongitudeField);
                }
            }
            else //Field sort
            {
                thisSort = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", SortField.Name, Method);
            }

            if ((NextSort != this) && (NextSort != null))
            {
                return thisSort + "+" + NextSort;
            }

            return thisSort;
        }
    }
}
