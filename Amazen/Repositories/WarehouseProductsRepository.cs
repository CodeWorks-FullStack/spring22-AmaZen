using System.Collections.Generic;
using System.Data;
using System.Linq;
using Amazen.Models;
using Dapper;

namespace Amazen.Repositories
{
  public class WarehouseProductsRepository
  {
    private readonly IDbConnection _db;

    public WarehouseProductsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal WarehouseProduct Create(WarehouseProduct newWarehouseProduct)
    {
      string sql = @"
        INSERT INTO warehouseproducts 
          (warehouseId, productId) 
        VALUES 
          (@WarehouseId, @ProductId); 
        SELECT LAST_INSERT_ID();";
      newWarehouseProduct.Id = _db.ExecuteScalar<int>(sql, newWarehouseProduct);
      return newWarehouseProduct;
    }

    internal WarehouseProduct Get(int id)
    {
      string sql = "SELECT * FROM warehouseproducts WHERE id = @id";
      return _db.QueryFirstOrDefault<WarehouseProduct>(sql, new { id });
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM warehouseproducts WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

    internal List<WarehouseProductProductViewModel> GetProducts(int warehouseId)
    {
      string sql = @"
      SELECT
       p.*,
       wp.id AS warehouseProductId
      FROM warehouseproducts wp
      JOIN products p ON wp.productId = p.id
      WHERE wp.warehouseId = @warehouseId
      ";
      return _db.Query<WarehouseProductProductViewModel>(sql, new { warehouseId }).ToList();
    }
  }
}
