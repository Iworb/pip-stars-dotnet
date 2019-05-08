using System.Threading.Tasks;
using PipServices3.Commons.Data;
using Stars.Data.Version1;

namespace Stars.Clients.Version1
{
    public interface IStarsClientV1
    {
        Task<DataPage<StarV1>> GetStarsAsync(string correlationId, FilterParams filter, PagingParams paging);
        Task<StarV1> GetStarByIdAsync(string correlationId, string id);
        Task<StarV1> CreateStarAsync(string correlationId, StarV1 star);
        Task<StarV1> UpdateStarAsync(string correlationId, StarV1 star);
        Task<StarV1> DeleteStarByIdAsync(string correlationId, string id);
    }
}