using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Security.Jwt.Core.Interfaces;
using NetDevPack.Security.Jwt.IdentityServer4;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Builder extension methods for registering crypto services
/// </summary>
public static class IdentityServerBuilderKeysExtensions
{
    /// <summary>
    /// Sets the signing credential.
    /// </summary>
    /// <returns></returns>
    public static IJwksBuilder IdentityServer4AutoJwksManager(this IJwksBuilder builder)
    {
        builder.Services.AddScoped<ISigningCredentialStore, IdentityServer4KeyStore>();
        builder.Services.AddScoped<IValidationKeysStore, IdentityServer4KeyStore>();
        builder.Services.AddMemoryCache();
        return builder;
    }
}