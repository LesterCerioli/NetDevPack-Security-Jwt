using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Security.JwtSigningCredentials.Interfaces;

namespace NetDevPack.Security.JwtSigningCredentials.Tests.Warmups
{
    /// <summary>
    /// 
    /// </summary>
    public class WarmupInMemoryStore : IWarmupTest
    {
        private readonly IJsonWebKeyStore _jsonWebKeyStore;

        public WarmupInMemoryStore()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();

            serviceCollection.AddJwksManager().PersistKeysInMemory();
            Services = serviceCollection.BuildServiceProvider();
            _jsonWebKeyStore = Services.GetRequiredService<IJsonWebKeyStore>();
        }

        public void Clear()
        {
            _jsonWebKeyStore.Clear();
        }
        public ServiceProvider Services { get; set; }
    }
}