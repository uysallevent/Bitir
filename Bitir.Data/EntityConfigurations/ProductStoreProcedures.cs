using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Bitir.Data.EntityConfigurations
{
    public class ProductStoreProcedures : Migration
    {
        protected override void Up([NotNullAttribute] MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE proc product.GetStoreProducts
                        @storeId int
                        as
                        SELECT 
                        pq.Id,
                        pstr.Id as ProductStoreId,
						pstck.Id as ProductStockId,
						pp.Id as ProductPriceId,
                        p.Name,
                        pq.Quantity,
                        pu.Name as Unit,
                        pu.Abbreviation,
                        pp.Price as Price,
                        pstck.Quantity as Stock,
                        ISNULL(c.Plate,'DEPO') AS Position,
						pstr.Status
                        FROM product.ProductQuantity Pq
                        LEFT JOIN product.ProductStore pstr on pstr.ProductQuantityId=pq.Id
                        LEFT JOIN product.Product p on p.Id=pq.ProductId
                        LEFT JOIN product.Unit pu on pu.Id=pq.UnitId
                        LEFT JOIN product.ProductStock pstck on pstck.ProductStoreId=pstr.Id
                        LEFT JOIN sales.carrier c on c.Id = pstck.CarrierId
                        LEFT JOIN product.ProductPrice pp on pp.ProductStoreId=pstr.Id
                        WHERE StoreId=@storeId AND pstr.Status = 1 AND pstck.Status = 1";
            migrationBuilder.Sql(sp);
        }
    }
}
