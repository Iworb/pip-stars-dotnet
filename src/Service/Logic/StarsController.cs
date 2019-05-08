using System.Threading.Tasks;
using PipServices3.Commons.Commands;
using PipServices3.Commons.Config;
using PipServices3.Commons.Data;
using PipServices3.Commons.Refer;
using Stars.Data.Version1;
using Stars.Persistence;

namespace Stars.Logic
{
    public class StarsController : IStarsController, IConfigurable, IReferenceable, ICommandable
    {
        private IStarsPersistence _persistence;
        private StarsCommandSet _commandSet;

        public StarsController()
        {
        }

        public void Configure(ConfigParams config)
        {
        }

        public void SetReferences(IReferences references)
        {
            _persistence = references.GetOneRequired<IStarsPersistence>(
                new Descriptor("stars", "persistence", "*", "*", "1.0")
            );
        }

        public CommandSet GetCommandSet()
        {
            if (_commandSet == null)
                _commandSet = new StarsCommandSet(this);
            return _commandSet;
        }

        public async Task<DataPage<StarV1>> GetStarsAsync(string correlationId, FilterParams filter,
            PagingParams paging)
        {
            return await _persistence.GetPageByFilterAsync(correlationId, filter, paging);
        }

        public async Task<StarV1> GetStarByIdAsync(string correlationId, string id)
        {
            return await _persistence.GetOneByIdAsync(correlationId, id);
        }

        public async Task<StarV1> CreateStarAsync(string correlationId, StarV1 star)
        {
            star.Id = star.Id ?? IdGenerator.NextLong();

            return await _persistence.CreateAsync(correlationId, star);
        }

        public async Task<StarV1> UpdateStarAsync(string correlationId, StarV1 star)
        {
            return await _persistence.UpdateAsync(correlationId, star);
        }

        public async Task<StarV1> DeleteStarByIdAsync(string correlationId, string id)
        {
            return await _persistence.DeleteByIdAsync(correlationId, id);
        }
    }
}