using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Module.Shared.Migrations;

namespace Bitir.Data.Migrations
{
    public partial class bitirInit : Migration
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
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreOrderViewModel",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    UserAddressId = table.Column<int>(nullable: false),
                    DistrictId = table.Column<int>(nullable: false),
                    ProvinceId = table.Column<int>(nullable: false),
                    ProductStoreId = table.Column<int>(nullable: false),
                    ProductQuantityId = table.Column<int>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    OrderProvinceName = table.Column<string>(nullable: true),
                    OrderDistrictName = table.Column<string>(nullable: true),
                    OrderAddress = table.Column<string>(nullable: true),
                    OrderQuantity = table.Column<int>(nullable: false),
                    OrderStatus = table.Column<int>(nullable: false),
                    OrderNote = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ProductQuantity = table.Column<int>(nullable: false),
                    ProductUnit = table.Column<string>(nullable: true),
                    ProductUnitAbbreviation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreOrderViewModel", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "StoreProductByCarrierViewModel",
                columns: table => new
                {
                    ProductStockId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductStoreId = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    UnitName = table.Column<string>(nullable: true),
                    Abbreviation = table.Column<string>(nullable: true),
                    Plate = table.Column<string>(nullable: true),
                    Capacity = table.Column<int>(nullable: false),
                    ProductStock = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProductByCarrierViewModel", x => x.ProductStockId);
                });

            migrationBuilder.CreateTable(
                name: "StoreProductByStoreViewModel",
                columns: table => new
                {
                    ProductStockId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductStoreId = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    UnitName = table.Column<string>(nullable: true),
                    Abbreviation = table.Column<string>(nullable: true),
                    Plate = table.Column<string>(nullable: true),
                    Capacity = table.Column<int>(nullable: true),
                    ProductStock = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProductByStoreViewModel", x => x.ProductStockId);
                });

            migrationBuilder.CreateTable(
                name: "StoreProductViewModel",
                columns: table => new
                {
                    ProductStockId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<int>(nullable: false),
                    ProductStoreId = table.Column<int>(nullable: false),
                    ProductPriceId = table.Column<int>(nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Quantity = table.Column<decimal>(nullable: false),
                    Unit = table.Column<string>(nullable: true),
                    Abbreviation = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProductViewModel", x => x.ProductStockId);
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
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                    table.ForeignKey(
                        name: "FK_District_Province_ProvinceId",
                        column: x => x.ProvinceId,
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
                    ProductStoreId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    OrderStatus = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    StoreId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_ProductStore_ProductStoreId",
                        column: x => x.ProductStoreId,
                        principalSchema: "product",
                        principalTable: "ProductStore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.InsertData(
                schema: "auth",
                table: "UserAccount",
                columns: new[] { "Id", "AccountTypeId", "Email", "InsertDate", "Name", "PasswordHash", "PasswordSalt", "Phone", "Status", "Surname", "UpdateDate", "Username" },
                values: new object[,]
                {
                    { 1, 1, "t@t.com", new DateTime(2021, 10, 31, 1, 47, 34, 466, DateTimeKind.Local).AddTicks(3001), "test", new byte[] { 1, 154, 200, 3, 222, 206, 114, 208, 177, 173, 154, 242, 230, 52, 187, 246, 23, 236, 212, 73, 106, 72, 171, 240, 1, 248, 204, 92, 137, 99, 9, 16, 126, 43, 204, 152, 38, 60, 147, 130, 199, 116, 218, 75, 177, 61, 184, 78, 103, 220, 200, 60, 151, 210, 161, 216, 25, 65, 245, 1, 115, 67, 195, 196 }, new byte[] { 156, 94, 85, 179, 227, 171, 205, 189, 83, 3, 121, 0, 100, 62, 95, 31, 135, 130, 122, 114, 6, 203, 227, 21, 133, 181, 98, 241, 120, 96, 199, 37, 39, 111, 192, 141, 1, 228, 120, 249, 142, 103, 236, 97, 80, 137, 204, 120, 28, 201, 116, 175, 36, 205, 123, 183, 78, 164, 31, 239, 110, 107, 169, 235, 176, 127, 81, 66, 154, 152, 10, 214, 146, 247, 39, 157, 12, 116, 150, 120, 172, 159, 111, 203, 104, 248, 177, 59, 72, 128, 98, 157, 39, 139, 33, 150, 119, 203, 176, 131, 239, 232, 197, 142, 218, 165, 122, 136, 64, 190, 198, 209, 42, 73, 220, 234, 49, 186, 1, 243, 11, 68, 193, 83, 52, 209, 36, 3 }, "505", 1, "test", new DateTime(2021, 10, 31, 1, 47, 34, 466, DateTimeKind.Local).AddTicks(8831), "admin" },
                    { 2, 2, "q@q.com", new DateTime(2021, 10, 31, 1, 47, 34, 466, DateTimeKind.Local).AddTicks(9928), "Vendor", new byte[] { 1, 154, 200, 3, 222, 206, 114, 208, 177, 173, 154, 242, 230, 52, 187, 246, 23, 236, 212, 73, 106, 72, 171, 240, 1, 248, 204, 92, 137, 99, 9, 16, 126, 43, 204, 152, 38, 60, 147, 130, 199, 116, 218, 75, 177, 61, 184, 78, 103, 220, 200, 60, 151, 210, 161, 216, 25, 65, 245, 1, 115, 67, 195, 196 }, new byte[] { 156, 94, 85, 179, 227, 171, 205, 189, 83, 3, 121, 0, 100, 62, 95, 31, 135, 130, 122, 114, 6, 203, 227, 21, 133, 181, 98, 241, 120, 96, 199, 37, 39, 111, 192, 141, 1, 228, 120, 249, 142, 103, 236, 97, 80, 137, 204, 120, 28, 201, 116, 175, 36, 205, 123, 183, 78, 164, 31, 239, 110, 107, 169, 235, 176, 127, 81, 66, 154, 152, 10, 214, 146, 247, 39, 157, 12, 116, 150, 120, 172, 159, 111, 203, 104, 248, 177, 59, 72, 128, 98, 157, 39, 139, 33, 150, 119, 203, 176, 131, 239, 232, 197, 142, 218, 165, 122, 136, 64, 190, 198, 209, 42, 73, 220, 234, 49, 186, 1, 243, 11, 68, 193, 83, 52, 209, 36, 3 }, "505", 1, "test", new DateTime(2021, 10, 31, 1, 47, 34, 466, DateTimeKind.Local).AddTicks(9933), "vendor" }
                });

            migrationBuilder.InsertData(
                schema: "product",
                table: "Category",
                columns: new[] { "Id", "InsertDate", "Name", "Status", "SubCategoryId", "UpdateDate" },
                values: new object[] { 1, new DateTime(2021, 10, 31, 1, 47, 34, 471, DateTimeKind.Local).AddTicks(6858), "Süt Ürünleri", 1, 0, new DateTime(2021, 10, 31, 1, 47, 34, 471, DateTimeKind.Local).AddTicks(6887) });

            migrationBuilder.InsertData(
                schema: "product",
                table: "Unit",
                columns: new[] { "Id", "Abbreviation", "InsertDate", "Name", "Status", "UpdateDate" },
                values: new object[] { 1, "lt", new DateTime(2021, 10, 31, 1, 47, 34, 477, DateTimeKind.Local).AddTicks(9356), "Litre", 1, new DateTime(2021, 10, 31, 1, 47, 34, 477, DateTimeKind.Local).AddTicks(9364) });

            migrationBuilder.InsertData(
                schema: "product",
                table: "Product",
                columns: new[] { "Id", "CategoryId", "Description", "Image", "InsertDate", "Name", "Status", "UpdateDate" },
                values: new object[] { 1, 1, "Günlük İnek Sütü", null, new DateTime(2021, 10, 31, 1, 47, 34, 475, DateTimeKind.Local).AddTicks(2718), "Süt", 1, new DateTime(2021, 10, 31, 1, 47, 34, 475, DateTimeKind.Local).AddTicks(2726) });

            migrationBuilder.InsertData(
                schema: "product",
                table: "ProductQuantity",
                columns: new[] { "Id", "InsertDate", "ProductId", "Quantity", "Status", "UnitId", "UpdateDate" },
                values: new object[] { 1, new DateTime(2021, 10, 31, 1, 47, 34, 479, DateTimeKind.Local).AddTicks(1460), 1, 1m, 1, 1, new DateTime(2021, 10, 31, 1, 47, 34, 479, DateTimeKind.Local).AddTicks(1466) });

            migrationBuilder.InsertData(
                schema: "product",
                table: "ProductQuantity",
                columns: new[] { "Id", "InsertDate", "ProductId", "Quantity", "Status", "UnitId", "UpdateDate" },
                values: new object[] { 2, new DateTime(2021, 10, 31, 1, 47, 34, 479, DateTimeKind.Local).AddTicks(1527), 1, 2m, 1, 1, new DateTime(2021, 10, 31, 1, 47, 34, 479, DateTimeKind.Local).AddTicks(1529) });

            migrationBuilder.CreateIndex(
                name: "IX_District_ProvinceId",
                table: "District",
                column: "ProvinceId");

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
                name: "IX_Order_ProductStoreId",
                schema: "sales",
                table: "Order",
                column: "ProductStoreId");

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
                name: "IX_Store_UserAccount_StoreId",
                schema: "sales",
                table: "Store_UserAccount",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_UserAccount_UserId",
                schema: "sales",
                table: "Store_UserAccount",
                column: "UserId");
            StoreProcedureMigrationHelper.ProductProcedures(migrationBuilder);
            ProvinceDistrictMigrationHelper.ProvinceDistrict(migrationBuilder);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "StoreOrderViewModel");

            migrationBuilder.DropTable(
                name: "StoreProductByCarrierViewModel");

            migrationBuilder.DropTable(
                name: "StoreProductByStoreViewModel");

            migrationBuilder.DropTable(
                name: "StoreProductViewModel");

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
                name: "Order",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "Store_UserAccount",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "Province");

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
