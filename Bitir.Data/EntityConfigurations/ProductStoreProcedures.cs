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
                        pstr.StoreId,
                        p.Name,
                        pq.Quantity,
                        pu.Name as Unit,
                        pu.Abbreviation,
                        pp.Price as Price,
                        pstck.Quantity as Stock
                        FROM product.ProductQuantity Pq
                        INNER JOIN product.ProductStore pstr on pstr.ProductQuantityId=pq.Id
                        INNER JOIN product.Product p on p.Id=pq.ProductId
                        INNER JOIN product.Unit pu on pu.Id=pq.UnitId
                        INNER JOIN product.ProductStock pstck on pstck.ProductStoreId=pstr.Id
                        INNER JOIN product.ProductPrice pp on pp.ProductStoreId=pstr.Id
                        WHERE StoreId=@storeId";
            migrationBuilder.Sql(sp);
        }
    }
}
