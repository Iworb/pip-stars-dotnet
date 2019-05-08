using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PipServices3.Commons.Data;
using PipServices3.Data.Persistence;
using Stars.Data.Version1;

namespace Stars.Persistence
{
    public class StarsMemoryPersistence : IdentifiableMemoryPersistence<StarV1, string>, IStarsPersistence
    {
        public StarsMemoryPersistence()
        {
            _maxPageSize = 1000;
        }

        private List<Func<StarV1, bool>> ComposeFilter(FilterParams filter)
        {
            filter = filter ?? new FilterParams();

            var id = filter.GetAsNullableString("id");
            var name = filter.GetAsNullableString("name");
            var stellarClass = filter.GetAsNullableString("stellar_class");

            return new List<Func<StarV1, bool>>() {
                (item) =>
                {
                    if (id != null && item.Id != id)
                        return false;
                    if (name != null && !item.Name.Contains(name))
                        return false;
                    if (stellarClass != null && !item.StellarClass.Contains(stellarClass))
                        return false;
                    return true;
                }
            };
        }

        public Task<DataPage<StarV1>> GetPageByFilterAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            return base.GetPageByFilterAsync(correlationId, ComposeFilter(filter), paging);
        }
    }
}
