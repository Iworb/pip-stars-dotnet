using System;
using System.Threading.Tasks;
using PipServices3.Commons.Config;
using Xunit;

namespace Stars.Persistence
{
    public class StarsFilePersistenceTest
    {
        private StarsFilePersistence _persistence;
        private StarsPersistenceFixture _fixture;

        public StarsFilePersistenceTest()
        {
            ConfigParams config = ConfigParams.FromTuples(
                "path", "stars.json"
            );
            _persistence = new StarsFilePersistence();
            _persistence.Configure(config);
            _persistence.OpenAsync(null).Wait();
            _persistence.ClearAsync(null).Wait();

            _fixture = new StarsPersistenceFixture(_persistence);
        }

        [Fact]
        public async Task TestCrudOperationsAsync()
        {
            await _fixture.TestCrudOperationsAsync();
        }

        [Fact]
        public async Task TestGetWithFiltersAsync()
        {
            await _fixture.TestGetWithFiltersAsync();
        }
    }
}
