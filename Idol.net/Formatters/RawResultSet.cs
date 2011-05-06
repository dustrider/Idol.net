using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Rbi.Search.Formatters
{
    public class RawResultSet
    {
        public XElement Result { get; private set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Parametrics")]
        //public XElement Parametrics { get; private set; }

        public static RawResultSet FormatResults(XElement idolResults)
        {
            return new RawResultSet {Result = idolResults};
        }
    }
}
