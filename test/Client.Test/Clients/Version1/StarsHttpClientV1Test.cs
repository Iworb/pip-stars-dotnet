using System.Threading.Tasks;
using PipServices3.Commons.Config;
using PipServices3.Commons.Refer;
using Stars.Logic;
using Stars.Persistence;
using Stars.Services.Version1;
using Xunit;

namespace Stars.Clients.Version1
{
    public class StarsHttpClientV1Test
    {
        private static readonly ConfigParams HttpConfig = ConfigParams.FromTuples(
            "connection.protocol", "http",
            "connection.host", "localhost",
            "connection.port", 8080
        );

        private StarsMemoryPersistence _persistence;
        private StarsController _controller;
        private StarsHttpClientV1 _client;
        private StarsHttpServiceV1 _service;
        private StarsClientV1Fixture _fixture;

        public StarsHttpClientV1Test()
        {
            _persistence = new StarsMemoryPersistence();
            _controller = new StarsController();
            _client = new StarsHttpClientV1();
            _service = new StarsHttpServiceV1();

            IReferences references = References.FromTuples(
                new Descriptor("stars", "persistence", "memory", "default", "1.0"), _persistence,
                new Descriptor("stars", "controller", "default", "default", "1.0"), _controller,
                new Descriptor("stars", "client", "http", "default", "1.0"), _client,
                new Descriptor("stars", "service", "http", "default", "1.0"), _service
            );

            _controller.SetReferences(references);

            _service.Configure(HttpConfig);
            _service.SetReferences(references);

            _client.Configure(HttpConfig);
            _client.SetReferences(references);

            _fixture = new StarsClientV1Fixture(_client);

            _service.OpenAsync(null).Wait();
            _client.OpenAsync(null).Wait();
        }

        [Fact]
        public async Task TestCrudOperationsAsync()
        {
            await _fixture.TestCrudOperationsAsync();
        }
    }
}