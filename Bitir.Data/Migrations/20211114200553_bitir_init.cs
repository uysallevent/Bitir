using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Module.Shared.Migrations;

namespace Bitir.Data.Migrations
{
    public partial class bitir_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.EnsureSchema(
                name: "product");

            migrationBuilder.EnsureSchema(
                name: "sales");

            migrationBuilder.CreateTable(
                name: "Province",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccount",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    AccountTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    SubCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Abbreviation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carrier",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Plate = table.Column<string>(nullable: false),
                    Capacity = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "District",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ProvinceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                    table.ForeignKey(
                        name: "FK_District_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalSchema: "auth",
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAddress",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    DistrictId = table.Column<int>(nullable: false),
                    Longitude = table.Column<double>(nullable: true),
                    Latitude = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAddress_UserAccount_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: false),
                    RefreshTokenExpirationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToken_UserAccount_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "product",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carrier_Store",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(nullable: false),
                    CarrierId = table.Column<int>(nullable: false),
                    StoreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrier_Store", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carrier_Store_Carrier_CarrierId",
                        column: x => x.CarrierId,
                        principalSchema: "sales",
                        principalTable: "Carrier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carrier_Store_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "sales",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Store_UserAccount",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    StoreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store_UserAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Store_UserAccount_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "sales",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Store_UserAccount_UserAccount_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Neighbourhood",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    LocalityName = table.Column<string>(nullable: false),
                    DistrictId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Neighbourhood", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Neighbourhood_District_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "auth",
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductQuantity",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQuantity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductQuantity_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductQuantity_Unit_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "product",
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductStore",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(nullable: false),
                    ProductQuantityId = table.Column<int>(nullable: false),
                    StoreId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductStore_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductStore_ProductQuantity_ProductQuantityId",
                        column: x => x.ProductQuantityId,
                        principalSchema: "product",
                        principalTable: "ProductQuantity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductStore_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "sales",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPrice",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertDate = table.Column<DateTime>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ProductStoreId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPrice_ProductStore_ProductStoreId",
                        column: x => x.ProductStoreId,
                        principalSchema: "product",
                        principalTable: "ProductStore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductStock",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ProductStoreId = table.Column<int>(nullable: false),
                    CarrierId = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductStock_Carrier_CarrierId",
                        column: x => x.CarrierId,
                        principalSchema: "sales",
                        principalTable: "Carrier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductStock_ProductStore_ProductStoreId",
                        column: x => x.ProductStoreId,
                        principalSchema: "product",
                        principalTable: "ProductStore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    UserAddressId = table.Column<int>(nullable: false),
                    CarrierId = table.Column<int>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    OrderStatus = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Product_StoreId = table.Column<int>(nullable: true),
                    StoreId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Carrier_CarrierId",
                        column: x => x.CarrierId,
                        principalSchema: "sales",
                        principalTable: "Carrier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_ProductStore_Product_StoreId",
                        column: x => x.Product_StoreId,
                        principalSchema: "product",
                        principalTable: "ProductStore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "sales",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_UserAccount_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    ProductStoreId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "sales",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_ProductStore_ProductStoreId",
                        column: x => x.ProductStoreId,
                        principalSchema: "product",
                        principalTable: "ProductStore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "UserAccount",
                columns: new[] { "Id", "AccountTypeId", "Email", "InsertDate", "Name", "PasswordHash", "PasswordSalt", "Phone", "Status", "Surname", "UpdateDate", "Username" },
                values: new object[,]
                {
                    { 1, 1, "t@t.com", new DateTime(2021, 11, 14, 23, 5, 53, 62, DateTimeKind.Local).AddTicks(907), "test", new byte[] { 68, 109, 248, 197, 240, 224, 22, 90, 24, 136, 67, 243, 154, 70, 111, 93, 148, 222, 61, 183, 100, 227, 247, 83, 176, 27, 211, 154, 132, 190, 32, 210, 140, 207, 100, 178, 220, 3, 37, 101, 227, 121, 214, 206, 107, 241, 235, 4, 126, 62, 222, 89, 245, 211, 152, 249, 39, 89, 18, 193, 134, 7, 160, 146 }, new byte[] { 250, 82, 152, 81, 97, 102, 44, 187, 135, 2, 213, 186, 7, 77, 21, 214, 165, 156, 34, 35, 178, 19, 65, 6, 10, 230, 30, 137, 35, 208, 128, 181, 230, 95, 129, 91, 247, 95, 121, 12, 96, 70, 79, 134, 240, 150, 207, 140, 220, 142, 128, 131, 14, 33, 62, 82, 250, 91, 193, 172, 89, 178, 92, 214, 222, 149, 117, 54, 225, 250, 187, 190, 217, 114, 249, 202, 11, 143, 128, 138, 25, 154, 17, 129, 22, 47, 54, 146, 75, 167, 17, 50, 132, 92, 157, 239, 158, 195, 126, 247, 177, 197, 138, 52, 92, 195, 218, 32, 172, 36, 236, 156, 33, 13, 160, 203, 78, 181, 79, 86, 192, 8, 127, 252, 217, 109, 34, 162 }, "505", 1, "test", new DateTime(2021, 11, 14, 23, 5, 53, 62, DateTimeKind.Local).AddTicks(8458), "admin" },
                    { 2, 2, "q@q.com", new DateTime(2021, 11, 14, 23, 5, 53, 62, DateTimeKind.Local).AddTicks(9829), "Vendor", new byte[] { 68, 109, 248, 197, 240, 224, 22, 90, 24, 136, 67, 243, 154, 70, 111, 93, 148, 222, 61, 183, 100, 227, 247, 83, 176, 27, 211, 154, 132, 190, 32, 210, 140, 207, 100, 178, 220, 3, 37, 101, 227, 121, 214, 206, 107, 241, 235, 4, 126, 62, 222, 89, 245, 211, 152, 249, 39, 89, 18, 193, 134, 7, 160, 146 }, new byte[] { 250, 82, 152, 81, 97, 102, 44, 187, 135, 2, 213, 186, 7, 77, 21, 214, 165, 156, 34, 35, 178, 19, 65, 6, 10, 230, 30, 137, 35, 208, 128, 181, 230, 95, 129, 91, 247, 95, 121, 12, 96, 70, 79, 134, 240, 150, 207, 140, 220, 142, 128, 131, 14, 33, 62, 82, 250, 91, 193, 172, 89, 178, 92, 214, 222, 149, 117, 54, 225, 250, 187, 190, 217, 114, 249, 202, 11, 143, 128, 138, 25, 154, 17, 129, 22, 47, 54, 146, 75, 167, 17, 50, 132, 92, 157, 239, 158, 195, 126, 247, 177, 197, 138, 52, 92, 195, 218, 32, 172, 36, 236, 156, 33, 13, 160, 203, 78, 181, 79, 86, 192, 8, 127, 252, 217, 109, 34, 162 }, "505", 1, "test", new DateTime(2021, 11, 14, 23, 5, 53, 62, DateTimeKind.Local).AddTicks(9836), "vendor" }
                });

