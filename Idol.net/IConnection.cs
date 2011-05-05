using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Rbi.Search.Configuration;

namespace Rbi.Search
{
    public interface IConnection<TResultSet>
    {
        Query<TResultSet> GetQuery(Func<XElement, TResultSet> resultSetFactory);
        Field this[string fieldName] { get; }
        XElement GetXElement(Uri uri, string command, bool acceptCompression);
    }
}
