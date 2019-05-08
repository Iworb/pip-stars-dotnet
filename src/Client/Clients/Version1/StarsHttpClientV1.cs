using System.Threading.Tasks;
using PipServices3.Commons.Data;
using PipServices3.Rpc.Clients;
using Stars.Data.Version1;

namespace Stars.Clients.Version1
{
    public class StarsHttpClientV1: CommandableHttpClient, IStarsClientV1
    {
        public StarsHttpClientV1()
            : base("v1/stars")
        { }

        public async Task<DataPage<StarV1>> GetStarsAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            return await CallCommandAsync<DataPage<StarV1>>(
                "get_stars",
                correlationId,
                new
                {
                    filter = filter,
                    paging = paging
                }
            );
        }

        public async Task<StarV1> GetStarByIdAsync(string correlationId, string id)
        {
            return await CallCommandAsync<StarV1>(
                "get_star_by_id",
                correlationId,
                new
                {
                    star_id = id
                }
            );
        }

        public async Task<StarV1> CreateStarAsync(string correlationId, StarV1 star)
        {
            return await CallCommandAsync<StarV1>(
                "create_star",
                correlationId,
                new
                {
                    star = star
                }
            );
        }

        public async Task<StarV1> UpdateStarAsync(string correlationId, StarV1 star)
        {
            return await CallCommandAsync<StarV1>(
                "update_star",
                correlationId,
                new
                {
                    star = star
                }
            );
        }

        public async Task<StarV1> DeleteStarByIdAsync(string correlationId, string id)
        {
            return await CallCommandAsync<StarV1>(
                "delete_star_by_id",
                correlationId,
                new
                {
                    star_id = id
                }
            );
        }

    }
}