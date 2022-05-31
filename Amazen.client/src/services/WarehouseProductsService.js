import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"

class WarehouseProductsService {

  async getProductsByWarehouseId(id) {
    const res = await api.get(`api/warehouses/${id}/products`)
    AppState.warehouseProducts = res.data
  }

  async add(wp) {
    await api.post('api/warehouseProducts', wp)
    this.getProductsByWarehouseId(wp.warehouseId)
  }

  async remove(id) {
    await api.delete('api/warehouseProducts/' + id)
    AppState.warehouseProducts = AppState.warehouseProducts.filter(wp => wp.warehouseProductId != id)
  }
}

export const warehouseProductsService = new WarehouseProductsService()