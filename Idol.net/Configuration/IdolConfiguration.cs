using System;
using System.Collections.Concurrent;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;
using System.Threading;

namespace Rbi.Search.Configuration
{
    [Export(typeof(IConfigurationFactory))]
    public class IdolConfiguration : IConfiguration, IConfigurationFactory
    {
        public Uri IdolActionUri { get; private set; }
        public Uri IdolAdminUri { get; private set; }
        public Uri IdolIndexUri { get; private set; }
        public Uri IdolConfigurationFileUri { get; private set; }

        private readonly Component component;
        private readonly Lazy<ConcurrentDictionary<string, Field>> fields;

        #region IConfigurationFactory methods
        private static readonly ConcurrentDictionary<Uri, IdolConfiguration> Configurations = new ConcurrentDictionary<Uri, IdolConfiguration>();

        internal IdolConfiguration()
        {
        }

        private IdolConfiguration(Uri idolActionUri, Uri idolAdminUri, Uri idolIndexUri, Uri idolConfigurationFileUri)
        {
            IdolActionUri = idolActionUri;
            IdolAdminUri = idolAdminUri;
            IdolIndexUri = idolIndexUri;
            IdolConfigurationFileUri = idolConfigurationFileUri;

            component = Component.Content;

            fields = new Lazy<ConcurrentDictionary<string, Field>>(ParseFieldsFromConfiguration, LazyThreadSafetyMode.PublicationOnly);
        }

        public IConfiguration GetConfiguration(Uri idolActionUri, Uri idolAdminUri, Uri idolIndexUri)
        {
            return Configurations.GetOrAdd(idolActionUri, new IdolConfiguration(idolActionUri, idolAdminUri, idolIndexUri, null));
        }
        #endregion

        public Component ComponentType
        {
            get { return component; }
        }

        private ConcurrentDictionary<string, Field> ParseFieldsFromConfiguration()
        {
            if (IdolConfigurationFileUri == null)
            {
                IdolConfigurationFileUri = new Uri(IdolAdminUri, "action=getconfig");
            }

            string configurationFileText = String.Empty;
            if (IdolConfigurationFileUri.IsFile)
            {
                using (var reader = File.OpenText(IdolConfigurationFileUri.ToString()))
                {
                    configurationFileText = reader.ReadToEnd();
                }
            }
            else
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(IdolConfigurationFileUri);
                httpWebRequest.Method = "GET";
                var response = (HttpWebResponse)httpWebRequest.GetResponse();
                var responseStream = response.GetResponseStream();
                using (var streamReader = new StreamReader(responseStream))
                {
                    configurationFileText = streamReader.ReadToEnd();
                }
            }

            return ParseConfiguration(configurationFileText);
        }

        private static ConcurrentDictionary<string, Field> ParseConfiguration(string configurationText)
        {
            return new ConcurrentDictionary<string, Field>();
        }

        public Field GetFieldType(string fieldName)
        {
            //Check cache first
            if (fields.Value.ContainsKey(fieldName))
            {
                return fields.Value[fieldName];
            }

            return default(Field);
        }
    }
}
