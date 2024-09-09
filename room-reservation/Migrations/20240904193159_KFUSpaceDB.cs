using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace room_reservation.Migrations
{
    public partial class KFUSpaceDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingsLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrantedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    BookedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingStatus = table.Column<int>(type: "int", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdditionalDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingsLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuildingsLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrantdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingId = table.Column<int>(type: "int", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdditionalDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingsLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FloorsLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrantdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FloorId = table.Column<int>(type: "int", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdditionalDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FloorsLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionsLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    GrantedTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrantedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermissionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionsLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomsLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    GrantdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdditionalDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomsLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblBookingStatues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatuesAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatuesEN = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBookingStatues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblGender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblLectures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Semester = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartLectureTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndLectureTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    LectureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LectureDurations = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLectures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleNameEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleNameAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblRoomType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRoomType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullNameAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullNameEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    genderAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    genderEN = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblBuildings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingNo = table.Column<int>(type: "int", nullable: false),
                    BuildingNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBuildings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBuildings_tblGender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "tblGender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "tblFloors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorNo = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BuildingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFloors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFloors_tblBuildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "tblBuildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "tblPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    BuildingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblPermissions_tblBuildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "tblBuildings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblPermissions_tblRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tblRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "tblRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorId = table.Column<int>(type: "int", nullable: false),
                    RoomNo = table.Column<int>(type: "int", nullable: false),
                    RoomTypeId = table.Column<int>(type: "int", nullable: false),
                    SeatCapacity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblRooms_tblFloors_FloorId",
                        column: x => x.FloorId,
                        principalTable: "tblFloors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_tblRooms_tblRoomType_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "tblRoomType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "tblBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingStart = table.Column<TimeSpan>(type: "time", nullable: false),
                    BookingEnd = table.Column<TimeSpan>(type: "time", nullable: false),
                    Duration = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RejectReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    BookingStatuesId = table.Column<int>(type: "int", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBookings_tblBookingStatues_BookingStatuesId",
                        column: x => x.BookingStatuesId,
                        principalTable: "tblBookingStatues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_tblBookings_tblGender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "tblGender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_tblBookings_tblRooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "tblRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblBookings_BookingStatuesId",
                table: "tblBookings",
                column: "BookingStatuesId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBookings_GenderId",
                table: "tblBookings",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBookings_RoomId",
                table: "tblBookings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBuildings_GenderId",
                table: "tblBuildings",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFloors_BuildingId",
                table: "tblFloors",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPermissions_BuildingId",
                table: "tblPermissions",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPermissions_RoleId",
                table: "tblPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRooms_FloorId",
                table: "tblRooms",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRooms_RoomTypeId",
                table: "tblRooms",
                column: "RoomTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingsLog");

            migrationBuilder.DropTable(
                name: "BuildingsLog");

            migrationBuilder.DropTable(
                name: "FloorsLog");

            migrationBuilder.DropTable(
                name: "PermissionsLog");

            migrationBuilder.DropTable(
                name: "RoomsLog");

            migrationBuilder.DropTable(
                name: "tblBookings");

            migrationBuilder.DropTable(
                name: "tblLectures");

            migrationBuilder.DropTable(
                name: "tblPermissions");

            migrationBuilder.DropTable(
                name: "tblUsers");

            migrationBuilder.DropTable(
                name: "tblBookingStatues");

            migrationBuilder.DropTable(
                name: "tblRooms");

            migrationBuilder.DropTable(
                name: "tblRoles");

            migrationBuilder.DropTable(
                name: "tblFloors");

            migrationBuilder.DropTable(
                name: "tblRoomType");

            migrationBuilder.DropTable(
                name: "tblBuildings");

            migrationBuilder.DropTable(
                name: "tblGender");
        }
    }
}
