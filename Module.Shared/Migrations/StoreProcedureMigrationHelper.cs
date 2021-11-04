using Microsoft.EntityFrameworkCore.Migrations;

namespace Module.Shared.Migrations
{
    public static class StoreProcedureMigrationHelper
    {
        public static void ProductProcedures(MigrationBuilder migrationBuilder)
        {
            var sp1 = @"CREATE PROC product.GetStoreProductsByCarrier
                        @CarrierId INT,
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
                        INNER JOIN sales.Carrier c ON c.Id = ps.CarrierId 
                        INNER JOIN product.ProductStore ps2 ON ps2.Id = ps.ProductStoreId 
                        INNER JOIN product.ProductQuantity pq ON pq.Id = ps2.ProductQuantityId 
                        INNER JOIN product.Product p on p.Id = pq.ProductId 
                        INNER JOIN product.Unit u ON u.Id = pq.UnitId
                        WHERE 
						c.Id = @CarrierId AND
						ps2.StoreId= @StoreId AND
						ps.Status = 1 AND
						pq.Status=1";
            migrationBuilder.Sql(sp1);

            var sp2 = @"CREATE PROC product.GetStoreProductsByStore
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
            migrationBuilder.Sql(sp2);

            var sp3 = @"CREATE proc [product].[GetStoreProducts]
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
						pstck.Status
                        FROM product.ProductQuantity Pq
                        LEFT JOIN product.ProductStore pstr on pstr.ProductQuantityId=pq.Id
                        LEFT JOIN product.Product p on p.Id=pq.ProductId
                        LEFT JOIN product.Unit pu on pu.Id=pq.UnitId
                        LEFT JOIN product.ProductStock pstck on pstck.ProductStoreId=pstr.Id
                        LEFT JOIN sales.carrier c on c.Id = pstck.CarrierId
                        LEFT JOIN product.ProductPrice pp on pp.ProductStoreId=pstr.Id
                        WHERE 
						StoreId=@storeId AND 
						pstr.Status = 1 AND 
						pstck.Status = 1 OR
						pstck.Status = 2";
            migrationBuilder.Sql(sp3);
        }
    }
}
