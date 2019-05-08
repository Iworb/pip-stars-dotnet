using System;
using System.Threading.Tasks;
using PipServices3.Commons.Config;
using Xunit;

namespace Stars.Persistence
{
    public class MemoryStarsPersistenceTest: IDisposable
    {
        public StarsMemoryPersistence _persistence;
        public StarsPersistenceFixture _fixture;

        public MemoryStarsPersistenceTest()
        {
            _persistence = new StarsMemoryPersistence();
            _persistence.Configure(new ConfigParams());

            _fixture = new StarsPersistenceFixture(_persistence);

            _persistence.OpenAsync(null).Wait();
        }

        public void Dispose()
        {
            _persistence.CloseAsync(null).Wait();    
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
