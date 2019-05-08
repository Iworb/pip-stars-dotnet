using PipServices3.Commons.Config;
using PipServices3.Commons.Convert;
using PipServices3.Commons.Data;
using PipServices3.Commons.Refer;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Stars.Persistence;
using Stars.Logic;
using Stars.Data.Version1;
using System.Threading;

namespace Stars.Services.Version1
{
    public class StarsHttpServiceV1Test
    {
        private StarV1 STAR1 = new StarV1
        {
            Id = "1",
            Name = "α Centauri A",
            StellarClass = "G2V",
            MagnitudeApparent = -0.01,
            MagnitudeAbsolute = 4.34,
            RightAscensionDeg = -140.09791667,
            Declination = new DeclinationV1() {Degree = -60, Minute = 50, Second = 2.3},
            Distance = 4.37
        };

        private StarV1 STAR2 = new StarV1
        {
            Id = "3",
            Name = "ε Eridani",
            StellarClass = "K2V",
            MagnitudeApparent = 3.72,
            MagnitudeAbsolute = 6.18,
            RightAscensionDeg = 53.23266667,
            Declination = new DeclinationV1() {Degree = -9, Minute = 27, Second = 29.7},
            Distance = 10.5
        };

        private static readonly ConfigParams HttpConfig = ConfigParams.FromTuples(
            "connection.protocol", "http",
            "connection.host", "localhost",
            "connection.port", "3000"
        );

        private StarsMemoryPersistence _persistence;
        private StarsController _controller;
        private StarsHttpServiceV1 _service;

        public StarsHttpServiceV1Test()
        {
            _persistence = new StarsMemoryPersistence();
            _controller = new StarsController();
            _service = new StarsHttpServiceV1();

            IReferences references = References.FromTuples(
                new Descriptor("stars", "persistence", "memory", "default", "1.0"), _persistence,
                new Descriptor("stars", "controller", "default", "default", "1.0"), _controller,
                new Descriptor("stars", "service", "http", "default", "1.0"), _service
            );

            _controller.SetReferences(references);

            _service.Configure(HttpConfig);
            _service.SetReferences(references);

            //_service.OpenAsync(null).Wait();
            // Todo: This is defect! Open shall not block the tread
            Task.Run(() => _service.OpenAsync(null));
            Thread.Sleep(1000); // Just let service a sec to be initialized
        }

        [Fact]
        public async Task TestCrudOperationsAsync()
        {
            // Create the first star
            var star = await Invoke<StarV1>("create_star", new { star = STAR1 });

            Assert.NotNull(star);
            Assert.Equal(STAR1.Name, star.Name);
            Assert.Equal(STAR1.StellarClass, star.StellarClass);
            Assert.NotNull(star.MagnitudeApparent);
            Assert.NotNull(star.MagnitudeAbsolute);
            Assert.NotNull(star.RightAscensionDeg);
            Assert.NotNull(star.Declination);
            Assert.NotNull(star.Distance);

            // Create the second star
            star = await Invoke<StarV1>("create_star", new { star = STAR2 });

            Assert.NotNull(star);
            Assert.Equal(STAR2.Name, star.Name);
            Assert.Equal(STAR2.StellarClass, star.StellarClass);
            Assert.NotNull(star.MagnitudeApparent);
            Assert.NotNull(star.MagnitudeAbsolute);
            Assert.NotNull(star.RightAscensionDeg);
            Assert.NotNull(star.Declination);
            Assert.NotNull(star.Distance);

            // Get all stars
            var page = await Invoke<DataPage<StarV1>>(
                "get_stars",
                new
                {
                    filter = new FilterParams(),
                    paging = new PagingParams()
                }
            );

            Assert.NotNull(page);
            Assert.Equal(2, page.Data.Count);

            var star1 = page.Data[0];

            // Update the star
            star1.Name = "α Centauri TEST";

            star = await Invoke<StarV1>("update_star", new { star = star1 });

            Assert.NotNull(star);
            Assert.Equal(star1.Id, star.Id);
            Assert.Equal("α Centauri TEST", star.Name);

            // Delete the star
            star = await Invoke<StarV1>("delete_star_by_id", new { star_id = star1.Id });

            Assert.NotNull(star);
            Assert.Equal(star1.Id, star.Id);

            // Try to get deleted star
            star = await Invoke<StarV1>("get_star_by_id", new { star_id = star1.Id });

            Assert.Null(star);
        }

        private static async Task<T> Invoke<T>(string route, dynamic request)
        {
            using (var httpClient = new HttpClient())
            {
                var requestValue = JsonConverter.ToJson(request);
                using (var content = new StringContent(requestValue, Encoding.UTF8, "application/json"))
                {
                    var response = await httpClient.PostAsync("http://localhost:3000/v1/stars/" + route, content);
                    var responseValue = response.Content.ReadAsStringAsync().Result;
                    return JsonConverter.FromJson<T>(responseValue);
                }
            }
        }
    }
}
