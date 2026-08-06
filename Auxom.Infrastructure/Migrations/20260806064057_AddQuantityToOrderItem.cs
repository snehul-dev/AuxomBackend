using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auxom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddQuantityToOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Qty",
                table: "OrderItems",
                newName: "Quantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderItems",
                newName: "Qty");
        }
    }
}
