using PipServices3.Commons.Convert;
using PipServices3.Commons.Validate;

namespace Stars.Data.Version1
{
    public class DeclinationV1Schema : ObjectSchema
    {
        public DeclinationV1Schema()
        {
            this.WithOptionalProperty("degree", TypeCode.Double);
            this.WithRequiredProperty("minute", TypeCode.Double);
            this.WithOptionalProperty("second", TypeCode.Double);
        }
    }
}