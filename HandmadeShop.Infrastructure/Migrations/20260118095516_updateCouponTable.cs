using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateCouponTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Coupon",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Coupon");
        }
    }
}
