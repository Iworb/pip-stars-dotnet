using MongoDB.Bson.Serialization.Attributes;
using PipServices3.Commons.Data;
using Stars.Data.Version1;

namespace Stars.Persistence
{
    [BsonIgnoreExtraElements]
    public class StarsMongoDbSchema : IStringIdentifiable
    {
        [BsonElement("id")]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("stellar_class")]
        public string StellarClass { get; set; }
        [BsonElement("mn_apparent")]
        public double MagnitudeApparent { get; set; }
        [BsonElement("mn_absolute")]
        public double MagnitudeAbsolute { get; set; }
        [BsonElement("right_ascension_deg")]
        public double RightAscensionDeg { get; set; }
        [BsonElement("declination")]
        public DeclinationV1 Declination { get; set; }
        [BsonElement("distance")]
        public double Distance { get; set; }

    }
}
