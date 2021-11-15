using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bitir.Data.Migrations
{
    public partial class carrier_distribution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "CarrierDistributionZone",
                newName: "CarrierDistributionZone",
                newSchema: "sales");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                schema: "sales",
                table: "CarrierDistributionZone",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertDate",
                schema: "sales",
                table: "CarrierDistributionZone",
                type: "datetime",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InsertDate", "PasswordHash", "PasswordSalt", "UpdateDate" },
                values: new object[] { new DateTime(2021, 11, 16, 1, 15, 14, 387, DateTimeKind.Local).AddTicks(1271), new byte[] { 75, 38, 103, 93, 54, 164, 91, 214, 177, 69, 155, 54, 221, 0, 66, 114, 68, 131, 158, 134, 230, 85, 202, 71, 14, 100, 4, 1, 138, 47, 175, 246, 237, 137, 222, 148, 79, 223, 87, 85, 187, 97, 197, 91, 107, 215, 210, 40, 227, 177, 85, 202, 107, 238, 160, 102, 218, 98, 115, 6, 131, 72, 131, 172 }, new byte[] { 195, 134, 211, 147, 227, 7, 20, 99, 45, 186, 45, 142, 186, 73, 236, 44, 231, 83, 80, 43, 49, 169, 80, 167, 244, 229, 246, 127, 168, 255, 139, 37, 156, 21, 140, 155, 52, 87, 139, 237, 84, 113, 200, 194, 6, 210, 206, 145, 150, 34, 153, 74, 89, 118, 81, 16, 156, 221, 220, 64, 185, 43, 247, 6, 135, 254, 195, 183, 92, 235, 112, 126, 122, 43, 167, 11, 210, 60, 38, 180, 24, 164, 161, 255, 159, 34, 147, 231, 71, 129, 208, 143, 187, 199, 66, 49, 77, 7, 136, 13, 18, 98, 200, 196, 60, 150, 63, 71, 118, 225, 14, 139, 92, 236, 7, 123, 73, 244, 107, 51, 88, 83, 210, 224, 67, 150, 176, 91 }, new DateTime(2021, 11, 16, 1, 15, 14, 387, DateTimeKind.Local).AddTicks(7599) });

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "InsertDate", "PasswordHash", "PasswordSalt", "UpdateDate" },
                values: new object[] { new DateTime(2021, 11, 16, 1, 15, 14, 387, DateTimeKind.Local).AddTicks(8832), new byte[] { 75, 38, 103, 93, 54, 164, 91, 214, 177, 69, 155, 54, 221, 0, 66, 114, 68, 131, 158, 134, 230, 85, 202, 71, 14, 100, 4, 1, 138, 47, 175, 246, 237, 137, 222, 148, 79, 223, 87, 85, 187, 97, 197, 91, 107, 215, 210, 40, 227, 177, 85, 202, 107, 238, 160, 102, 218, 98, 115, 6, 131, 72, 131, 172 }, new byte[] { 195, 134, 211, 147, 227, 7, 20, 99, 45, 186, 45, 142, 186, 73, 236, 44, 231, 83, 80, 43, 49, 169, 80, 167, 244, 229, 246, 127, 168, 255, 139, 37, 156, 21, 140, 155, 52, 87, 139, 237, 84, 113, 200, 194, 6, 210, 206, 145, 150, 34, 153, 74, 89, 118, 81, 16, 156, 221, 220, 64, 185, 43, 247, 6, 135, 254, 195, 183, 92, 235, 112, 126, 122, 43, 167, 11, 210, 60, 38, 180, 24, 164, 161, 255, 159, 34, 147, 231, 71, 129, 208, 143, 187, 199, 66, 49, 77, 7, 136, 13, 18, 98, 200, 196, 60, 150, 63, 71, 118, 225, 14, 139, 92, 236, 7, 123, 73, 244, 107, 51, 88, 83, 210, 224, 67, 150, 176, 91 }, new DateTime(2021, 11, 16, 1, 15, 14, 387, DateTimeKind.Local).AddTicks(8837) });

            migrationBuilder.UpdateData(
                schema: "product",
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2021, 11, 16, 1, 15, 14, 394, DateTimeKind.Local).AddTicks(8824), new DateTime(2021, 11, 16, 1, 15, 14, 394, DateTimeKind.Local).AddTicks(8854) });

            migrationBuilder.UpdateData(
                schema: "product",
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2021, 11, 16, 1, 15, 14, 398, DateTimeKind.Local).AddTicks(7890), new DateTime(2021, 11, 16, 1, 15, 14, 398, DateTimeKind.Local).AddTicks(7902) });

            migrationBuilder.UpdateData(
                schema: "product",
                table: "ProductQuantity",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2021, 11, 16, 1, 15, 14, 402, DateTimeKind.Local).AddTicks(8192), new DateTime(2021, 11, 16, 1, 15, 14, 402, DateTimeKind.Local).AddTicks(8199) });

            migrationBuilder.UpdateData(
                schema: "product",
                table: "ProductQuantity",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2021, 11, 16, 1, 15, 14, 402, DateTimeKind.Local).AddTicks(8265), new DateTime(2021, 11, 16, 1, 15, 14, 402, DateTimeKind.Local).AddTicks(8266) });

            migrationBuilder.UpdateData(
                schema: "product",
                table: "Unit",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2021, 11, 16, 1, 15, 14, 401, DateTimeKind.Local).AddTicks(5901), new DateTime(2021, 11, 16, 1, 15, 14, 401, DateTimeKind.Local).AddTicks(5909) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "CarrierDistributionZone",
                schema: "sales",
                newName: "CarrierDistributionZone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "CarrierDistributionZone",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertDate",
                table: "CarrierDistributionZone",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InsertDate", "PasswordHash", "PasswordSalt", "UpdateDate" },
                values: new object[] { new DateTime(2021, 11, 16, 1, 11, 14, 252, DateTimeKind.Local).AddTicks(1899), new byte[] { 34, 54, 46, 139, 71, 216, 238, 112, 123, 182, 109, 192, 67, 18, 118, 55, 228, 251, 158, 196, 195, 246, 185, 147, 46, 206, 162, 70, 232, 107, 73, 63, 22, 40, 183, 135, 78, 10, 77, 198, 179, 32, 91, 166, 247, 45, 151, 198, 225, 154, 211, 226, 234, 69, 79, 119, 187, 41, 154, 248, 141, 83, 213, 185 }, new byte[] { 222, 99, 58, 72, 242, 67, 177, 222, 30, 5, 210, 12, 212, 135, 89, 7, 88, 101, 225, 65, 96, 146, 125, 31, 67, 231, 70, 104, 4, 202, 71, 82, 2, 116, 134, 25, 254, 217, 210, 32, 145, 49, 127, 56, 127, 214, 69, 164, 8, 92, 183, 50, 75, 81, 210, 207, 10, 105, 161, 62, 44, 118, 162, 191, 1, 194, 240, 166, 13, 251, 189, 180, 16, 242, 161, 119, 210, 25, 177, 186, 106, 240, 28, 171, 181, 123, 236, 148, 224, 125, 7, 70, 180, 70, 145, 12, 139, 1, 102, 47, 180, 141, 138, 106, 141, 248, 168, 43, 226, 8, 54, 88, 174, 174, 68, 100, 223, 175, 99, 51, 16, 60, 31, 216, 7, 253, 237, 164 }, new DateTime(2021, 11, 16, 1, 11, 14, 252, DateTimeKind.Local).AddTicks(8315) });

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "InsertDate", "PasswordHash", "PasswordSalt", "UpdateDate" },
                values: new object[] { new DateTime(2021, 11, 16, 1, 11, 14, 252, DateTimeKind.Local).AddTicks(9490), new byte[] { 34, 54, 46, 139, 71, 216, 238, 112, 123, 182, 109, 192, 67, 18, 118, 55, 228, 251, 158, 196, 195, 246, 185, 147, 46, 206, 162, 70, 232, 107, 73, 63, 22, 40, 183, 135, 78, 10, 77, 198, 179, 32, 91, 166, 247, 45, 151, 198, 225, 154, 211, 226, 234, 69, 79, 119, 187, 41, 154, 248, 141, 83, 213, 185 }, new byte[] { 222, 99, 58, 72, 242, 67, 177, 222, 30, 5, 210, 12, 212, 135, 89, 7, 88, 101, 225, 65, 96, 146, 125, 31, 67, 231, 70, 104, 4, 202, 71, 82, 2, 116, 134, 25, 254, 217, 210, 32, 145, 49, 127, 56, 127, 214, 69, 164, 8, 92, 183, 50, 75, 81, 210, 207, 10, 105, 161, 62, 44, 118, 162, 191, 1, 194, 240, 166, 13, 251, 189, 180, 16, 242, 161, 119, 210, 25, 177, 186, 106, 240, 28, 171, 181, 123, 236, 148, 224, 125, 7, 70, 180, 70, 145, 12, 139, 1, 102, 47, 180, 141, 138, 106, 141, 248, 168, 43, 226, 8, 54, 88, 174, 174, 68, 100, 223, 175, 99, 51, 16, 60, 31, 216, 7, 253, 237, 164 }, new DateTime(2021, 11, 16, 1, 11, 14, 252, DateTimeKind.Local).AddTicks(9496) });

            migrationBuilder.UpdateData(
                schema: "product",
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2021, 11, 16, 1, 11, 14, 259, DateTimeKind.Local).AddTicks(6132), new DateTime(2021, 11, 16, 1, 11, 14, 259, DateTimeKind.Local).AddTicks(6161) });

            migrationBuilder.UpdateData(
                schema: "product",
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2021, 11, 16, 1, 11, 14, 263, DateTimeKind.Local).AddTicks(2503), new DateTime(2021, 11, 16, 1, 11, 14, 263, DateTimeKind.Local).AddTicks(2511) });

            migrationBuilder.UpdateData(
                schema: "product",
                table: "ProductQuantity",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2021, 11, 16, 1, 11, 14, 267, DateTimeKind.Local).AddTicks(3483), new DateTime(2021, 11, 16, 1, 11, 14, 267, DateTimeKind.Local).AddTicks(3491) });

            migrationBuilder.UpdateData(
                schema: "product",
                table: "ProductQuantity",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2021, 11, 16, 1, 11, 14, 267, DateTimeKind.Local).AddTicks(3556), new DateTime(2021, 11, 16, 1, 11, 14, 267, DateTimeKind.Local).AddTicks(3557) });

            migrationBuilder.UpdateData(
                schema: "product",
                table: "Unit",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2021, 11, 16, 1, 11, 14, 266, DateTimeKind.Local).AddTicks(1178), new DateTime(2021, 11, 16, 1, 11, 14, 266, DateTimeKind.Local).AddTicks(1187) });
        }
    }
}
