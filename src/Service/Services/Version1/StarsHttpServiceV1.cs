using PipServices3.Commons.Refer;
using PipServices3.Rpc.Services;

namespace Stars.Services.Version1
{
    public class StarsHttpServiceV1 : CommandableHttpService
    {
        public StarsHttpServiceV1()
            : base("v1/stars")
        {
            _dependencyResolver.Put("controller", new Descriptor("stars", "controller", "default", "*", "1.0"));
        }
    }
}