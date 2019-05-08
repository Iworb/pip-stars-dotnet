using System.Threading.Tasks;
using PipServices3.Commons.Data;
using Stars.Data.Version1;

namespace Stars.Persistence
{
    public interface IStarsPersistence
    {
        Task<DataPage<StarV1>> GetPageByFilterAsync(string correlationId, FilterParams filter, PagingParams paging);
        Task<StarV1> GetOneByIdAsync(string correlationId, string id);
        Task<StarV1> CreateAsync(string correlationId, StarV1 item);
        Task<StarV1> UpdateAsync(string correlationId, StarV1 item);
        Task<StarV1> DeleteByIdAsync(string correlationId, string id);
    }
}
