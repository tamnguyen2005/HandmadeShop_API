using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategoryTableV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCollection",
                table: "Category",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCollection",
                table: "Category");
        }
    }
}
