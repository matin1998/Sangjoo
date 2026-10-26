using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sangjoo.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSellerUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Sellers",
                type: "bigint",
                nullable: true);

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProcessingTypes_ProcessingTypeId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                     name: "PK_ProcessingTypes",
                     table: "ProcessingTypes");

            migrationBuilder.AlterColumn<long>(
                name: "ProcessingTypeId",
                table: "Products",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ProcessingTypes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcessingTypes",
                table: "ProcessingTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProcessingTypes_ProcessingTypeId",
                table: "Products",
                column: "ProcessingTypeId",
                principalTable: "ProcessingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_UserId",
                table: "Sellers",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            
            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_AspNetUsers_UserId",
                table: "Sellers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_AspNetUsers_UserId",
                table: "Sellers");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProcessingTypes_ProcessingTypeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_UserId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sellers");

            migrationBuilder.AlterColumn<int>(
                name: "ProcessingTypeId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProcessingTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProcessingTypes_ProcessingTypeId",
                table: "Products",
                column: "ProcessingTypeId",
                principalTable: "ProcessingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
