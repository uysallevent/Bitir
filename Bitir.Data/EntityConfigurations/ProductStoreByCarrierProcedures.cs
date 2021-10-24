using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Bitir.Data.EntityConfigurations
{
    public class ProductStoreByCarrierProcedures : Migration
    {
        protected override void Up([NotNullAttribute] MigrationBuilder migrationBuilder)
        {
            var sp = @"ALTER PROC product.GetStoreProductsByCarrier
                        @CarrierId INT
                        AS
                        SELECT 
                        ps2.Id AS ProductStoreId,
                        p.Name ProductName,
                        pq.Quantity,
                        u.Name UnitName,
                        u.Abbreviation,
                        ps.Quantity AS ProductStock,
                        c.Plate,
                        c.Capacity
                        FROM product.ProductStock ps 
                        INNER JOIN sales.Carrier c ON c.Id = ps.CarrierId 
                        INNER JOIN product.ProductStore ps2 ON ps2.Id = ps.ProductStoreId 
                        INNER JOIN product.ProductQuantity pq ON pq.Id = ps2.ProductQuantityId 
                        INNER JOIN product.Product p on p.Id = pq.ProductId 
                        INNER JOIN product.Unit u ON u.Id = pq.UnitId
                        WHERE c.Id = @CarrierId AND ps.Status = 1";
            migrationBuilder.Sql(sp);
        }
    }
}
