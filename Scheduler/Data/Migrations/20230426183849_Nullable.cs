using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Data.Migrations
{
    public partial class Nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Place_Service_ServiceId",
                table: "Place");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "Place",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Place_Service_ServiceId",
                table: "Place",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Place_Service_ServiceId",
                table: "Place");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "Place",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Place_Service_ServiceId",
                table: "Place",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
