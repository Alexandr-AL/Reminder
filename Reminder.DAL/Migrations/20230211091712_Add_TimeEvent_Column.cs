using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reminder.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeEventColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTimeEvent",
                table: "Events",
                newName: "DateEvent");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeEvent",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan());
                //defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeEvent",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "DateEvent",
                table: "Events",
                newName: "DateTimeEvent");
        }
    }
}
