using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using NetDevPack.Security.JwtSigningCredentials.DefaultStore;

namespace NetDevPack.Security.JwtSigningCredentials.Tests.Warmups
{
    public class WarmupDataProtectionStore : IWarmupTest
    {
        private readonly DirectoryInfo _keysRepository;

        public WarmupDataProtectionStore()
        {
            _keysRepository = DefaultKeyStorageDirectories.Instance.GetKeyStorageDirectoryForAzureWebSites();
            if (_keysRepository == null)
                _keysRepository = DefaultKeyStorageDirectories.Instance.GetKeyStorageDirectory();

            if (!_keysRepository.Exists)
                _keysRepository.Create();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            serviceCollection.AddMemoryCache();
            serviceCollection.AddDataProtection().PersistKeysToFileSystem(_keysRepository);
            serviceCollection.AddJwksManager().PersistKeysToDataProtection();

            Services = serviceCollection.BuildServiceProvider();
        }
        public ServiceProvider Services { get; set; }

        public void Clear()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            foreach (var fileInfo in _keysRepository.GetFiles("*jw*.xml"))
            {
                fileInfo.Delete();
            }
        }
    }
}