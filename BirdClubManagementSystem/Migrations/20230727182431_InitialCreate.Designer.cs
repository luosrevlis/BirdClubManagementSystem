﻿// <auto-generated />
using System;
using BirdClubManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BirdClubManagementSystem.Migrations
{
    [DbContext(typeof(BcmsDbContext))]
    [Migration("20230727182431_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BirdClubManagementSystem.Models.Bird", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<byte[]>("ProfilePicture")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Birds");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BlogCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Contents")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<byte[]>("Thumbnail")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BlogCategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.BlogCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("BlogCategories");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BlogId")
                        .HasColumnType("int");

                    b.Property<string>("Contents")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contents")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.FieldTrip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpectedEndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Fee")
                        .HasColumnType("int");

                    b.Property<string>("Highlights")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("RegCloseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RegLimit")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegOpenDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("Id");

                    b.ToTable("FieldTrips");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.FieldTripRegistration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("FieldTripId")
                        .HasColumnType("int");

                    b.Property<bool>("PaymentReceived")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FieldTripId");

                    b.HasIndex("UserId");

                    b.ToTable("FieldTripRegistrations");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpectedEndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Fee")
                        .HasColumnType("int");

                    b.Property<string>("Highlights")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("RegCloseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RegLimit")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegOpenDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("Id");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.MeetingRegistration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("MeetingId")
                        .HasColumnType("int");

                    b.Property<bool>("PaymentReceived")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MeetingId");

                    b.HasIndex("UserId");

                    b.ToTable("MeetingRegistrations");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.MembershipRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("Id");

                    b.ToTable("MembershipRequests");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpectedEndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Fee")
                        .HasColumnType("int");

                    b.Property<string>("Highlights")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("RegCloseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RegLimit")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegOpenDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("Id");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.TournamentRegistration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BirdId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("PaymentReceived")
                        .HasColumnType("bit");

                    b.Property<int>("TournamentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BirdId");

                    b.HasIndex("TournamentId");

                    b.ToTable("TournamentRegistrations");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.TournamentStanding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BirdId")
                        .HasColumnType("int");

                    b.Property<string>("Placement")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<int>("TournamentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BirdId");

                    b.HasIndex("TournamentId");

                    b.ToTable("TournamentStandings");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(86)
                        .HasColumnType("nvarchar(86)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<byte[]>("ProfilePicture")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ResetPasswordCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<DateTime?>("ResetPasswordRequestTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.Bird", b =>
                {
                    b.HasOne("BirdClubManagementSystem.Models.User", "User")
                        .WithMany("Birds")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.Blog", b =>
                {
                    b.HasOne("BirdClubManagementSystem.Models.BlogCategory", "BlogCategory")
                        .WithMany("Blogs")
                        .HasForeignKey("BlogCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BirdClubManagementSystem.Models.User", "User")
                        .WithMany("Blogs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BlogCategory");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.Comment", b =>
                {
                    b.HasOne("BirdClubManagementSystem.Models.Blog", "Blog")
                        .WithMany("Comments")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BirdClubManagementSystem.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blog");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.Feedback", b =>
                {
                    b.HasOne("BirdClubManagementSystem.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.FieldTripRegistration", b =>
                {
                    b.HasOne("BirdClubManagementSystem.Models.FieldTrip", "FieldTrip")
                        .WithMany("FieldTripRegistrations")
                        .HasForeignKey("FieldTripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BirdClubManagementSystem.Models.User", "User")
                        .WithMany("FieldTripRegistrations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FieldTrip");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.MeetingRegistration", b =>
                {
                    b.HasOne("BirdClubManagementSystem.Models.Meeting", "Meeting")
                        .WithMany("MeetingRegistrations")
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BirdClubManagementSystem.Models.User", "User")
                        .WithMany("MeetingRegistrations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meeting");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.TournamentRegistration", b =>
                {
                    b.HasOne("BirdClubManagementSystem.Models.Bird", "Bird")
                        .WithMany("TournamentRegistrations")
                        .HasForeignKey("BirdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BirdClubManagementSystem.Models.Tournament", "Tournament")
                        .WithMany("TournamentRegistrations")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bird");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.TournamentStanding", b =>
                {
                    b.HasOne("BirdClubManagementSystem.Models.Bird", "Bird")
                        .WithMany("TournamentStandings")
                        .HasForeignKey("BirdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BirdClubManagementSystem.Models.Tournament", "Tournament")
                        .WithMany("TournamentStandings")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bird");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.Bird", b =>
                {
                    b.Navigation("TournamentRegistrations");

                    b.Navigation("TournamentStandings");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.Blog", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.BlogCategory", b =>
                {
                    b.Navigation("Blogs");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.FieldTrip", b =>
                {
                    b.Navigation("FieldTripRegistrations");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.Meeting", b =>
                {
                    b.Navigation("MeetingRegistrations");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.Tournament", b =>
                {
                    b.Navigation("TournamentRegistrations");

                    b.Navigation("TournamentStandings");
                });

            modelBuilder.Entity("BirdClubManagementSystem.Models.User", b =>
                {
                    b.Navigation("Birds");

                    b.Navigation("Blogs");

                    b.Navigation("Comments");

                    b.Navigation("FieldTripRegistrations");

                    b.Navigation("MeetingRegistrations");
                });
#pragma warning restore 612, 618
        }
    }
}
