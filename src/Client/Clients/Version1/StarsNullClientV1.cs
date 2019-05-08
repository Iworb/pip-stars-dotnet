using System.Threading.Tasks;
using PipServices3.Commons.Data;
using Stars.Data.Version1;

namespace Stars.Clients.Version1
{
    public class StarsNullClientV1: IStarsClientV1
    {
        public async Task<DataPage<StarV1>> GetStarsAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            return await Task.FromResult(new DataPage<StarV1>());
        }

        public async Task<StarV1> GetStarByIdAsync(string correlationId, string id)
        {
            return await Task.FromResult(new StarV1());
        }

        public async Task<StarV1> CreateStarAsync(string correlationId, StarV1 star)
        {
            return await Task.FromResult(new StarV1());
        }

        public async Task<StarV1> UpdateStarAsync(string correlationId, StarV1 star)
        {
            return await Task.FromResult(new StarV1());
        }

        public async Task<StarV1> DeleteStarByIdAsync(string correlationId, string id)
        {
            return await Task.FromResult(new StarV1());
        }
    }
}