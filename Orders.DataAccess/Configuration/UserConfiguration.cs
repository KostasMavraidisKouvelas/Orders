using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.DataAccess.Configuration
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = Guid.Parse("41d819ed-0b13-4cc8-9b3c-fab3b977a004"),
                    UserName = "admin@example.com",
                    NormalizedUserName = "ADMIN@EXAMPLE.COM",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEJzv...",
                    SecurityStamp = "JZ6X...",
                    ConcurrencyStamp = "d7e...",
                    PhoneNumber = "1234567890",
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
             );
        }
    }
    public class UserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<Guid>>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<IdentityUserClaim<Guid>> builder)
        {
            builder.HasData(
                new IdentityUserClaim<Guid>
                {
                    Id = 1,
                    UserId = Guid.Parse("41d819ed-0b13-4cc8-9b3c-fab3b977a004"),
                    ClaimType = "ViewProducts",
                    ClaimValue = "WebUser"
                },
                new IdentityUserClaim<Guid>
                {
                    Id = 2,
                    UserId = Guid.Parse("41d819ed-0b13-4cc8-9b3c-fab3b977a004"),
                    ClaimType = "EditProducts",
                    ClaimValue = "WebUser"
                },
                new IdentityUserClaim<Guid>
                {
                    Id = 3,
                    UserId = Guid.Parse("41d819ed-0b13-4cc8-9b3c-fab3b977a004"),
                    ClaimType = "ViewOrders",
                    ClaimValue = "WebUser"
                },
                new IdentityUserClaim<Guid>
                {
                    Id = 4,
                    UserId = Guid.Parse("41d819ed-0b13-4cc8-9b3c-fab3b977a004"),
                    ClaimType = "EditOrders",
                    ClaimValue = "WebUser"
                },
                new IdentityUserClaim<Guid>
                {
                    Id = 5,
                    UserId = Guid.Parse("41d819ed-0b13-4cc8-9b3c-fab3b977a004"),
                    ClaimType = "ResendInvoice",
                    ClaimValue = "WebUser"
                }
            );
        }
    }
}
