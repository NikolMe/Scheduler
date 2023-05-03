using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Data.Migrations
{
    public partial class EndOfTheDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndOfTheDay",
                table: "Place",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndOfTheDay",
                table: "Place");
        }
    }
}
