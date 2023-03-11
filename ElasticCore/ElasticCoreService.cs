using Models;
using Nest;

namespace ElasticCore
{
    public class ElasticCoreService<T> : IDisposable, IElasticCoreService<T> where T : BaseModel
    {
        public Task CheckExistsAndInsertLogAsync(T logModel, string indexName)
        {
            using (ElasticClientProvider provider = new ElasticClientProvider())
            {
                ElasticClient elasticClient = provider.ElasticClient;
                if (!elasticClient.Indices.Exists(indexName).Exists)
                {
                    string? newIndexName = indexName + DateTime.Now.Ticks;
                    IndexSettings? indexSettings = new IndexSettings();
                    indexSettings.NumberOfReplicas = 1;
                    indexSettings.NumberOfShards = 3;

                    var response = elasticClient.Indices.CreateAsync(newIndexName,
                        index => index.Map<T>(m => m.AutoMap())
                            .InitializeUsing(new IndexState())
                            .Aliases(a => a.Alias(indexName))
                        );
                }
                return elasticClient.IndexAsync<T>(logModel, idx => idx.Index(indexName));
            }
        }


        public IReadOnlyCollection<T> SearchLog(int rowCount)
        {
            throw new NotImplementedException();
        }


        private bool _disposedValue;

        ~ElasticCoreService() => Dispose(false);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
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
