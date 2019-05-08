using System.Threading.Tasks;
using PipServices3.Commons.Data;
using Stars.Data.Version1;
using Xunit;

namespace Stars.Clients.Version1
{
    public class StarsClientV1Fixture
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

        private IStarsClientV1 _client;

        public StarsClientV1Fixture(IStarsClientV1 client)
        {
            _client = client;
        }

        public async Task TestCrudOperationsAsync()
        {
            // Create the first star
            var star = await _client.CreateStarAsync(null, STAR1);

            Assert.NotNull(star);
            Assert.Equal(STAR1.Name, star.Name);
            Assert.Equal(STAR1.StellarClass, star.StellarClass);
            Assert.NotNull(star.MagnitudeApparent);
            Assert.NotNull(star.MagnitudeAbsolute);
            Assert.NotNull(star.RightAscensionDeg);
            Assert.NotNull(star.Declination);
            Assert.NotNull(star.Distance);

            // Create the second star
            star = await _client.CreateStarAsync(null, STAR2);

            Assert.NotNull(star);
            Assert.Equal(STAR2.Name, star.Name);
            Assert.Equal(STAR2.StellarClass, star.StellarClass);
            Assert.NotNull(star.MagnitudeApparent);
            Assert.NotNull(star.MagnitudeAbsolute);
            Assert.NotNull(star.RightAscensionDeg);
            Assert.NotNull(star.Declination);
            Assert.NotNull(star.Distance);

            // Get all stars
            var page = await _client.GetStarsAsync(
                null,
                new FilterParams(),
                new PagingParams()
            );

            Assert.NotNull(page);
            Assert.Equal(2, page.Data.Count);

            var star1 = page.Data[0];

            // Update the star
            star1.Name = "α Centauri TEST";

            star = await _client.UpdateStarAsync(null, star1);

            Assert.NotNull(star);
            Assert.Equal(star1.Id, star.Id);
            Assert.Equal("α Centauri TEST", star.Name);

            // Delete the star
            star = await _client.DeleteStarByIdAsync(null, star1.Id);

            Assert.NotNull(star);
            Assert.Equal(star1.Id, star.Id);

            // Try to get deleted star
            star = await _client.GetStarByIdAsync(null, star1.Id);

            Assert.Null(star);

            // Clean up for the second test
            await _client.DeleteStarByIdAsync(null, STAR2.Id);
        }
    }
}