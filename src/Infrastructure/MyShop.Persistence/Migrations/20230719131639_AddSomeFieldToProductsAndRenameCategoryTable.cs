using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyShop.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSomeFieldToProductsAndRenameCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentCategoryId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Category_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopManager_Managers_ManagerId",
                table: "ShopManager");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopManager_Shops_ShopId",
                table: "ShopManager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopManager",
                table: "ShopManager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "ShopManager",
                newName: "ShopManagers");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_ShopManager_ManagerId",
                table: "ShopManagers",
                newName: "IX_ShopManagers_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Categories",
                newName: "IX_Categories_ParentCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_Id_Name",
                table: "Categories",
                newName: "IX_Categories_Id_Name");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Displayed",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopManagers",
                table: "ShopManagers",
                columns: new[] { "ShopId", "ManagerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopManagers_Managers_ManagerId",
                table: "ShopManagers",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopManagers_Shops_ShopId",
                table: "ShopManagers",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopManagers_Managers_ManagerId",
                table: "ShopManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopManagers_Shops_ShopId",
                table: "ShopManagers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopManagers",
                table: "ShopManagers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Displayed",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "ShopManagers",
                newName: "ShopManager");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_ShopManagers_ManagerId",
                table: "ShopManager",
                newName: "IX_ShopManager_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Category",
                newName: "IX_Category_ParentCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_Id_Name",
                table: "Category",
                newName: "IX_Category_Id_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopManager",
                table: "ShopManager",
                columns: new[] { "ShopId", "ManagerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Category_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopManager_Managers_ManagerId",
                table: "ShopManager",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopManager_Shops_ShopId",
                table: "ShopManager",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
