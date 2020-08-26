using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodOrdering.Data.Migrations
{
    public partial class addMenuItemToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "menuItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    SubCategoryId = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menuItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_menuItem_category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_menuItem_subCategory_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "subCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_menuItem_CategoryId",
                table: "menuItem",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_menuItem_SubCategoryId",
                table: "menuItem",
                column: "SubCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menuItem");
        }
    }
}
