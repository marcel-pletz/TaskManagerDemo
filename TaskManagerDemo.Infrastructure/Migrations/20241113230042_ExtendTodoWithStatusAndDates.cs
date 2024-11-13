using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerDemo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExtendTodoWithStatusAndDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "Todo",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DueDate",
                table: "Todo",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedDateTime",
                table: "Todo",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartProgressDateTime",
                table: "Todo",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Todo",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "Todo");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Todo");

            migrationBuilder.DropColumn(
                name: "FinishedDateTime",
                table: "Todo");

            migrationBuilder.DropColumn(
                name: "StartProgressDateTime",
                table: "Todo");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Todo");
        }
    }
}
