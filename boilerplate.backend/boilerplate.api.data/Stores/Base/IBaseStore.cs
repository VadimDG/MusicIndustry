using boilerplate.api.core.Models;
using System.Threading.Tasks;

namespace boilerplate.api.data.Stores
{
    public interface IBaseStore
    {   
        Task<EntriesQueryResponse<T>> GetEntries<T>(EntriesQueryRequest request);
        Task<EntriesQueryResponse<T>> GetEntries<T>(string sqlRequest);
        Task<EntryQueryResponse<T>> GetEntry<T, K>(EntryQueryRequest<K> request);
        Task<EntryQueryResponse<T>> GetEntry<T>(string sqlRequest);
        Task<K> CreateEntry<T, K>(CreateCommandRequest<T> request);
        Task<bool> UpdateEntry<T, K>(UpdateCommandRequest<T> request);
        Task<bool> DeleteEntry<T>(DeleteCommandRequest<T> request);
        Task<int> ExecuteAsync(string sqlRequest);
    }
}