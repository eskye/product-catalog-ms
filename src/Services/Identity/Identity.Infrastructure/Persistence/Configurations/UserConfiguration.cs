using System;
using Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(k => k.Id);
            builder.HasIndex(k => k.Email).IsUnique();
            builder.HasIndex(k => k.UserName).IsUnique();

            builder.Property(f => f.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(f => f.LastName).HasMaxLength(50).IsRequired();
            builder.Property(f => f.PhoneNumber).HasMaxLength(20).IsRequired();
            builder.Property(f => f.Email).HasMaxLength(50).IsRequired();
            builder.Property(f => f.PasswordHash).HasColumnName("Password");
            builder.Property(f => f.RefreshToken).HasMaxLength(100);

        }
    }
}

