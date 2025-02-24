using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastracture.configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
          new IdentityRole { Id = "f2c721e9-df3c-4663-9f06-6e22dcb134c8", Name = "Admin", NormalizedName = "ADMIN" },
           new IdentityRole { Id = "df295d04-b2ec-489e-a3da-8ccf29a8c48c", Name = "Customer", NormalizedName = "CUSTOMER" });
        }
    }
}

/*

    builder.HasData(
        new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" },
        new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Customer", NormalizedName = "CUSTOMER" }
    );
 */