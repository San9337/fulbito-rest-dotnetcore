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
    [Migration("20180218151354_InitialCreate")]
    partial class InitialCreate
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

                    b.Property<int>("Age");

                    b.Property<int?>("CityId");

                    b.Property<int?>("CountryId");

                    b.Property<int>("Gender");

                    b.Property<string>("LastName");

                    b.Property<string>("Name");

                    b.Property<string>("NickName");

                    b.Property<string>("ProfilePictureUrl");

                    b.Property<string>("RealTeam");

                    b.Property<string>("SkilledFoot");

                    b.Property<int?>("StateId");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.HasIndex("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("model.UserCredentials", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Email");

                    b.Property<string>("HashedPassword");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.ToTable("UserCredentials");
                });

            modelBuilder.Entity("model.Model.City", b =>
                {
                    b.HasOne("model.Model.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId");
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
                        .HasForeignKey("CityId");

                    b.HasOne("model.Model.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("model.Model.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("model.UserCredentials", b =>
                {
                    b.HasOne("model.Model.User", "User")
                        .WithOne("Credentials")
                        .HasForeignKey("model.UserCredentials", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
