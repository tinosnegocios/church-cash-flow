﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Registration.DomainBase.Entities.Registrations;

namespace Registration.Repository.Mapping;
public class ChurchMap : IEntityTypeConfiguration<Church>
{
    public void Configure(EntityTypeBuilder<Church> builder)
    {
        builder.ToTable("Church");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(50);

        builder.Property(x => x.Active)
            .IsRequired()
            .HasColumnName("Active")
            .HasColumnType("BIT")
            .HasDefaultValue(true); 

        builder.HasOne(church => church.Address)
            .WithOne()
            .HasConstraintName("Fk_Church_Address")
            .OnDelete(DeleteBehavior.NoAction);

        InsertDataTemp(builder);
    }

    private void InsertDataTemp(EntityTypeBuilder<Church> builder)
    {
        builder.HasData(new Church(1, "CEO São Lourenço", 1, "SLC"));
        builder.HasData(new Church(2, "CEP Cristina", 2, "CRT"));
        builder.HasData(new Church(3, "CEP Lambari", 3, "LBR"));
    }
}
