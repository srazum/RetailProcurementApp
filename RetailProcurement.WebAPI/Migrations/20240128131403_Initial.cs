using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RetailProcurement.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SupplierId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuarterlyPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quarter = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    SupplierId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuarterlyPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuarterlyPlans_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SupplierStoreItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    StoreItemId = table.Column<int>(type: "integer", nullable: false),
                    SupplierId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierStoreItems", x => x.Id);
                    table.UniqueConstraint("AK_SupplierStoreItems_StoreItemId_SupplierId", x => new { x.StoreItemId, x.SupplierId });
                    table.ForeignKey(
                        name: "FK_SupplierStoreItems_StoreItems_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "StoreItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SupplierStoreItems_Suppliers_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItemSupplier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    SupplierStoreItemId = table.Column<int>(type: "integer", nullable: false),
                    SupplierId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemSupplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemSupplier_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItemSupplier_SupplierStoreItems_SupplierStoreItemId",
                        column: x => x.SupplierStoreItemId,
                        principalTable: "SupplierStoreItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItemSupplier_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "StoreItems",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bike" },
                    { 2, "Bacon" },
                    { 3, "Soap" },
                    { 4, "Car" },
                    { 5, "Tuna" },
                    { 6, "Car" },
                    { 7, "Bike" },
                    { 8, "Pizza" },
                    { 9, "Table" },
                    { 10, "Fish" },
                    { 11, "Chicken" },
                    { 12, "Shoes" },
                    { 13, "Pizza" },
                    { 14, "Pants" },
                    { 15, "Ball" },
                    { 16, "Cheese" },
                    { 17, "Hat" },
                    { 18, "Cheese" },
                    { 19, "Pants" },
                    { 20, "Bacon" },
                    { 21, "Pants" },
                    { 22, "Salad" },
                    { 23, "Computer" },
                    { 24, "Pizza" },
                    { 25, "Ball" },
                    { 26, "Tuna" },
                    { 27, "Chicken" },
                    { 28, "Cheese" },
                    { 29, "Fish" },
                    { 30, "Mouse" },
                    { 31, "Chair" },
                    { 32, "Pants" },
                    { 33, "Salad" },
                    { 34, "Computer" },
                    { 35, "Computer" },
                    { 36, "Pizza" },
                    { 37, "Sausages" },
                    { 38, "Towels" },
                    { 39, "Gloves" },
                    { 40, "Computer" },
                    { 41, "Gloves" },
                    { 42, "Chips" },
                    { 43, "Chair" },
                    { 44, "Shoes" },
                    { 45, "Pants" },
                    { 46, "Shirt" },
                    { 47, "Bacon" },
                    { 48, "Hat" },
                    { 49, "Towels" },
                    { 50, "Pizza" },
                    { 51, "Ball" },
                    { 52, "Bacon" },
                    { 53, "Chicken" },
                    { 54, "Chips" },
                    { 55, "Car" },
                    { 56, "Bike" },
                    { 57, "Chair" },
                    { 58, "Hat" },
                    { 59, "Gloves" },
                    { 60, "Keyboard" },
                    { 61, "Salad" },
                    { 62, "Computer" },
                    { 63, "Cheese" },
                    { 64, "Mouse" },
                    { 65, "Fish" },
                    { 66, "Pizza" },
                    { 67, "Salad" },
                    { 68, "Bacon" },
                    { 69, "Shoes" },
                    { 70, "Pants" },
                    { 71, "Chips" },
                    { 72, "Table" },
                    { 73, "Ball" },
                    { 74, "Cheese" },
                    { 75, "Gloves" },
                    { 76, "Soap" },
                    { 77, "Bike" },
                    { 78, "Shirt" },
                    { 79, "Hat" },
                    { 80, "Hat" },
                    { 81, "Gloves" },
                    { 82, "Chips" },
                    { 83, "Chair" },
                    { 84, "Salad" },
                    { 85, "Mouse" },
                    { 86, "Ball" },
                    { 87, "Cheese" },
                    { 88, "Mouse" },
                    { 89, "Table" },
                    { 90, "Bike" },
                    { 91, "Shoes" },
                    { 92, "Shoes" },
                    { 93, "Gloves" },
                    { 94, "Pants" },
                    { 95, "Ball" },
                    { 96, "Chicken" },
                    { 97, "Shoes" },
                    { 98, "Ball" },
                    { 99, "Hat" },
                    { 100, "Tuna" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Turcotte, Toy and Welch" },
                    { 2, "Turcotte - Nicolas" },
                    { 3, "Hoeger, Klocko and Greenholt" },
                    { 4, "Nader Group" },
                    { 5, "Lueilwitz - Simonis" },
                    { 6, "Blick, Waelchi and Gutkowski" },
                    { 7, "Parisian, Flatley and Volkman" },
                    { 8, "Harber - McKenzie" },
                    { 9, "Rippin - Bergstrom" },
                    { 10, "West Group" },
                    { 11, "Muller, Schuster and Lubowitz" },
                    { 12, "Maggio, Hagenes and Homenick" },
                    { 13, "Mante - Walsh" },
                    { 14, "Mraz - Gutmann" },
                    { 15, "White Inc" },
                    { 16, "Hyatt LLC" },
                    { 17, "Crona, Schuster and Osinski" },
                    { 18, "Stiedemann and Sons" },
                    { 19, "Champlin - Volkman" },
                    { 20, "Buckridge Group" },
                    { 21, "Goyette, Powlowski and Auer" },
                    { 22, "Kuhic, Bogisich and Marvin" },
                    { 23, "Von LLC" },
                    { 24, "Nicolas, VonRueden and Toy" },
                    { 25, "Abbott, Heaney and Marvin" },
                    { 26, "MacGyver - McCullough" },
                    { 27, "Leannon - Wisoky" },
                    { 28, "Runolfsson - Farrell" },
                    { 29, "Simonis, Emmerich and Bernhard" },
                    { 30, "Kutch, Bradtke and Murray" },
                    { 31, "Wisozk, Gutmann and Klein" },
                    { 32, "Jaskolski, Kreiger and Lubowitz" },
                    { 33, "Wilkinson and Sons" },
                    { 34, "Legros, Mertz and Howe" },
                    { 35, "Bode, Cormier and Adams" },
                    { 36, "Corwin - Wiegand" },
                    { 37, "Weber Group" },
                    { 38, "Stoltenberg - Waters" },
                    { 39, "Hane - Sporer" },
                    { 40, "Prosacco - Anderson" },
                    { 41, "Grimes - Von" },
                    { 42, "Ernser Group" },
                    { 43, "Hand - Wolf" },
                    { 44, "Schuster, Abshire and Macejkovic" },
                    { 45, "Lind, Deckow and Toy" },
                    { 46, "Ankunding, Koelpin and Leffler" },
                    { 47, "Lueilwitz LLC" },
                    { 48, "Pfeffer, O'Connell and Marks" },
                    { 49, "Hills and Sons" },
                    { 50, "Goldner - Weber" },
                    { 51, "Cormier, Lynch and Spencer" },
                    { 52, "Nikolaus - Blanda" },
                    { 53, "Rath LLC" },
                    { 54, "Bednar, Feeney and Schmitt" },
                    { 55, "Harvey - Marquardt" },
                    { 56, "Baumbach - Donnelly" },
                    { 57, "Weber Inc" },
                    { 58, "Pfeffer - Stiedemann" },
                    { 59, "Hoppe - Huels" },
                    { 60, "Sipes and Sons" },
                    { 61, "Morar - Dietrich" },
                    { 62, "Lowe and Sons" },
                    { 63, "Wiegand - Kozey" },
                    { 64, "Stamm, Botsford and Haley" },
                    { 65, "Herzog Inc" },
                    { 66, "Schultz, Wiza and Dooley" },
                    { 67, "Beatty LLC" },
                    { 68, "Gulgowski, Rippin and Conroy" },
                    { 69, "Rohan Inc" },
                    { 70, "Corwin, Morissette and Buckridge" },
                    { 71, "McLaughlin, Marquardt and Schuppe" },
                    { 72, "Barrows - Wehner" },
                    { 73, "Towne - Beier" },
                    { 74, "Balistreri, Robel and Murazik" },
                    { 75, "Waelchi - Mraz" },
                    { 76, "Ortiz - Rodriguez" },
                    { 77, "Dietrich LLC" },
                    { 78, "Schulist, Heidenreich and Bode" },
                    { 79, "Fisher LLC" },
                    { 80, "Hintz LLC" },
                    { 81, "Emmerich, Swift and Barton" },
                    { 82, "White Group" },
                    { 83, "Cole Group" },
                    { 84, "D'Amore, Witting and Nikolaus" },
                    { 85, "Gutkowski, Gulgowski and Satterfield" },
                    { 86, "Casper, Haag and Ward" },
                    { 87, "Walker - Prohaska" },
                    { 88, "Lueilwitz LLC" },
                    { 89, "Wintheiser, Trantow and White" },
                    { 90, "Orn, Reilly and Goldner" },
                    { 91, "McCullough - Gerhold" },
                    { 92, "Ruecker, Beatty and Lang" },
                    { 93, "Gerhold - Russel" },
                    { 94, "Cassin, Bailey and Tillman" },
                    { 95, "Vandervort - Powlowski" },
                    { 96, "Muller LLC" },
                    { 97, "Hills Group" },
                    { 98, "Brakus LLC" },
                    { 99, "Dibbert - Ritchie" },
                    { 100, "Rodriguez - Grant" }
                });

            migrationBuilder.InsertData(
                table: "SupplierStoreItems",
                columns: new[] { "Id", "Price", "StoreItemId", "SupplierId" },
                values: new object[,]
                {
                    { 1, 0m, 79, 80 },
                    { 2, 0m, 34, 15 },
                    { 3, 0m, 41, 47 },
                    { 4, 0m, 47, 81 },
                    { 5, 0m, 57, 36 },
                    { 6, 0m, 42, 75 },
                    { 7, 0m, 92, 25 },
                    { 8, 0m, 72, 64 },
                    { 9, 0m, 48, 47 },
                    { 10, 0m, 41, 62 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemSupplier_OrderId",
                table: "OrderItemSupplier",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemSupplier_SupplierId",
                table: "OrderItemSupplier",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemSupplier_SupplierStoreItemId",
                table: "OrderItemSupplier",
                column: "SupplierStoreItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SupplierId",
                table: "Orders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_QuarterlyPlans_SupplierId",
                table: "QuarterlyPlans",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItemSupplier");

            migrationBuilder.DropTable(
                name: "QuarterlyPlans");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "SupplierStoreItems");

            migrationBuilder.DropTable(
                name: "StoreItems");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
