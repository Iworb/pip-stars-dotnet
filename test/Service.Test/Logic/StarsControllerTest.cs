using PipServices3.Commons.Refer;
using Xunit;
using PipServices3.Commons.Data;
using PipServices3.Commons.Config;
using Stars.Persistence;
using Stars.Data.Version1;
using System;
using System.Threading.Tasks;

namespace Stars.Logic
{
    public class StarsControllerTest: IDisposable
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

        private StarsController _controller;
        private StarsMemoryPersistence _persistence;

        public StarsControllerTest()
        {
            _persistence = new StarsMemoryPersistence();
            _persistence.Configure(new ConfigParams());

            _controller = new StarsController();

            var references = References.FromTuples(
                new Descriptor("stars", "persistence", "memory", "*", "1.0"), _persistence,
                new Descriptor("stars", "controller", "default", "*", "1.0"), _controller
            );

            _controller.SetReferences(references);

            _persistence.OpenAsync(null).Wait();
        }

        public void Dispose()
        {
            _persistence.CloseAsync(null).Wait();
        }

        [Fact]
        public async Task TestCrudOperationsAsync()
        {
            // Create the first star
            var star = await _controller.CreateStarAsync(null, STAR1);

            Assert.NotNull(star);
            Assert.Equal(STAR1.Name, star.Name);
            Assert.Equal(STAR1.StellarClass, star.StellarClass);
            Assert.NotNull(star.MagnitudeApparent);
            Assert.NotNull(star.MagnitudeAbsolute);
            Assert.NotNull(star.RightAscensionDeg);
            Assert.NotNull(star.Declination);
            Assert.NotNull(star.Distance);

            // Create the second star
            star = await _controller.CreateStarAsync(null, STAR2);

            Assert.NotNull(star);
            Assert.Equal(STAR2.Name, star.Name);
            Assert.Equal(STAR2.StellarClass, star.StellarClass);
            Assert.NotNull(star.MagnitudeApparent);
            Assert.NotNull(star.MagnitudeAbsolute);
            Assert.NotNull(star.RightAscensionDeg);
            Assert.NotNull(star.Declination);
            Assert.NotNull(star.Distance);

            // Get all stars
            var page = await _controller.GetStarsAsync(
                null,
                new FilterParams(),
                new PagingParams()
            );

            Assert.NotNull(page);
            Assert.Equal(2, page.Data.Count);

            var star1 = page.Data[0];

            // Update the star
            star1.Name = "α Centauri TEST";

            star = await _controller.UpdateStarAsync(null, star1);

            Assert.NotNull(star);
            Assert.Equal(star1.Id, star.Id);
            Assert.Equal("α Centauri TEST", star.Name);

            // Delete the star
            star = await _controller.DeleteStarByIdAsync(null, star1.Id);

            Assert.NotNull(star);
            Assert.Equal(star1.Id, star.Id);

            // Try to get deleted star
            star = await _controller.GetStarByIdAsync(null, star1.Id);

            Assert.Null(star);
        }

    }
}
