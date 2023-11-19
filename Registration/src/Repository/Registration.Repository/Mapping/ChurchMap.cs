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

        builder.Property(x => x.InaugurationDate)
            .IsRequired()
            .HasColumnName("InaugurationDate")
            .HasColumnType("DATE");

        builder.Property(x => x.RegisterDate)
            .IsRequired()
            .HasColumnName("RegisterDate")
            .HasColumnType("DATE");

        builder.Property(x => x.Active)
            .IsRequired()
            .HasColumnName("Active")
            .HasColumnType("BIT")
            .HasDefaultValue(true); 

        builder.HasOne(church => church.Address)
            .WithOne()
            .HasConstraintName("Fk_Church_Address")
            .OnDelete(DeleteBehavior.NoAction);

        //builder.HasOne(church => church.FirstPastor)
        //    .WithOne(member => member.ChurchFirstPastor)
        //    .HasForeignKey<Church>(church => church.FirstPastorId)
        //    .OnDelete(DeleteBehavior.SetNull)
        //    .IsRequired(false);

        InsertDataTemp(builder);
    }

    private void InsertDataTemp(EntityTypeBuilder<Church> builder)
    {
        builder.HasData(new Church(1, "CEO São Lourenço", new DateTime(2023, 11, 01), new DateTime(2023, 10, 01) , 1, "SLC"));
        builder.HasData(new Church(2, "CEP Cristina", new DateTime(2023, 11, 01), new DateTime(2023, 10, 01), 2, "CRT"));
        builder.HasData(new Church(3, "CEP Lambari", new DateTime(2023, 11, 01), new DateTime(2023, 10, 01), 3, "LBR"));
    }
}
