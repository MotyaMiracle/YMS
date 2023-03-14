﻿// <auto-generated />
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YardManagementSystem.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230225110034_AddNumber")]
    partial class AddNumber
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Database.Entity.HistoryEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("HistoryEntries");
                });

            modelBuilder.Entity("Database.Entity.Storage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("Yard_Management_System.Entity.Driver", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("Yard_Management_System.Entity.EntityFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("uuid");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Yard_Management_System.Entity.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int[]>("ListOfPermissions")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Yard_Management_System.Entity.Trip", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("uuid");

                    b.Property<int>("NowStatus")
                        .HasColumnType("integer");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("StorageId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.HasIndex("StorageId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("Yard_Management_System.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("Login", "Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Database.Entity.HistoryEntry", b =>
                {
                    b.HasOne("Yard_Management_System.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yard_Management_System.Entity.EntityFile", b =>
                {
                    b.HasOne("Yard_Management_System.Entity.Driver", null)
                        .WithMany("Files")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Yard_Management_System.Entity.Trip", b =>
                {
                    b.HasOne("Yard_Management_System.Entity.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Entity.Storage", "Storage")
                        .WithMany("Trips")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("Yard_Management_System.Entity.User", b =>
                {
                    b.HasOne("Yard_Management_System.Entity.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Database.Entity.Storage", b =>
                {
                    b.Navigation("Trips");
                });

            modelBuilder.Entity("Yard_Management_System.Entity.Driver", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
