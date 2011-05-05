using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Rbi.Search.Configuration;
using Rbi.Search.Formatters;

namespace API_Test_App
{
    public static class ExampleDocument
    {
        public static MatchField<int> ArticleId = new MatchField<int>("ARTICLEID");
        public static TextField Title = new TextField("DRETITLE");
        public static LocationField Location = new LocationField("LAT1", "LONG1");
        public static NumericField StatusId = new NumericField("STATUSID");
    }
}
