﻿using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Security.Jwt.Core;
using NetDevPack.Security.Jwt.Core.Interfaces;

namespace NetDevPack.Security.Jwt.Tests.Warmups;

public class WarmupInMemoryStore : IWarmupTest
{
    private readonly IJsonWebKeyStore _store;

    public WarmupInMemoryStore()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddJwksManager().PersistKeysInMemory();
        Services = serviceCollection.BuildServiceProvider();

        _store = Services.GetRequiredService<IJsonWebKeyStore>();
    }

    public async Task Clear()
    {
        await _store.Clear();
    }
    public ServiceProvider Services { get; set; }
}