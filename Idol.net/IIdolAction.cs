using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rbi.Search
{
    public interface IIdolAction<out TResultSet>
    {
        string GetCommand();
        TResultSet Execute();
    }
}
