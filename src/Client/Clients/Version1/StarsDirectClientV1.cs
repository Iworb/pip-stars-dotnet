using System.Threading.Tasks;
using PipServices3.Commons.Data;
using PipServices3.Commons.Refer;
using PipServices3.Rpc.Clients;
using Stars.Data.Version1;
using Stars.Logic;

namespace Stars.Clients.Version1
{
    public class StarsDirectClientV1: DirectClient<IStarsController>, IStarsClientV1
    {
        public StarsDirectClientV1() : base()
        {
            _dependencyResolver.Put("controller", new Descriptor("stars", "controller", "*", "*", "1.0"));
        }

        public async Task<DataPage<StarV1>> GetStarsAsync(
            string correlationId, FilterParams filter, PagingParams paging)
        {
            using (Instrument(correlationId, "stars.get_stars"))
            {
                return await _controller.GetStarsAsync(correlationId, filter, paging);
            }
        }

        public async Task<StarV1> GetStarByIdAsync(string correlationId, string id)
        {
            using (Instrument(correlationId, "stars.get_star_by_id"))
            {
                return await _controller.GetStarByIdAsync(correlationId, id);
            }
        }

        public async Task<StarV1> CreateStarAsync(string correlationId, StarV1 star)
        {
            using (Instrument(correlationId, "stars.create_star"))
            {
                return await _controller.CreateStarAsync(correlationId, star);
            }
        }

        public async Task<StarV1> UpdateStarAsync(string correlationId, StarV1 star)
        {
            using (Instrument(correlationId, "stars.update_star"))
            {
                return await _controller.UpdateStarAsync(correlationId, star);
            }
        }

        public async Task<StarV1> DeleteStarByIdAsync(string correlationId, string id)
        {
            using (Instrument(correlationId, "stars.delete_star_by_id"))
            {
                return await _controller.DeleteStarByIdAsync(correlationId, id);
            }
        }
    }
}