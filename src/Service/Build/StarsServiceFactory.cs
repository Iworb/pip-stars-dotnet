using PipServices3.Commons.Refer;
using PipServices3.Components.Build;
using Stars.Logic;
using Stars.Persistence;
using Stars.Services.Version1;

namespace Stars.Build
{
    public class StarsServiceFactory : Factory
    {
        public static Descriptor Descriptor = new Descriptor("stars", "factory", "service", "default", "1.0");
        public static Descriptor MemoryPersistenceDescriptor = new Descriptor("stars", "persistence", "memory", "*", "1.0");
        public static Descriptor MongoDbPersistenceDescriptor = new Descriptor("stars", "persistence", "mongodb", "*", "1.0");
        public static Descriptor ControllerDescriptor = new Descriptor("stars", "controller", "default", "*", "1.0");
        public static Descriptor HttpServiceDescriptor = new Descriptor("stars", "service", "http", "*", "1.0");


        public StarsServiceFactory()
        {
            RegisterAsType(MemoryPersistenceDescriptor, typeof(StarsMemoryPersistence));
            RegisterAsType(MongoDbPersistenceDescriptor, typeof(StarsMongoDbPersistence));
            RegisterAsType(ControllerDescriptor, typeof(StarsController));
            RegisterAsType(HttpServiceDescriptor, typeof(StarsHttpServiceV1));
        }
    }
}