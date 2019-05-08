using System.Threading.Tasks;
using PipServices3.Commons.Data;
using Stars.Data.Version1;

namespace Stars.Logic
{
    public interface IStarsController
    {
        Task<DataPage<StarV1>> GetStarsAsync(string correlationId, FilterParams filter, PagingParams paging);
        Task<StarV1> GetStarByIdAsync(string correlationId, string id);
        Task<StarV1> CreateStarAsync(string correlationId, StarV1 beacon);
        Task<StarV1> UpdateStarAsync(string correlationId, StarV1 beacon);
        Task<StarV1> DeleteStarByIdAsync(string correlationId, string id);
    }
}