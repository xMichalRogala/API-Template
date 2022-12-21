﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TemplateApi.Persistence.DbContexts.Auth;

#nullable disable

namespace TemplateApi.Migrations.AuthDb
{
    [DbContext(typeof(AuthDbContext))]
    partial class AuthDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Auth")
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Auth.Domain.Schemas.Entities.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permissions", "Auth");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Access"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Read"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Create"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Update"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Delete"
                        });
                });

            modelBuilder.Entity("Auth.Domain.Schemas.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles", "Auth");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Registered"
                        });
                });

            modelBuilder.Entity("Auth.Domain.Schemas.Entities.RolePermission", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermission", "Auth");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            PermissionId = 2
                        });
                });

            modelBuilder.Entity("Auth.Domain.Schemas.Entities.UserCredential", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Iterations")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("PasswordBytes")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("Password");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique()
                        .HasFilter("[Login] IS NOT NULL");

                    b.ToTable("UserCredentials", "Auth");
                });

            modelBuilder.Entity("RoleUserCredential", b =>
                {
                    b.Property<Guid>("CredentialsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.HasKey("CredentialsId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("RoleUserCredential", "Auth");
                });

            modelBuilder.Entity("Auth.Domain.Schemas.Entities.RolePermission", b =>
                {
                    b.HasOne("Auth.Domain.Schemas.Entities.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Auth.Domain.Schemas.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoleUserCredential", b =>
                {
                    b.HasOne("Auth.Domain.Schemas.Entities.UserCredential", null)
                        .WithMany()
                        .HasForeignKey("CredentialsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Auth.Domain.Schemas.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
