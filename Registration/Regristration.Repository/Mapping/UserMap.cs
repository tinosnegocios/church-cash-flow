﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Registration.DomainBase.Entities;
using SecureIdentity.Password;

namespace Regristration.Repository.Mapping;
public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(50);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasColumnName("Code")
            .HasColumnType("VARCHAR")
            .HasMaxLength(6)
            .IsUnicode();

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasColumnName("PasswordHash")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);

        builder.Property(x => x.Active)
            .IsRequired()
            .HasColumnName("Active")
            .HasColumnType("BIT")
            .HasDefaultValue(true);

        builder.HasOne(user => user.Church)
            .WithMany(church => church.Users)
            .HasConstraintName("Fk_User_Church")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(user => user.Role)
            .WithMany(roles => roles.Users)
            .HasConstraintName("Fk_User_Role")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(x => x.Code, "IX_User_Code")
            .IsUnique();

        InsertDataTemp(builder);
    }

    private void InsertDataTemp(EntityTypeBuilder<User> builder)
    {
        var passWord = "12345678";

        var user = new User(1, "Rodolfo de Jesus Silva", 1, 1, passWord);
        user.GeneratePassWordHash("12345678");
        user.GenerateCode();
        builder.HasData(user);

        user = new User(2, "Kelly Cristina Martins", 2, 2, passWord);
        user.GeneratePassWordHash("12345678");
        user.GenerateCode();
        builder.HasData(user);

    }
}