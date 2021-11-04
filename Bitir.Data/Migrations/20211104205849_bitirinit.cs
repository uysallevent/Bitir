using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Module.Shared.Migrations;

namespace Bitir.Data.Migrations
{
    public partial class bitirinit : Migration
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
                    ProductQuantity = table.Column<decimal>(nullable: false),
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
                name: "StoreOrdersView",
                schema: "sales",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(nullable: false),
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
                    ProductQuantity = table.Column<decimal>(nullable: false),
                    ProductUnit = table.Column<string>(nullable: true),
                    ProductUnitAbbreviation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreOrdersView", x => x.OrderId);
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
                    { 1, 1, "t@t.com", new DateTime(2021, 11, 4, 23, 58, 49, 350, DateTimeKind.Local).AddTicks(8767), "test", new byte[] { 85, 221, 178, 169, 244, 56, 228, 134, 154, 80, 172, 140, 29, 86, 36, 190, 209, 170, 84, 139, 6, 110, 175, 128, 70, 255, 131, 50, 35, 164, 87, 23, 43, 53, 218, 202, 183, 203, 195, 188, 6, 211, 0, 247, 248, 46, 139, 52, 172, 253, 8, 86, 180, 178, 153, 216, 232, 111, 172, 208, 8, 213, 140, 128 }, new byte[] { 76, 130, 118, 234, 237, 233, 135, 221, 164, 39, 200, 139, 53, 77, 141, 158, 8, 2, 151, 90, 79, 193, 93, 176, 62, 113, 254, 196, 26, 191, 177, 213, 45, 183, 129, 47, 217, 212, 210, 199, 229, 106, 192, 149, 34, 174, 59, 228, 31, 249, 39, 193, 53, 128, 43, 135, 212, 174, 60, 100, 31, 124, 234, 78, 177, 4, 100, 41, 133, 42, 244, 214, 63, 191, 224, 79, 239, 167, 148, 204, 33, 121, 241, 233, 245, 131, 83, 160, 136, 143, 252, 64, 188, 144, 199, 141, 5, 161, 14, 225, 225, 22, 42, 78, 233, 134, 66, 69, 104, 44, 96, 155, 129, 228, 234, 181, 14, 47, 192, 253, 135, 46, 232, 115, 73, 170, 5, 210 }, "505", 1, "test", new DateTime(2021, 11, 4, 23, 58, 49, 351, DateTimeKind.Local).AddTicks(4913), "admin" },
                    { 2, 2, "q@q.com", new DateTime(2021, 11, 4, 23, 58, 49, 351, DateTimeKind.Local).AddTicks(6076), "Vendor", new byte[] { 85, 221, 178, 169, 244, 56, 228, 134, 154, 80, 172, 140, 29, 86, 36, 190, 209, 170, 84, 139, 6, 110, 175, 128, 70, 255, 131, 50, 35, 164, 87, 23, 43, 53, 218, 202, 183, 203, 195, 188, 6, 211, 0, 247, 248, 46, 139, 52, 172, 253, 8, 86, 180, 178, 153, 216, 232, 111, 172, 208, 8, 213, 140, 128 }, new byte[] { 76, 130, 118, 234, 237, 233, 135, 221, 164, 39, 200, 139, 53, 77, 141, 158, 8, 2, 151, 90, 79, 193, 93, 176, 62, 113, 254, 196, 26, 191, 177, 213, 45, 183, 129, 47, 217, 212, 210, 199, 229, 106, 192, 149, 34, 174, 59, 228, 31, 249, 39, 193, 53, 128, 43, 135, 212, 174, 60, 100, 31, 124, 234, 78, 177, 4, 100, 41, 133, 42, 244, 214, 63, 191, 224, 79, 239, 167, 148, 204, 33, 121, 241, 233, 245, 131, 83, 160, 136, 143, 252, 64, 188, 144, 199, 141, 5, 161, 14, 225, 225, 22, 42, 78, 233, 134, 66, 69, 104, 44, 96, 155, 129, 228, 234, 181, 14, 47, 192, 253, 135, 46, 232, 115, 73, 170, 5, 210 }, "505", 1, "test", new DateTime(2021, 11, 4, 23, 58, 49, 351, DateTimeKind.Local).AddTicks(6081), "vendor" }
                });

            migrationBuilder.InsertData(
                schema: "product",
                table: "Category",
                columns: new[] { "Id", "InsertDate", "Name", "Status", "SubCategoryId", "UpdateDate" },
                values: new object[] { 1, new DateTime(2021, 11, 4, 23, 58, 49, 357, DateTimeKind.Local).AddTicks(6359), "Süt Ürünleri", 1, 0, new DateTime(2021, 11, 4, 23, 58, 49, 357, DateTimeKind.Local).AddTicks(6383) });

            migrationBuilder.InsertData(
                schema: "product",
                table: "Unit",
                columns: new[] { "Id", "Abbreviation", "InsertDate", "Name", "Status", "UpdateDate" },
                values: new object[] { 1, "lt", new DateTime(2021, 11, 4, 23, 58, 49, 364, DateTimeKind.Local).AddTicks(3912), "Litre", 1, new DateTime(2021, 11, 4, 23, 58, 49, 364, DateTimeKind.Local).AddTicks(3921) });

            migrationBuilder.InsertData(
                schema: "product",
                table: "Product",
                columns: new[] { "Id", "CategoryId", "Description", "Image", "InsertDate", "Name", "Status", "UpdateDate" },
                values: new object[] { 1, 1, "Günlük İnek Sütü", null, new DateTime(2021, 11, 4, 23, 58, 49, 361, DateTimeKind.Local).AddTicks(3986), "Süt", 1, new DateTime(2021, 11, 4, 23, 58, 49, 361, DateTimeKind.Local).AddTicks(3995) });

            migrationBuilder.InsertData(
                schema: "product",
                table: "ProductQuantity",
                columns: new[] { "Id", "InsertDate", "ProductId", "Quantity", "Status", "UnitId", "UpdateDate" },
                values: new object[] { 1, new DateTime(2021, 11, 4, 23, 58, 49, 365, DateTimeKind.Local).AddTicks(6643), 1, 1m, 1, 1, new DateTime(2021, 11, 4, 23, 58, 49, 365, DateTimeKind.Local).AddTicks(6651) });

            migrationBuilder.InsertData(
                schema: "product",
                table: "ProductQuantity",
                columns: new[] { "Id", "InsertDate", "ProductId", "Quantity", "Status", "UnitId", "UpdateDate" },
                values: new object[] { 2, new DateTime(2021, 11, 4, 23, 58, 49, 365, DateTimeKind.Local).AddTicks(6713), 1, 2m, 1, 1, new DateTime(2021, 11, 4, 23, 58, 49, 365, DateTimeKind.Local).AddTicks(6714) });

            migrationBuilder.CreateIndex(
                name: "IX_District_ProvinceId",
                schema: "auth",
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

            //ViewMigrationHelper.ViewMigrations(migrationBuilder);
            StoreProcedureMigrationHelper.ProductProcedures(migrationBuilder);
            ProvinceDistrictMigrationHelper.ProvinceDistrict(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreOrderViewModel");

            migrationBuilder.DropTable(
                name: "StoreProductByCarrierViewModel");

            migrationBuilder.DropTable(
                name: "StoreProductByStoreViewModel");

            migrationBuilder.DropTable(
                name: "StoreProductViewModel");

            migrationBuilder.DropTable(
                name: "District",
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
                name: "StoreOrdersView",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "Province",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "Carrier",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "Order",
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
