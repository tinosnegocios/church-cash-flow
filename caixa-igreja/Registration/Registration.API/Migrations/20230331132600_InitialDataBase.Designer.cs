﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Regristration.Repository;

#nullable disable

namespace Registration.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230331132600_InitialDataBase")]
    partial class InitialDataBase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Registration.DomainBase.Entities.Address", b =>
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
                        },
                        new
                        {
                            Id = 3,
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

            modelBuilder.Entity("Registration.DomainBase.Entities.Church", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Acronym")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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
                            Acronym = "SLC",
                            AddressId = 1,
                            Name = "CEO São Lourenço"
                        },
                        new
                        {
                            Id = 2,
                            Acronym = "CRT",
                            AddressId = 2,
                            Name = "CEP Cristina"
                        },
                        new
                        {
                            Id = 3,
                            Acronym = "LBR",
                            AddressId = 3,
                            Name = "CEP Lambari"
                        });
                });

            modelBuilder.Entity("Registration.DomainBase.Entities.MeetingKind", b =>
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
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("MeetingKind", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Culto dos dias quinta com oração para libertação",
                            Name = "Culto de Libertação"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Culto do último domingo do mês com ministração da Santa Ceia do Senhor",
                            Name = "Culto de Santa Ceia"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Culto do 4º domingo do mês (quando haver 5 domingos). Culto para convidar o amigo ",
                            Name = "Culto de Centésima Ovelha"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Culto do 2º domingo do mês. Culto das primicias e dizímos",
                            Name = "Culto de Prosperidade"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Culto do 3º domingo do mês. Ofertas serão destinas às missões",
                            Name = "Culto de Missões"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Culto do 1º domingo do mês. Culto de poder, glória e batismo e renovo do Espirito Santo",
                            Name = "Culto do Poder"
                        },
                        new
                        {
                            Id = 7,
                            Description = "Culto em meio a semana. Culto em outros dias da semana. Redes e Eventos",
                            Name = "Culto de Celebração"
                        });
                });

            modelBuilder.Entity("Registration.DomainBase.Entities.Member", b =>
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
                        .HasMaxLength(11)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Code");

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

                    b.HasIndex("ChurchId");

                    b.HasIndex("PostId");

                    b.HasIndex(new[] { "Code" }, "IX_Member_Code")
                        .IsUnique();

                    b.ToTable("Member", (string)null);
                });

            modelBuilder.Entity("Registration.DomainBase.Entities.OfferingKind", b =>
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
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("OfferingKind", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Oferta de amor com pix",
                            Name = "PIX"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Oferta de amor com Cédulas",
                            Name = "Cédulas"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Oferta de amor com cartão crédito/débito",
                            Name = "Crédito/Débito"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Oferta de amor com TED/DOC",
                            Name = "Transferência"
                        });
                });

            modelBuilder.Entity("Registration.DomainBase.Entities.OutFlow", b =>
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

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Amount");

                    b.Property<bool>("Authorized")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValue(true)
                        .HasColumnName("Authorized");

                    b.Property<int>("ChurchId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Day")
                        .HasColumnType("DATE")
                        .HasColumnName("Day");

                    b.Property<decimal>("Discount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m)
                        .HasColumnName("Discount");

                    b.Property<decimal>("Interest")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m)
                        .HasColumnName("Interest");

                    b.Property<string>("MonthYear")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("MonthYear");

                    b.Property<int>("OutFlowKindId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("TotalAmount");

                    b.HasKey("Id");

                    b.HasIndex("ChurchId");

                    b.HasIndex("OutFlowKindId");

                    b.ToTable("OutFlow", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 100m,
                            Authorized = true,
                            ChurchId = 1,
                            Day = new DateTime(2023, 3, 31, 13, 25, 59, 634, DateTimeKind.Utc).AddTicks(4850),
                            Discount = 0m,
                            Interest = 2m,
                            MonthYear = "03/2023",
                            OutFlowKindId = 1,
                            TotalAmount = 0m
                        },
                        new
                        {
                            Id = 2,
                            Amount = 1000.01m,
                            Authorized = true,
                            ChurchId = 2,
                            Day = new DateTime(2023, 3, 31, 13, 25, 59, 634, DateTimeKind.Utc).AddTicks(4983),
                            Discount = 0m,
                            Interest = 1.56m,
                            MonthYear = "03/2023",
                            OutFlowKindId = 2,
                            TotalAmount = 0m
                        },
                        new
                        {
                            Id = 3,
                            Amount = 1500.56m,
                            Authorized = true,
                            ChurchId = 3,
                            Day = new DateTime(2023, 3, 31, 13, 25, 59, 634, DateTimeKind.Utc).AddTicks(4997),
                            Discount = 20m,
                            Interest = 0.6m,
                            MonthYear = "03/2023",
                            OutFlowKindId = 3,
                            TotalAmount = 0m
                        });
                });

            modelBuilder.Entity("Registration.DomainBase.Entities.OutFlowKind", b =>
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
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("OutFlowKind", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Conta de Luz",
                            Name = "Energia"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Conta de água",
                            Name = "Água"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Produto de limpeza",
                            Name = "Zeladoria"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Oferta de gratidão para ministrante convidado",
                            Name = "Auxilio Ministrante"
                        });
                });

            modelBuilder.Entity("Registration.DomainBase.Entities.Post", b =>
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
                            Description = "Membro batisado porém transferido sob benção para outra igreja",
                            Name = "Transferido"
                        },
                        new
                        {
                            Id = 12,
                            Description = "Membro batisado porém afastado da igreja",
                            Name = "Desligado"
                        });
                });

            modelBuilder.Entity("Registration.DomainBase.Entities.Role", b =>
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

            modelBuilder.Entity("Registration.DomainBase.Entities.User", b =>
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

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("PasswordHash");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChurchId");

                    b.HasIndex("RoleId");

                    b.HasIndex(new[] { "Code" }, "IX_User_Code")
                        .IsUnique();

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChurchId = 1,
                            Code = "541CCC",
                            Name = "Rodolfo de Jesus Silva",
                            PassWord = "12345678",
                            PasswordHash = "10000.mBBs7QP1jNrR/kGl0V6naw==.sOW+Bbla3Ia2rruiUqskML89NXz4yWxjWvCrAQsCJPU=",
                            RoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            ChurchId = 2,
                            Code = "B5E2DD",
                            Name = "Kelly Cristina Martins",
                            PassWord = "12345678",
                            PasswordHash = "10000.hXZnkwJmN7kzF+UdAe1A7w==.uJdeCJjEI8qPuF0kYwXxBaNe+OYsb6kUBOaVg2HP5pw=",
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("Registration.DomainBase.Entities.Church", b =>
                {
                    b.HasOne("Registration.DomainBase.Entities.Address", "Address")
                        .WithOne()
                        .HasForeignKey("Registration.DomainBase.Entities.Church", "AddressId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("Fk_Church_Address");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Registration.DomainBase.Entities.Member", b =>
                {
                    b.HasOne("Registration.DomainBase.Entities.Church", "Church")
                        .WithMany("Members")
                        .HasForeignKey("ChurchId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("Fk_Member_Church");

                    b.HasOne("Registration.DomainBase.Entities.Post", "Post")
                        .WithMany("Members")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("Fk_Member_Post");

                    b.Navigation("Church");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Registration.DomainBase.Entities.OutFlow", b =>
                {
                    b.HasOne("Registration.DomainBase.Entities.Church", "Church")
                        .WithMany("OutFlows")
                        .HasForeignKey("ChurchId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("Fk_OutFlow-Church");

                    b.HasOne("Registration.DomainBase.Entities.OutFlowKind", "OutFlowKind")
                        .WithMany("OutFlows")
                        .HasForeignKey("OutFlowKindId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("Fk_OutFlowKind-OutFlow");

                    b.Navigation("Church");

                    b.Navigation("OutFlowKind");
                });

            modelBuilder.Entity("Registration.DomainBase.Entities.User", b =>
                {
                    b.HasOne("Registration.DomainBase.Entities.Church", "Church")
                        .WithMany("Users")
                        .HasForeignKey("ChurchId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("Fk_User_Church");

                    b.HasOne("Registration.DomainBase.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("Fk_User_Role");

                    b.Navigation("Church");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Registration.DomainBase.Entities.Church", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("OutFlows");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Registration.DomainBase.Entities.OutFlowKind", b =>
                {
                    b.Navigation("OutFlows");
                });

            modelBuilder.Entity("Registration.DomainBase.Entities.Post", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("Registration.DomainBase.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
