﻿// <auto-generated />
using System;
using ChurchCashFlow.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChurchCashFlow.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230315004443_InitialDataBase")]
    partial class InitialDataBase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataModelChurchCashFlow.Models.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Additional")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Additional");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("City");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Country");

                    b.Property<string>("District")
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("District");

                    b.Property<int>("Number")
                        .HasMaxLength(6)
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("State");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Street");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("Address", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Additional = "",
                            City = "São Lourenço",
                            Country = "Brasil",
                            District = "Centro",
                            Number = 780,
                            State = "Minas Gerais",
                            Street = "Rua Dr Ribeiro da Luz",
                            ZipCode = "37470-000"
                        },
                        new
                        {
                            Id = 2,
                            Additional = "Prédio 1",
                            City = "Itaguai",
                            Country = "Brasil",
                            District = "Mesquita",
                            Number = 258,
                            State = "Rio de Janeiro",
                            Street = "Avenida André Chaves",
                            ZipCode = "13710-000"
                        });
                });

            modelBuilder.Entity("DataModelChurchCashFlow.Models.Entities.Church", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Active")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValue(true)
                        .HasColumnName("Active");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Church", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddressId = 1,
                            Name = "CEO São Lourenço"
                        },
                        new
                        {
                            Id = 2,
                            AddressId = 2,
                            Name = "CEP Cristina"
                        });
                });

            modelBuilder.Entity("DataModelChurchCashFlow.Models.Entities.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Active")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValue(true)
                        .HasColumnName("Active");

                    b.Property<DateTime>("DateBirth")
                        .HasColumnType("DATE")
                        .HasColumnName("DateBirth");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId")
                        .IsUnique();

                    b.ToTable("Member", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateBirth = new DateTime(2021, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Isaque de souza",
                            PostId = 1
                        },
                        new
                        {
                            Id = 2,
                            DateBirth = new DateTime(2021, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Fernanda Miranda",
                            PostId = 2
                        },
                        new
                        {
                            Id = 3,
                            DateBirth = new DateTime(2021, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Gabriela Soares",
                            PostId = 3
                        },
                        new
                        {
                            Id = 4,
                            DateBirth = new DateTime(2021, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "João Vitor Nascimento",
                            PostId = 4
                        },
                        new
                        {
                            Id = 5,
                            DateBirth = new DateTime(2021, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Mauricio Emanuel",
                            PostId = 5
                        },
                        new
                        {
                            Id = 6,
                            DateBirth = new DateTime(2021, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Joana Darc Crispim",
                            PostId = 6
                        },
                        new
                        {
                            Id = 7,
                            DateBirth = new DateTime(2021, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Rogerio Gegrório Martins",
                            PostId = 7
                        },
                        new
                        {
                            Id = 8,
                            DateBirth = new DateTime(2021, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Valéria De Carvalho",
                            PostId = 8
                        },
                        new
                        {
                            Id = 9,
                            DateBirth = new DateTime(2021, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Taisa Fonseca da Silva",
                            PostId = 9
                        },
                        new
                        {
                            Id = 10,
                            DateBirth = new DateTime(2021, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Auxiliadora de Souza Morais",
                            PostId = 10
                        },
                        new
                        {
                            Id = 11,
                            DateBirth = new DateTime(2021, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Victor Figueredo Junior",
                            PostId = 11
                        });
                });

            modelBuilder.Entity("DataModelChurchCashFlow.Models.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Active")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValue(true)
                        .HasColumnName("Active");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Post", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Pessoa visitante eventual",
                            Name = "Visitante avulso"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Pessoa visitante frenquente",
                            Name = "Visitante frequente"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Membro batisado com ficha",
                            Name = "Membro"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Membro batisado pertencente ao grupo de louvor",
                            Name = "Levita"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Membro batisado ajudante nos cultos",
                            Name = "Obreiro"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Membro batisado cooperador da obra",
                            Name = "Diacono"
                        },
                        new
                        {
                            Id = 7,
                            Description = "Membro batisado auxiliador do pastor",
                            Name = "Preisbitero"
                        },
                        new
                        {
                            Id = 8,
                            Description = "Membro batisado lider espiritual da Igreja",
                            Name = "Pastor Auxiliar"
                        },
                        new
                        {
                            Id = 9,
                            Description = "Membro batisado lider espiritual e administrativo da Igreja",
                            Name = "Visitante frequente"
                        },
                        new
                        {
                            Id = 10,
                            Description = "Membro batisado lider da cobertura espiritual",
                            Name = "Bispo"
                        },
                        new
                        {
                            Id = 11,
                            Description = "Membro batisado porém afastado da igreja",
                            Name = "Desligado"
                        });
                });

            modelBuilder.Entity("DataModelChurchCashFlow.Models.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "LOCAL"
                        },
                        new
                        {
                            Id = 2,
                            Name = "MINISTERIO"
                        });
                });

            modelBuilder.Entity("DataModelChurchCashFlow.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Active")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValue(true)
                        .HasColumnName("Active");

                    b.Property<int>("ChurchId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(6)
                        .IsUnicode(true)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.Property<string>("PassWordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("PasswordHash");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChurchId");

                    b.HasIndex("RoleId");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChurchId = 1,
                            Code = "F97C2B",
                            Name = "Rodolfo de Jesus Silva",
                            PassWordHash = "10000.TIgVKNrhXjPOK1WA16Sjww==.1jkg7E1BkCn1PWzD3C0LqwW+mCXBW2kn6JbK0VveVKA=",
                            RoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            ChurchId = 2,
                            Code = "0554A6",
                            Name = "Kelly Cristina Martins",
                            PassWordHash = "10000.HQM0PeJ09LIj/zEQycQR0Q==.9CLOh/XPQ7wQTOXibTLzxzzHlP3YdnxTk1e9CEa6JXo=",
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("DataModelChurchCashFlow.Models.Entities.Church", b =>
                {
                    b.HasOne("DataModelChurchCashFlow.Models.Entities.Address", "Address")
                        .WithOne()
                        .HasForeignKey("DataModelChurchCashFlow.Models.Entities.Church", "AddressId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("Fk_Church_Address");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("DataModelChurchCashFlow.Models.Entities.Member", b =>
                {
                    b.HasOne("DataModelChurchCashFlow.Models.Entities.Post", "Post")
                        .WithOne()
                        .HasForeignKey("DataModelChurchCashFlow.Models.Entities.Member", "PostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("Fk_Member_Post");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("DataModelChurchCashFlow.Models.Entities.User", b =>
                {
                    b.HasOne("DataModelChurchCashFlow.Models.Entities.Church", "Church")
                        .WithMany("Users")
                        .HasForeignKey("ChurchId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("Fk_User_Church");

                    b.HasOne("DataModelChurchCashFlow.Models.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("Fk_User_Role");

                    b.Navigation("Church");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DataModelChurchCashFlow.Models.Entities.Church", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("DataModelChurchCashFlow.Models.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
