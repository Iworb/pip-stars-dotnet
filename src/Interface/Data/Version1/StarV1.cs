using System.Runtime.Serialization;
using PipServices3.Commons.Data;

namespace Stars.Data.Version1
{
    [DataContract]
    public class StarV1 : IStringIdentifiable
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "stellar_class")]
        public string StellarClass { get; set; }

        [DataMember(Name = "mn_apparent")]
        public double MagnitudeApparent { get; set; }
        
        [DataMember(Name = "mn_absolute")]
        public double MagnitudeAbsolute { get; set; }
        
        [DataMember(Name = "right_ascension_deg")]
        public double RightAscensionDeg { get; set; }

        [DataMember(Name = "declination")]
        public DeclinationV1 Declination { get; set; }
        
        [DataMember(Name = "distance")]
        public double Distance { get; set; }
    }
}