using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheapTasks.Migrations
{
    /// <inheritdoc />
    public partial class AddIsPinned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPinned",
                table: "Tasks",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId_IsPinned",
                table: "Tasks",
                columns: new[] { "OwnerId", "IsPinned" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tasks_OwnerId_IsPinned",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IsPinned",
                table: "Tasks");
        }
    }
}
