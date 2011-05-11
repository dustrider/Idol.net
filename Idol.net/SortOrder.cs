using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rbi.Search
{
    /// <summary>
    /// Determines the display order of results
    /// </summary>
    internal enum SortOrder
    {
        /// <summary>
        /// Results are displayed unsorted
        /// </summary>
        Off,
        /// <summary>
        /// Results are displayed in order of the value in their AutnRankType field. The document with the highest AutnRankType field value is listed first
        /// </summary>
        AutnRank,
        /// <summary>
        /// If you have added Cluster=true to the action, the Cluster sort option allows you to display results in order of cluster (in decreasing cluster ID order)
        /// </summary>
        Cluster,
        /// <summary>
        /// Results are displayed in order of database number (in increasing order). The database numbers are defined in IDOL server's configuration file
        /// </summary>
        Database,
        /// <summary>
        /// Results are displayed in order of their date (the date contained in the results' DateType fields). The most recent document is listed first. If several documents have the same date, their display order is determined by their autn:docid (document ID) number (the highest autn:docid is listed first)
        /// </summary>
        Date,
        /// <summary>
        /// Results are displayed according to their distance from a specified point using Cartesian coordinates (X/Y). The option has the following format
        /// </summary>
        Distcartesian,
        /// <summary>
        /// Results are displayed according to their distance from a specified point using spherical coordinates (latitude/longitude). The option has the following format
        /// </summary>
        Distspherical,
        /// <summary>
        /// Results aredisplayed in order of their autn:docid (document ID) number. The document with the highest autn:docid is listed first
        /// </summary>
        DocIdDecreasing,
        /// <summary>
        /// Results are displayed in order of their autn:docid (document ID) number. The document with the lowest autn:docid is listed first
        /// </summary>
        DocIdIncreasing,
        /// <summary>
        /// Results are displayed in random order
        /// </summary>
        Random,
        /// <summary>
        /// Results are displayed in order of their relevance (the document with the highest is listed first). If documents have the same relevance, their display order is determined by their autn:docid (document ID) number (the highest autn:docid is listed first)
        /// </summary>
        Relevance,
        /// <summary>
        /// Results are displayed in order of their date (the date contained in the results' DateType fields). The oldest document is listed first. If several documents have the same date, their display order is determined by their autn:docid (document ID) number (the highest autn:docid is listed first)
        /// </summary>
        ReverseDate,
        /// <summary>
        /// Results are displayed in order of their relevance (the document with the lowest is listed first). If documents have the same relevance, their display order is determined by their autn:docid (document ID) number (the lowest autn:docid is listed first)
        /// </summary>
        ReverseRelevance
    }
}
