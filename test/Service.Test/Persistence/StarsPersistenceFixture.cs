using Stars.Data.Version1;
using PipServices3.Commons.Data;
using System.Threading.Tasks;
using Xunit;

namespace Stars.Persistence
{
    public class StarsPersistenceFixture
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
            Id = "2",
            Name = "α Centauri B",
            StellarClass = "K1V",
            MagnitudeApparent = 1.35,
            MagnitudeAbsolute = 5.71,
            RightAscensionDeg = -140.10383333,
            Declination = new DeclinationV1() {Degree = -60, Minute = 50, Second = 13.8},
            Distance = 4.37
        };

        private StarV1 STAR3 = new StarV1
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

        private IStarsPersistence _persistence;

        public StarsPersistenceFixture(IStarsPersistence persistence)
        {
            _persistence = persistence;
        }

        private async Task TestCreateStarsAsync()
        {
            // Create the first star
            var star = await _persistence.CreateAsync(null, STAR1);

            Assert.NotNull(star);
            Assert.Equal(STAR1.Name, star.Name);
            Assert.Equal(STAR1.StellarClass, star.StellarClass);
            Assert.NotNull(star.MagnitudeApparent);
            Assert.NotNull(star.MagnitudeAbsolute);
            Assert.NotNull(star.RightAscensionDeg);
            Assert.NotNull(star.Declination);
            Assert.NotNull(star.Distance);

            // Create the second star
            star = await _persistence.CreateAsync(null, STAR2);

            Assert.NotNull(star);
            Assert.Equal(STAR2.Name, star.Name);
            Assert.Equal(STAR2.StellarClass, star.StellarClass);
            Assert.NotNull(star.MagnitudeApparent);
            Assert.NotNull(star.MagnitudeAbsolute);
            Assert.NotNull(star.RightAscensionDeg);
            Assert.NotNull(star.Declination);
            Assert.NotNull(star.Distance);

            // Create the third star
            star = await _persistence.CreateAsync(null, STAR3);

            Assert.NotNull(star);
            Assert.Equal(STAR3.Name, star.Name);
            Assert.Equal(STAR3.StellarClass, star.StellarClass);
            Assert.NotNull(star.MagnitudeApparent);
            Assert.NotNull(star.MagnitudeAbsolute);
            Assert.NotNull(star.RightAscensionDeg);
            Assert.NotNull(star.Declination);
            Assert.NotNull(star.Distance);
        }

        public async Task TestCrudOperationsAsync()
        {
            // Create items
            await TestCreateStarsAsync();

            // Get all Stars
            var page = await _persistence.GetPageByFilterAsync(
                null,
                new FilterParams(),
                new PagingParams()
            );

            Assert.NotNull(page);
            Assert.Equal(3, page.Data.Count);

            var star1 = page.Data[0];

            // Update the star
            star1.Name = "α Centauri TEST";

            var star = await _persistence.UpdateAsync(null, star1);

            Assert.NotNull(star);
            Assert.Equal(star1.Id, star.Id);
            Assert.Equal("α Centauri TEST", star.Name);

            // Delete the star
            star = await _persistence.DeleteByIdAsync(null, star1.Id);

            Assert.NotNull(star);
            Assert.Equal(star1.Id, star.Id);

            // Try to get deleted star
            star = await _persistence.GetOneByIdAsync(null, star1.Id);

            Assert.Null(star);
        }

        public async Task TestGetWithFiltersAsync()
        {
            // Create items
            await TestCreateStarsAsync();

            // Filter by id
            var page = await _persistence.GetPageByFilterAsync(
                null,
                FilterParams.FromTuples(
                    "id", "1"
                ),
                new PagingParams()
            );

            Assert.Single(page.Data);

            // Filter by site_id
            page = await _persistence.GetPageByFilterAsync(
                null,
                FilterParams.FromTuples(
                    "name", "Eridani"
                ),
                new PagingParams()
            );

            Assert.Equal(1, page.Data.Count);
        }
    }
}