﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using room_reservation.Models;

#nullable disable

namespace room_reservation.Migrations
{
    [DbContext(typeof(KFUSpaceContext))]
    partial class KFUSpaceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("room_reservation.Models.BookingsLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AdditionalDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int>("BookingStatus")
                        .HasColumnType("int");

                    b.Property<string>("GrantedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OperationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("BookingsLog");
                });

            modelBuilder.Entity("room_reservation.Models.BuildingsLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AdditionalDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BuildingId")
                        .HasColumnType("int");

                    b.Property<string>("GrantdBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OperationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OperationType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BuildingsLog");
                });

            modelBuilder.Entity("room_reservation.Models.FloorsLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AdditionalDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FloorId")
                        .HasColumnType("int");

                    b.Property<string>("GrantdBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OperationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OperationType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FloorsLog");
                });

            modelBuilder.Entity("room_reservation.Models.PermissionsLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AdditionalDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("GrantedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GrantedTo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OperationType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<string>("PermissionType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PermissionsLog");
                });

            modelBuilder.Entity("room_reservation.Models.RoomsLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AdditionalDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GrantdBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OperationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OperationType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RoomsLog");
                });

            modelBuilder.Entity("room_reservation.Models.tblBookings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("BookingStatuesId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RejectReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("guid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BookingStatuesId");

                    b.ToTable("tblBookings");
                });

            modelBuilder.Entity("room_reservation.Models.tblBookingStatues", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("StatuesAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatuesEN")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tblBookingStatues");
                });

            modelBuilder.Entity("room_reservation.Models.tblBuildings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BuildingNameAr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuildingNameEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BuildingNo")
                        .HasColumnType("int");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("tblBuildings");
                });

            modelBuilder.Entity("room_reservation.Models.tblFloors", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BuildingId")
                        .HasColumnType("int");

                    b.Property<int>("FloorNo")
                        .HasColumnType("int");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("tblFloors");
                });

            modelBuilder.Entity("room_reservation.Models.tblLectures", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BuildingNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("EndLectureTime")
                        .HasColumnType("time");

                    b.Property<DateTime>("LectureDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("LectureDurations")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RoomNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Semester")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("StartLectureTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("tblLectures");
                });

            modelBuilder.Entity("room_reservation.Models.tblPermissions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BuildingId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<Guid>("guid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.HasIndex("RoleId");

                    b.ToTable("tblPermissions");
                });

            modelBuilder.Entity("room_reservation.Models.tblRoles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RoleNameAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleNameEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("guid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("tblRoles");
                });

            modelBuilder.Entity("room_reservation.Models.tblRooms", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("FloorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RoomNo")
                        .HasColumnType("int");

                    b.Property<int>("RoomTypeId")
                        .HasColumnType("int");

                    b.Property<int>("SeatCapacity")
                        .HasColumnType("int");

                    b.Property<Guid>("guid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FloorId");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("tblRooms");
                });

            modelBuilder.Entity("room_reservation.Models.tblRoomType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RoomAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoomEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("guid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("tblRoomType");
                });

            modelBuilder.Entity("room_reservation.Models.tblUsers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullNameAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullNameEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tblUsers");
                });

            modelBuilder.Entity("room_reservation.Models.tblBookings", b =>
                {
                    b.HasOne("room_reservation.Models.tblBookingStatues", "BookingStatues")
                        .WithMany("Bookings")
                        .HasForeignKey("BookingStatuesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookingStatues");
                });

            modelBuilder.Entity("room_reservation.Models.tblFloors", b =>
                {
                    b.HasOne("room_reservation.Models.tblBuildings", "Building")
                        .WithMany("Floors")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");
                });

            modelBuilder.Entity("room_reservation.Models.tblPermissions", b =>
                {
                    b.HasOne("room_reservation.Models.tblBuildings", "Building")
                        .WithMany("Permissions")
                        .HasForeignKey("BuildingId");

                    b.HasOne("room_reservation.Models.tblRoles", "Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("room_reservation.Models.tblRooms", b =>
                {
                    b.HasOne("room_reservation.Models.tblFloors", "Floor")
                        .WithMany("Rooms")
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("room_reservation.Models.tblRoomType", "RoomType")
                        .WithMany()
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Floor");

                    b.Navigation("RoomType");
                });

            modelBuilder.Entity("room_reservation.Models.tblBookingStatues", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("room_reservation.Models.tblBuildings", b =>
                {
                    b.Navigation("Floors");

                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("room_reservation.Models.tblFloors", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("room_reservation.Models.tblRoles", b =>
                {
                    b.Navigation("Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}
