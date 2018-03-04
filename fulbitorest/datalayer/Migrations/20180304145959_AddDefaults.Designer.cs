﻿// <auto-generated />
using datalayer.FulbitoContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using model.Enums;
using System;

namespace datalayer.Migrations
{
    [DbContext(typeof(FulbitoDbContext))]
    [Migration("20180304145959_AddDefaults")]
    partial class AddDefaults
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("model.Model.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("StateId");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("model.Model.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("model.Model.ProfessionalTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CountryName");

                    b.Property<string>("LogoUrl");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name", "CountryName");

                    b.ToTable("ProfessionalTeams");
                });

            modelBuilder.Entity("model.Model.Security.AuthContext", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("AuthMethod");

                    b.Property<DateTime>("ExpireDate");

                    b.Property<string>("RefreshToken");

                    b.Property<bool>("Revoked");

                    b.HasKey("Id");

                    b.HasIndex("RefreshToken");

                    b.ToTable("AuthContexts");
                });

            modelBuilder.Entity("model.Model.Security.UserCredentials", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("AuthMethod");

                    b.Property<string>("Email");

                    b.Property<string>("HashedPassword");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.ToTable("UserCredentials");
                });

            modelBuilder.Entity("model.Model.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CountryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("model.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("BirthDate");

                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.Property<int>("Gender");

                    b.Property<string>("LastName");

                    b.Property<string>("Name");

                    b.Property<string>("NickName");

                    b.Property<string>("ProfilePictureUrl");

                    b.Property<int>("RealTeamId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.Property<int>("SkilledFoot");

                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.HasIndex("Id");

                    b.HasIndex("RealTeamId");

                    b.HasIndex("StateId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("model.Model.City", b =>
                {
                    b.HasOne("model.Model.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("model.Model.Security.AuthContext", b =>
                {
                    b.HasOne("model.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("model.Model.Security.UserCredentials", b =>
                {
                    b.HasOne("model.Model.User", "User")
                        .WithOne("Credentials")
                        .HasForeignKey("model.Model.Security.UserCredentials", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("model.Model.State", b =>
                {
                    b.HasOne("model.Model.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");
                });

            modelBuilder.Entity("model.Model.User", b =>
                {
                    b.HasOne("model.Model.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("model.Model.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("model.Model.ProfessionalTeam", "RealTeam")
                        .WithMany()
                        .HasForeignKey("RealTeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("model.Model.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
