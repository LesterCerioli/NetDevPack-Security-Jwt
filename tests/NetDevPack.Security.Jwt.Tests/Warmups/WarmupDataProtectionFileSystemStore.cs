﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Security.Jwt.Core;

namespace NetDevPack.Security.Jwt.Tests.Warmups;

public class WarmupDataProtectionFileSystemStore : IWarmupTest
{
    private readonly DirectoryInfo _keysRepository;

    public WarmupDataProtectionFileSystemStore()
    {
        _keysRepository = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "keys"));
        if (!_keysRepository.Exists)
            _keysRepository.Create();
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        serviceCollection.AddMemoryCache();
        serviceCollection.AddDataProtection().PersistKeysToFileSystem(_keysRepository);
        serviceCollection.AddJwksManager();

        Services = serviceCollection.BuildServiceProvider();
    }
    public ServiceProvider Services { get; set; }

    public Task Clear()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        foreach (var fileInfo in _keysRepository.GetFiles())
        {
            try
            {
                fileInfo.Delete();
            }
            catch
            {
                // ignored
            }
        }

        return Task.CompletedTask;
    }
}