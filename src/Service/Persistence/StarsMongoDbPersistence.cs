using System.Threading.Tasks;
using MongoDB.Driver;
using PipServices3.Commons.Data;
using PipServices3.MongoDb.Persistence;
using Stars.Data.Version1;

namespace Stars.Persistence
{
    public class StarsMongoDbPersistence : IdentifiableMongoDbPersistence<StarV1, string>, IStarsPersistence
    {
        public StarsMongoDbPersistence()
            : base("stars")
        { }

        private new FilterDefinition<StarV1> ComposeFilter(FilterParams filterParams)
        {
            filterParams = filterParams ?? new FilterParams();

            var builder = Builders<StarV1>.Filter;
            var filter = builder.Empty;

            var id = filterParams.GetAsNullableString("id");
            if (!string.IsNullOrEmpty(id))
                filter &= builder.Eq(b => b.Id, id);
            
            var name = filterParams.GetAsNullableString("name");
            if (!string.IsNullOrEmpty(name))
                filter &= builder.Where(b => b.Name.Contains(name));
            
            var stellarClass = filterParams.GetAsNullableString("stellar_class");
            if (!string.IsNullOrEmpty(stellarClass))
                filter &= builder.Where(b => b.StellarClass.Contains(stellarClass));

            return filter;
        }

        public async Task<DataPage<StarV1>> GetPageByFilterAsync(
            string correlationId, FilterParams filter, PagingParams paging)
        {
            return await GetPageByFilterAsync(correlationId, ComposeFilter(filter), paging);
        }
    }
}