            migrationBuilder.InsertData(
                schema: "product",
                table: "Category",
                columns: new[] { "Id", "InsertDate", "Name", "Status", "SubCategoryId", "UpdateDate" },
                values: new object[] { 1, new DateTime(2021, 11, 14, 23, 5, 53, 70, DateTimeKind.Local).AddTicks(6291), "Süt Ürünleri", 1, 0, new DateTime(2021, 11, 14, 23, 5, 53, 70, DateTimeKind.Local).AddTicks(6348) });

            migrationBuilder.InsertData(
                schema: "product",
                table: "Unit",
                columns: new[] { "Id", "Abbreviation", "InsertDate", "Name", "Status", "UpdateDate" },
                values: new object[] { 1, "lt", new DateTime(2021, 11, 14, 23, 5, 53, 77, DateTimeKind.Local).AddTicks(7487), "Litre", 1, new DateTime(2021, 11, 14, 23, 5, 53, 77, DateTimeKind.Local).AddTicks(7498) });

            migrationBuilder.InsertData(
                schema: "product",
                table: "Product",
                columns: new[] { "Id", "CategoryId", "Description", "Image", "InsertDate", "Name", "Status", "UpdateDate" },
                values: new object[] { 1, 1, "Günlük İnek Sütü", null, new DateTime(2021, 11, 14, 23, 5, 53, 74, DateTimeKind.Local).AddTicks(7597), "Süt", 1, new DateTime(2021, 11, 14, 23, 5, 53, 74, DateTimeKind.Local).AddTicks(7613) });

