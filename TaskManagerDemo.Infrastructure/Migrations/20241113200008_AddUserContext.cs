using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagerDemo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("801059ca-2427-4710-b085-ce84cfe0b800"), "admin@example.com", 1, "admin" },
                    { new Guid("e37604b5-2b8e-4db9-a00f-afce5ce31ffa"), "user1@example.com", 0, "user1" },
                    { new Guid("f0a15867-e199-4567-8595-7369cbb99baa"), "user2@example.com", 0, "user2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
