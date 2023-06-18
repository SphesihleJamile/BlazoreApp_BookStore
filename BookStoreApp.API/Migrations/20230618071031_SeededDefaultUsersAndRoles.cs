using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2ba25a6b-7443-428c-987b-e04eed682e35", "10fa8d3c-a66d-4d3b-8e3e-c88269d6cc4e", "Administrator", "ADMINISTRATOR" },
                    { "7446dfca-c29c-49c1-a9b0-6a5194472687", "ec74664a-439d-4adb-89ea-fbeab9ce1e09", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Image", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "cde5fb6a-5d0d-455a-8a92-4b226f60c54b", 0, "fbd992d8-adb7-474a-8d52-9101cc91142e", "user@bookstore.com", false, "System", null, "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEAg+94f60fOhoqfRMLd+fPpwdf2OIfSMRPtjPtd5p7uw26BTEVVH27V7h2+C8AOZMQ==", null, false, "d4a1762f-a15b-4f1d-aeff-10c91c27b7bf", false, "user@bookstore.com" },
                    { "f3492501-1a7b-461f-8bf8-e8610f4976ac", 0, "c4ecfae0-9dae-4a5a-b660-799ea5c0f0a6", "admin@bookstore.com", false, "System", null, "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAEAACcQAAAAENntUqVrzYsselXNluuVP9yuMAdtZv5jrEBXI7NIuRLSMqJMb+OSxZcvqFHSlayrsQ==", null, false, "c0d2f7e6-5af5-4b77-b970-4074bdba1714", false, "admin@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "7446dfca-c29c-49c1-a9b0-6a5194472687", "cde5fb6a-5d0d-455a-8a92-4b226f60c54b" },
                    { "2ba25a6b-7443-428c-987b-e04eed682e35", "f3492501-1a7b-461f-8bf8-e8610f4976ac" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7446dfca-c29c-49c1-a9b0-6a5194472687", "cde5fb6a-5d0d-455a-8a92-4b226f60c54b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2ba25a6b-7443-428c-987b-e04eed682e35", "f3492501-1a7b-461f-8bf8-e8610f4976ac" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ba25a6b-7443-428c-987b-e04eed682e35");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7446dfca-c29c-49c1-a9b0-6a5194472687");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cde5fb6a-5d0d-455a-8a92-4b226f60c54b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f3492501-1a7b-461f-8bf8-e8610f4976ac");
        }
    }
}
