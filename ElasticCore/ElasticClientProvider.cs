using Nest;
using System.Configuration;

namespace ElasticCore
{
    public class ElasticClientProvider : IDisposable
    {
        public ElasticClient ElasticClient { get; set; }
        public string ElasticSearchHost { get; set; }

        public ElasticClientProvider()
        {
            ElasticSearchHost = ConfigurationManager.AppSettings["ElasticSearchHost"];
            ElasticClient = CreateElasticClient();
        }

        private ElasticClient CreateElasticClient()
        {
            ConnectionSettings? connectionSettings = new ConnectionSettings(new Uri(ElasticSearchHost))
                .BasicAuthentication(ConfigurationManager.AppSettings["ElasticSearchUser"], ConfigurationManager.AppSettings["ElasticSearchPassword"])
                .DisablePing()
                .DisableDirectStreaming(true)
                .SniffOnStartup(false)
                .SniffOnConnectionFault(false);

            return new ElasticClient(connectionSettings);
        }

        private bool _disposedValue;
        ~ElasticClientProvider() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }
                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        



    }
}
