using System;
using System.Collections.Generic;
using Amazen.Models;
using Amazen.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Amazen.Controllers
{
  [Route("api/[controller]")]
  public class WarehousesController : Controller
  {
    private readonly WarehousesService _ws;
    private readonly WarehouseProductsService _wps;

    public WarehousesController(WarehousesService ws, WarehouseProductsService wps)
    {
      _ws = ws;
      _wps = wps;
    }

    [HttpGet]
    public ActionResult<List<Warehouse>> Get()
    {
      try
      {
        List<Warehouse> warehouses = _ws.Get();
        return Ok(warehouses);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Warehouse> Get(int id)
    {
      try
      {
        Warehouse warehouse = _ws.Get(id);
        return Ok(warehouse);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}/products")]
    public ActionResult<List<WarehouseProductProductViewModel>> GetProducts(int id)
    {
      try
      {
        List<WarehouseProductProductViewModel> products = _wps.GetProducts(id);
        return Ok(products);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }




    [HttpPost]
    [Authorize]
    public ActionResult<Warehouse> Create([FromBody] Warehouse warehouse)
    {
      try
      {
        Warehouse newWarehouse = _ws.Create(warehouse);
        return Created($"/api/warehouses/{newWarehouse.Id}", newWarehouse);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public ActionResult<Warehouse> Edit([FromBody] Warehouse warehouse, int id)
    {
      try
      {
        warehouse.Id = id;
        Warehouse updated = _ws.Edit(warehouse);
        return Ok(updated);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<Warehouse> Delete(int id)
    {
      try
      {
        _ws.Delete(id);
        return Ok();
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

  }
}