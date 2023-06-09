﻿// <auto-generated />
using System;
using FACTORINGHONOR.PE.DATA.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FACTORINGHONOR.PE.DATA.Migrations
{
    [DbContext(typeof(FactoringHonorDbContext))]
    [Migration("20191119070430_CurrencyUpdate1")]
    partial class CurrencyUpdate1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FACTORINGHONOR.PE.ENTITIES.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<Guid>("CurrencyTypeId");

                    b.Property<string>("Dni")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MaternalSurname")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PaternalSurname")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int>("RateType");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("CurrencyTypeId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("FACTORINGHONOR.PE.ENTITIES.Models.Bank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("RateTerm");

                    b.Property<int>("RateType");

                    b.Property<double>("RateValue");

                    b.Property<string>("ShortName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("FACTORINGHONOR.PE.ENTITIES.Models.BankCost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BankId");

                    b.Property<Guid>("CurrencyTypeId");

                    b.Property<bool>("IsFinal");

                    b.Property<string>("Name");

                    b.Property<double>("Value");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex("CurrencyTypeId");

                    b.ToTable("BankCosts");
                });

            modelBuilder.Entity("FACTORINGHONOR.PE.ENTITIES.Models.CurrencyType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("ShortName")
                        .IsRequired();

                    b.Property<string>("Symbol")
                        .IsRequired();

                    b.Property<double>("Value");

                    b.HasKey("Id");

                    b.ToTable("CurrencyTypes");
                });

            modelBuilder.Entity("FACTORINGHONOR.PE.ENTITIES.Models.EntityRecord", b =>
                {
                    b.Property<string>("Ruc")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Ruc");

                    b.ToTable("EntityRecords");
                });

            modelBuilder.Entity("FACTORINGHONOR.PE.ENTITIES.Models.FeeReceipt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BankId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid>("CurrencyTypeId");

                    b.Property<string>("DebtorRuc")
                        .IsRequired();

                    b.Property<DateTime>("DiscountDate");

                    b.Property<DateTime>("IssueDate");

                    b.Property<string>("IssuingRuc")
                        .IsRequired();

                    b.Property<DateTime>("PaymentDate");

                    b.Property<double>("TermEffectiveRate");

                    b.Property<double>("TotalAmmount");

                    b.Property<double>("TotalFinalCost");

                    b.Property<double>("TotalInitialCost");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.Property<double>("Withholding");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex("CurrencyTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("FeeReceipts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FACTORINGHONOR.PE.ENTITIES.Models.ApplicationUser", b =>
                {
                    b.HasOne("FACTORINGHONOR.PE.ENTITIES.Models.CurrencyType", "CurrencyType")
                        .WithMany("ApplicationUsers")
                        .HasForeignKey("CurrencyTypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FACTORINGHONOR.PE.ENTITIES.Models.BankCost", b =>
                {
                    b.HasOne("FACTORINGHONOR.PE.ENTITIES.Models.Bank", "Bank")
                        .WithMany("BankCosts")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FACTORINGHONOR.PE.ENTITIES.Models.CurrencyType", "CurrencyType")
                        .WithMany("BankCosts")
                        .HasForeignKey("CurrencyTypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FACTORINGHONOR.PE.ENTITIES.Models.FeeReceipt", b =>
                {
                    b.HasOne("FACTORINGHONOR.PE.ENTITIES.Models.Bank", "Bank")
                        .WithMany("FeeReceipts")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FACTORINGHONOR.PE.ENTITIES.Models.CurrencyType", "CurrencyType")
                        .WithMany("FeeReceipts")
                        .HasForeignKey("CurrencyTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FACTORINGHONOR.PE.ENTITIES.Models.ApplicationUser", "User")
                        .WithMany("FeeReceipts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FACTORINGHONOR.PE.ENTITIES.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FACTORINGHONOR.PE.ENTITIES.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FACTORINGHONOR.PE.ENTITIES.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FACTORINGHONOR.PE.ENTITIES.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
