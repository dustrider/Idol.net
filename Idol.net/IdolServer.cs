﻿using System;

namespace Rbi.Search
{
    public class IdolServer
    {
        public static IConnection<TResultSet> GetInstance<TResultSet>(Uri idolActionUri, Uri idolAdminUri, Uri idolIndexUri)
        {
            return new IdolConnection<TResultSet>(idolActionUri, idolAdminUri, idolIndexUri);
        }
    }
}
