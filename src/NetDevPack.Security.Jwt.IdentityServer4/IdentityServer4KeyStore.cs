using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetDevPack.Security.Jwt.Core;
using NetDevPack.Security.Jwt.Core.Interfaces;

namespace NetDevPack.Security.Jwt.IdentityServer4;

internal class IdentityServer4KeyStore : IValidationKeysStore, ISigningCredentialStore
{
    private readonly IJwtService _keyService;
    private readonly IMemoryCache _memoryCache;
    private readonly IOptions<JwtOptions> _options;

    /// <summary>Constructor for IdentityServer4KeyStore.</summary>
    /// <param name="keyService"></param>
    /// <param name="memoryCache"></param>
    /// <param name="options"></param>
    public IdentityServer4KeyStore(IJwtService keyService, IMemoryCache memoryCache, IOptions<JwtOptions> options)
    {
        _keyService = keyService;
        _memoryCache = memoryCache;
        _options = options;
    }

    /// <summary>Returns the current signing key.</summary>
    /// <returns></returns>
    public Task<SigningCredentials> GetSigningCredentialsAsync()
    {
        return _keyService.GetCurrentSigningCredentials();
    }

    /// <summary>Returns all the validation keys.</summary>
    /// <returns></returns>
    public async Task<IEnumerable<SecurityKeyInfo>> GetValidationKeysAsync()
    {
        var keys = await _keyService.GetLastKeys(_options.Value.AlgorithmsToKeep);
        if (!keys.Any())
            await _keyService.GenerateKey();

        keys = await _keyService.GetLastKeys(_options.Value.AlgorithmsToKeep);

        return keys.Select(s => new SecurityKeyInfo()
        {
            Key = s,
            SigningAlgorithm = _options.Value.Jws
        });
    }
}