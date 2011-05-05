using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using Rbi.Search.Configuration;

namespace Rbi.Search
{
    public class IdolConnection<TResultSet> : IConnection<TResultSet>
    {
         #region Static Members
#pragma warning disable 0649
        [Import(typeof(IConfigurationFactory))]
        private static IConfigurationFactory configurationReader = new IdolConfiguration();
#pragma warning restore 0649
        #endregion

        private TimeSpan timeout = new TimeSpan(0, 0, 60);
        internal IConfiguration Configuration;

        internal IdolConnection(Uri idolActionUri, Uri idolAdminUri, Uri idolIndexUri)
        {
            Configuration = configurationReader.GetConfiguration(idolActionUri, idolAdminUri, idolIndexUri);
        }

        public Query<TResultSet> GetQuery(Func<XElement, TResultSet> resultSetFactory)
        {
            return new Query<TResultSet>(resultSetFactory, this);
        }

        public Field this[string fieldName]
        {
            get { return Configuration.GetFieldType(fieldName); }
        }

        /// <summary>
        /// Simple routine to retrieve HTTP Content as a Stream with
        /// optional compression.
        /// </summary>
        private Stream GetUrlStream(Uri uri, string command, bool acceptCompression)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri + command);
            request.KeepAlive = false;
            request.ServicePoint.ConnectionLimit = 200;
            request.Timeout = (int)timeout.TotalMilliseconds;

            if (acceptCompression)
            {
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
            }

            var response = (HttpWebResponse)request.GetResponse();

            Stream responseStream = GetDecompressedStream(response);
            return responseStream;
        }

        public XElement GetXElement(Uri uri, string command, bool acceptCompression)
        {
            using (var stream = GetUrlStream(uri, command, acceptCompression))
            {
                return XElement.Load(stream);
            }
        }

        /// <summary>
        /// Decompresses a web-response stream where the response is compressed
        /// </summary>
        /// <param name="response">Web response to work from</param>
        /// <returns>Decompressed stream where it was compressed, or
        /// original response stream when not compressed.</returns>
        private static Stream GetDecompressedStream(HttpWebResponse response)
        {
            Stream returnStream = response.GetResponseStream();
            if (response.ContentEncoding.ToLower().Contains("gzip"))
            {
                //Untested - left in just in case Autonomy supports GZip in the future,
                //but will need testing
                returnStream = new GZipStream(returnStream, CompressionMode.Decompress);
            }
            else if (response.ContentEncoding.ToLower().Contains("deflate"))
            {
                //Skip first two bytes, as per
                // http://www.chiramattel.com/george/blog/2007/09/09/deflatestream-block-length-does-not-match.html
                returnStream.ReadByte();
                returnStream.ReadByte();
                returnStream = new DeflateStream(returnStream, CompressionMode.Decompress);
            }

            return returnStream;
        }
    }
}
