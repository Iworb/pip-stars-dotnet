using PipServices3.Commons.Convert;
using PipServices3.Commons.Validate;

namespace Stars.Data.Version1
{
    public class StarV1Schema : ObjectSchema
    {
        public StarV1Schema()
        {
            this.WithOptionalProperty("id", TypeCode.String);
            this.WithRequiredProperty("name", TypeCode.String);
            this.WithOptionalProperty("stellar_class", TypeCode.String);
            this.WithRequiredProperty("mn_apparent", TypeCode.Double);
            this.WithRequiredProperty("mn_absolute", TypeCode.Double);
            this.WithOptionalProperty("right_ascension_deg", TypeCode.Double);
            this.WithOptionalProperty("declination", new DeclinationV1Schema());
            this.WithOptionalProperty("distance", TypeCode.Double);
        }
    }
}