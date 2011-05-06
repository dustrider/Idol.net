using System.Xml.Linq;

namespace Rbi.Search.Formatters
{
    public class RawResultSet
    {
        public XElement Result { get; private set; }

        public static RawResultSet FormatResults(XElement idolResults)
        {
            return new RawResultSet {Result = idolResults};
        }
    }
}