            migrationBuilder.InsertData(
                schema: "product",
                table: "ProductQuantity",
                columns: new[] { "Id", "InsertDate", "ProductId", "Quantity", "Status", "UnitId", "UpdateDate" },
                values: new object[] { 1, new DateTime(2021, 11, 14, 23, 5, 53, 79, DateTimeKind.Local).AddTicks(967), 1, 1m, 1, 1, new DateTime(2021, 11, 14, 23, 5, 53, 79, DateTimeKind.Local).AddTicks(976) });

            migrationBuilder.InsertData(
                schema: "product",
                table: "ProductQuantity",
                columns: new[] { "Id", "InsertDate", "ProductId", "Quantity", "Status", "UnitId", "UpdateDate" },
                values: new object[] { 2, new DateTime(2021, 11, 14, 23, 5, 53, 79, DateTimeKind.Local).AddTicks(1067), 1, 2m, 1, 1, new DateTime(2021, 11, 14, 23, 5, 53, 79, DateTimeKind.Local).AddTicks(1068) });

            migrationBuilder.CreateIndex(
                name: "IX_District_ProvinceId",
                schema: "auth",
                table: "District",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Neighbourhood_DistrictId",
                schema: "auth",
                table: "Neighbourhood",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_UserId",
                schema: "auth",
                table: "UserAddress",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_UserId",
                schema: "auth",
                table: "UserToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                schema: "product",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrice_ProductStoreId",
                schema: "product",
                table: "ProductPrice",
                column: "ProductStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantity_ProductId",
                schema: "product",
                table: "ProductQuantity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantity_UnitId",
                schema: "product",
                table: "ProductQuantity",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStock_CarrierId",
                schema: "product",
                table: "ProductStock",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStock_ProductStoreId",
                schema: "product",
                table: "ProductStock",
                column: "ProductStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStore_ProductId",
                schema: "product",
                table: "ProductStore",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStore_ProductQuantityId",
                schema: "product",
                table: "ProductStore",
                column: "ProductQuantityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStore_StoreId",
                schema: "product",
                table: "ProductStore",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Carrier_Store_CarrierId",
                schema: "sales",
                table: "Carrier_Store",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_Carrier_Store_StoreId",
                schema: "sales",
                table: "Carrier_Store",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CarrierId",
                schema: "sales",
                table: "Order",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Product_StoreId",
                schema: "sales",
                table: "Order",
                column: "Product_StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_StoreId",
                schema: "sales",
                table: "Order",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                schema: "sales",
                table: "Order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                schema: "sales",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductStoreId",
                schema: "sales",
                table: "OrderDetail",
                column: "ProductStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_UserAccount_StoreId",
                schema: "sales",
                table: "Store_UserAccount",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_UserAccount_UserId",
                schema: "sales",
                table: "Store_UserAccount",
                column: "UserId");

            ViewMigrationHelper.ViewMigrations(migrationBuilder);
            StoreProcedureMigrationHelper.ProductProcedures(migrationBuilder);
            ProvinceDistrictMigrationHelper.ProvinceDistrict(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Neighbourhood",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "UserAddress",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "UserToken",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "ProductPrice",
                schema: "product");

            migrationBuilder.DropTable(
                name: "ProductStock",
                schema: "product");

            migrationBuilder.DropTable(
                name: "Carrier_Store",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "OrderDetail",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "Store_UserAccount",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "District",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "Province",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "Carrier",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "ProductStore",
                schema: "product");

            migrationBuilder.DropTable(
                name: "UserAccount",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "ProductQuantity",
                schema: "product");

            migrationBuilder.DropTable(
                name: "Store",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "product");

            migrationBuilder.DropTable(
                name: "Unit",
                schema: "product");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "product");
        }
    }
}
