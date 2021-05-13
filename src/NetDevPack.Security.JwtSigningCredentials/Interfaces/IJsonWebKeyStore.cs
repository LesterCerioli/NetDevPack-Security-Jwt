using NetDevPack.Security.JwtSigningCredentials.Model;
using System.Collections.Generic;

namespace NetDevPack.Security.JwtSigningCredentials.Interfaces
{
    public interface IJsonWebKeyStore
    {
        void Save(SecurityKeyWithPrivate securityParamteres);
        SecurityKeyWithPrivate GetCurrentKey(JsonWebKeyType jwkType);
        IReadOnlyCollection<SecurityKeyWithPrivate> Get(JsonWebKeyType jwkType, int quantity = 5);
        void Clear();
        bool NeedsUpdate(JsonWebKeyType jsonWebKeyType);
        void Revoke(SecurityKeyWithPrivate securityKeyWithPrivate);
    }
}