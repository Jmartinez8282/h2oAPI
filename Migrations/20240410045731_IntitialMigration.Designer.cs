﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using h2oAPI.Data;

#nullable disable

namespace h2oAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240410045731_IntitialMigration")]
    partial class IntitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("h2oAPI.Models.LoginRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LoginRequests");
                });

            modelBuilder.Entity("h2oAPI.Models.Question", b =>
                {
                    b.Property<int>("QuestionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Competition")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("INTEGER");

                    b.Property<string>("QuestionText")
                        .HasColumnType("TEXT");

                    b.Property<int>("SortOrder")
                        .HasColumnType("INTEGER");

                    b.HasKey("QuestionID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("h2oAPI.Models.Score", b =>
                {
                    b.Property<int>("ScoreID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Competition")
                        .HasColumnType("TEXT");

                    b.Property<int>("QuestionID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ScoreValue")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TeamID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ScoreID");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("h2oAPI.Models.Team", b =>
                {
                    b.Property<int>("TeamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Coach")
                        .HasColumnType("TEXT");

                    b.Property<string>("Competition")
                        .HasColumnType("TEXT");

                    b.Property<string>("Division")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("TeamID");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("h2oAPI.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Competition")
                        .HasColumnType("TEXT");

                    b.Property<string>("Division")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("IsDeleted")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserRole")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserType")
                        .HasColumnType("TEXT");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
