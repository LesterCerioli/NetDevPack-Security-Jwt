using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetDevPack.Security.Jwt.Core.Model;

namespace NetDevPack.Security.Jwt.Store.EntityFrameworkCore;

public class KeyMaterialMap : IEntityTypeConfiguration<KeyMaterial>
{
    public void Configure(EntityTypeBuilder<KeyMaterial> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Parameters)
            .HasMaxLength(8000)
            .IsRequired();
    }
}