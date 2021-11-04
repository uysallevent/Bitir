using Microsoft.EntityFrameworkCore.Migrations;

namespace Module.Shared.Migrations
{
    public static class ViewMigrationHelper
    {
        public static void ViewMigrations(MigrationBuilder migrationBuilder)
        {
            var view1 = @"CREATE VIEW [sales].[StoreOrdersView]
						AS	
            SELECT
                        OrderId=o.Id,
						StoreId=ps.StoreId,
                        UserId=uac.Id,
                        UserAddressId=uad.Id,
                        DistrictId=dis.Id,
                        ProvinceId=pro.Id,
                        ProductStoreId=ps.Id,
                        ProductQuantityId=pq.Id,
                        CustomerName=CONCAT(uac.Name,' ',uac.Surname),
                        OrderProvinceName=pro.Name,
                        OrderDistrictName=dis.Name,
                        OrderAddress=uad.Address,
                        OrderQuantity=od.Quantity,
                        OrderStatus=o.OrderStatus,
						OrderDetailStatus=od.Status,
                        OrderNote=o.Note,
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
        }
    }
}
