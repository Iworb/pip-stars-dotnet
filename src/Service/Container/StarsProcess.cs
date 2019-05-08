using PipServices3.Container;
using PipServices3.Rpc.Build;
using Stars.Build;

namespace Stars.Container
{
    public class StarsProcess: ProcessContainer
    {
        public StarsProcess()
            : base("stars", "Stars microservice")
        {
            _factories.Add(new DefaultRpcFactory());
            _factories.Add(new StarsServiceFactory());
        }
    }
}