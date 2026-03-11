using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStoryBehindColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StoryBehind",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoryBehind",
                table: "Product");
        }
    }
}
