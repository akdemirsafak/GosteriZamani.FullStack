using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GosteriZamani.API.Migrations
{
    /// <inheritdoc />
    public partial class EventEntityPropNameUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventDate",
                table: "Events",
                newName: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Events",
                newName: "EventDate");
        }
    }
}
