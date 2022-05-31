using System;
using System.Collections.Generic;
using Amazen.Models;
using Amazen.Repositories;

namespace Amazen.Services
{
  public class WarehouseProductsService
  {
    private readonly WarehouseProductsRepository _repo;

    public WarehouseProductsService(WarehouseProductsRepository repo)
    {
      _repo = repo;
    }

    internal WarehouseProduct Create(WarehouseProduct newWarehouseProduct)
    {
      return _repo.Create(newWarehouseProduct);
    }

    internal WarehouseProduct Get(int id)
    {
      WarehouseProduct found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal void Delete(int id)
    {
      Get(id);
      _repo.Delete(id);
    }

    internal List<WarehouseProductProductViewModel> GetProducts(int warehouseId)
    {
      List<WarehouseProductProductViewModel> products = _repo.GetProducts(warehouseId);
      return products;
    }
  }
}