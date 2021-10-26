using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Bitir.Data.EntityConfigurations
{
    public class ProductStoreByStoreProcedures : Migration
    {
        protected override void Up([NotNullAttribute] MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROC product.GetStoreProductsByStore
                        @StoreId INT
                        AS
                        SELECT 
						ps.Id AS ProductStockId,
                        ps2.Id AS ProductStoreId,
                        p.Name ProductName,
                        pq.Quantity,
                        u.Name UnitName,
                        u.Abbreviation,
                        ps.Quantity AS ProductStock,
                        c.Plate,
                        c.Capacity
                        FROM product.ProductStock ps 
                        LEFT JOIN sales.Carrier c ON c.Id = ps.CarrierId 
                        LEFT JOIN product.ProductStore ps2 ON ps2.Id = ps.ProductStoreId 
                        LEFT JOIN product.ProductQuantity pq ON pq.Id = ps2.ProductQuantityId 
                        LEFT JOIN product.Product p on p.Id = pq.ProductId 
                        LEFT JOIN product.Unit u ON u.Id = pq.UnitId
                        WHERE
						ps.CarrierId IS NULL AND 
						ps2.StoreId=@StoreId AND
						ps.Status = 1 AND 
						pq.Status=1";
            migrationBuilder.Sql(sp);
        }
    }
}
