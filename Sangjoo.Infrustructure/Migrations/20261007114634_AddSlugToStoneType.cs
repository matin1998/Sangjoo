using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sangjoo.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugToStoneType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Colors_ColorId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ColorId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "StoneTypes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ColorStone",
                columns: table => new
                {
                    ColorsId = table.Column<long>(type: "bigint", nullable: false),
                    StonesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorStone", x => new { x.ColorsId, x.StonesId });
                    table.ForeignKey(
                        name: "FK_ColorStone_Colors_ColorsId",
                        column: x => x.ColorsId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColorStone_Products_StonesId",
                        column: x => x.StonesId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoneTypes_Slug",
                table: "StoneTypes",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ColorStone_StonesId",
                table: "ColorStone",
                column: "StonesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColorStone");

            migrationBuilder.DropIndex(
                name: "IX_StoneTypes_Slug",
                table: "StoneTypes");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "StoneTypes");

            migrationBuilder.AddColumn<long>(
                name: "ColorId",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ColorId",
                table: "Products",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Colors_ColorId",
                table: "Products",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
