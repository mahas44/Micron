using Models;


namespace ElasticCore
{
    public interface IElasticCoreService<T> where T : BaseModel
    {
        public IReadOnlyCollection<T> SearchLog(int rowCount);
        public Task CheckExistsAndInsertLogAsync(T logModel, string indexName);
    }
}
