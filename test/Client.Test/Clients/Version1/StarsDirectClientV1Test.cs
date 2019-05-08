using System.Threading.Tasks;
using PipServices3.Commons.Refer;
using Stars.Clients.Version1;
using Stars.Persistence;
using Xunit;

namespace Stars.Logic
{
    public class StarsDirectClientV1Test
    {
        private StarsMemoryPersistence _persistence;
        private StarsController _controller;
        private StarsDirectClientV1 _client;
        private StarsClientV1Fixture _fixture;

        public StarsDirectClientV1Test()
        {
            _persistence = new StarsMemoryPersistence();
            _controller = new StarsController();
            _client = new StarsDirectClientV1();

            IReferences references = References.FromTuples(
                new Descriptor("stars", "persistence", "memory", "default", "1.0"), _persistence,
                new Descriptor("stars", "controller", "default", "default", "1.0"), _controller,
                new Descriptor("stars", "client", "direct", "default", "1.0"), _client
            );

            _controller.SetReferences(references);

            _client.SetReferences(references);

            _fixture = new StarsClientV1Fixture(_client);

            _client.OpenAsync(null).Wait();
        }

        [Fact]
        public async Task TestCrudOperationsAsync()
        {
            await _fixture.TestCrudOperationsAsync();
        }
    }
}