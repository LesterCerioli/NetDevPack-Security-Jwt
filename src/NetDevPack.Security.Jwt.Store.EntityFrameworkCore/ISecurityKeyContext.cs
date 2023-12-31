using Microsoft.EntityFrameworkCore;
using NetDevPack.Security.Jwt.Core.Model;

namespace NetDevPack.Security.Jwt.Store.EntityFrameworkCore
{
    public interface ISecurityKeyContext
    {
        /// <summary>
        /// A collection of <see cref="T:NetDevPack.Security.Jwt.Core.Model.KeyMaterial" />
        /// </summary>
        DbSet<KeyMaterial> SecurityKeys { get; set; }
    }
}
