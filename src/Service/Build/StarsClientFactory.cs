using System;
using PipServices3.Commons.Refer;
using PipServices3.Components.Build;
using Stars.Clients.Version1;

namespace Stars.Build
{
    public class StarsClientFactory: Factory
    {
        public static Descriptor NullClientDescriptor = new Descriptor("stars", "client", "null", "*", "1.0");
        public static Descriptor DirectClientDescriptor = new Descriptor("stars", "client", "direct", "*", "1.0");
        public static Descriptor HttpClientDescriptor = new Descriptor("stars", "client", "http", "*", "1.0");

        public StarsClientFactory()
        {
            RegisterAsType(StarsClientFactory.NullClientDescriptor, typeof(StarsNullClientV1));
            RegisterAsType(StarsClientFactory.DirectClientDescriptor, typeof(StarsDirectClientV1));
            RegisterAsType(StarsClientFactory.HttpClientDescriptor, typeof(StarsHttpClientV1));
        }

        private void RegisterAsType(Descriptor nullClientDescriptor, Type p1)
        {
            throw new NotImplementedException();
        }
    }
}