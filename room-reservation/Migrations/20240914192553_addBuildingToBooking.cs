using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace room_reservation.Migrations
{
    public partial class addBuildingToBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserBuildingAR",
                table: "tblBookings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserBuildingAR",
                table: "tblBookings");
        }
    }
}
