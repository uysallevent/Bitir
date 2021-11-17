using Microsoft.EntityFrameworkCore.Migrations;

namespace Module.Shared.Migrations
{
    public static class ViewMigrationHelper
    {
        public static void ViewMigrations(MigrationBuilder migrationBuilder)
        {
            var view1 = @"CREATE VIEW [sales].[StoreOrdersView]
						AS	SELECT
                        OrderId=o.Id,
						StoreId=ps.StoreId,
                        UserId=uac.Id,
                        UserAddressId=uad.Id,
                        DistrictId=dis.Id,
                        ProvinceId=pro.Id,
                        ProductStoreId=ps.Id,
                        ProductQuantityId=pq.Id,
						CarrierId=o.CarrierId,
                        CustomerName=CONCAT(uac.Name,' ',uac.Surname),
                        OrderProvinceName=pro.Name,
                        OrderDistrictName=dis.Name,
                        OrderAddress=uad.Address,
                        OrderQuantity=od.Quantity,
                        OrderStatus=o.OrderStatus,
						OrderDetailStatus=od.Status,
                        OrderNote=o.Note,
						OrderDate=o.Date,
                        ProductName=p.Name,
                        ProductQuantity=pq.Quantity,
                        ProductUnit=un.Name,
                        ProductUnitAbbreviation=un.Abbreviation
                        FROM sales.[Order] o
						LEFT JOIN sales.OrderDetail od on od.OrderId=o.Id
                        LEFT JOIN auth.UserAccount uac on uac.Id=o.UserId
                        LEFT JOIN auth.UserAddress uad on uad.Id=o.UserAddressId
                        LEFT JOIN auth.District dis on dis.Id=uad.DistrictId
                        LEFT JOIN auth.Province pro on pro.Id=dis.ProvinceId
                        LEFT JOIN product.ProductStore ps on ps.Id=od.ProductStoreId
                        LEFT JOIN product.ProductQuantity pq on pq.Id=ps.ProductQuantityId
                        LEFT JOIN product.Product p on p.Id=pq.ProductId
                        LEFT JOIN product.Unit un on un.Id=pq.UnitId";
            migrationBuilder.Sql(view1);

            var view2 = @"CREATE VIEW [sales].[StoreCarriersView]
                        AS
                        SELECT 
                        CarrierId=C.Id,
                        StoreId=cs.StoreId,
                        ProvinceId=p.Id,
                        DistrictId=d.Id,
                        NeighbourhoodId=n.Id,
                        Plate=c.Plate,
                        Capacity=c.Capacity,
                        ProvinceName=p.Name,
                        DistrictName=d.Name,
                        NeighbourhoodName=n.Name,
                        CarrierStatus=c.Status
                        FROM sales.Carrier c
                        LEFT JOIN sales.Carrier_Store cs ON cs.CarrierId=c.Id
                        LEFT JOIN sales.CarrierDistributionZone cz ON cz.CarrierId=c.Id
                        LEFT JOIN auth.Province p ON p.Id=cz.ProvinceId
                        LEFT JOIN auth.District d ON d.Id=cz.DistrictId
                        LEFT JOIN auth.Neighbourhood n ON n.Id=cz.NeighbourhoodId";
            migrationBuilder.Sql(view2);

        }
    }
}
