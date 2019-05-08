using System.Runtime.Serialization;

namespace Stars.Data.Version1
{
    [DataContract]
    public class DeclinationV1
    {
        [DataMember(Name = "degree")]
        public double Degree { get; set; }

        [DataMember(Name = "minute")]
        public double Minute { get; set; }
        
        [DataMember(Name = "second")]
        public double Second { get; set; }
    }
}