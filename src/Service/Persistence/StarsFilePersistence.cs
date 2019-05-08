using PipServices3.Commons.Config;
using PipServices3.Data.Persistence;
using Stars.Data.Version1;

namespace Stars.Persistence
{
    public class StarsFilePersistence : StarsMemoryPersistence
    {
        protected JsonFilePersister<StarV1> _persister;

        public StarsFilePersistence()
        {
            _persister = new JsonFilePersister<StarV1>();
            _loader = _persister;
            _saver = _persister;
        }

        public override void Configure(ConfigParams config)
        {
            base.Configure(config);
            _persister.Configure(config);
        }
    }
}
