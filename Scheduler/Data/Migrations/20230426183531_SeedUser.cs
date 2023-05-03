using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Data.Migrations
{
    public partial class SeedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d9f7533c-267c-4d5e-9fe3-34d078bac07c", 0, "9be049e0-7d71-4f09-8fc6-69071124d73c", "admin", false, false, null, "ADMIN", "ADMIN", "AQAAAAEAACcQAAAAEMagSRqe13G2UPkJwrYPcO3DrTIBWE8jdtAbo3JgJ/84ZUAuEQb5bKASkE2f5ab5Eg==", null, false, "PW7Q4CER4OQTVUXHWZSWLZVKJSCKL2LY", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9f7533c-267c-4d5e-9fe3-34d078bac07c");
        }
    }
}
