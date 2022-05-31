<template>
  <div class="product row my-2 p-2 border">
    <div class="col-3">{{ product.name }}</div>
    <div class="col-7">{{ product.description }}</div>
    <div class="col-2 text-end " v-if="product.warehouseProductId">
      <i class="mdi mdi-dump-truck mdi-24px action" title="Remove from Warehouse" @click="remove"></i>
    </div>
    <div class="col-2 text-end" v-else>
      <select class="form-control" name="location" id="location" v-model="warehouseId">
        <option v-for="w in warehouses" :key="w.id" :value="w.id">{{ w.name }}</option>
      </select>
      <button class="btn btn-success" @click="add">Add</button>
    </div>
  </div>
</template>


<script>
import { computed, ref } from 'vue'
import { AppState } from '../AppState.js'
import { warehouseProductsService } from '../services/WarehouseProductsService.js'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'

export default {
  props: {
    product: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const warehouseId = ref(0)
    return {
      warehouseId,
      warehouses: computed(() => AppState.warehouses),
      async add() {
        try {
          const newWp = { productId: props.product.id, warehouseId: warehouseId.value }
          await warehouseProductsService.add(newWp)
          warehouseId.value = 0
          Pop.toast("added")
        } catch (error) {
          logger.error(error)
          Pop.toast("Something Failed", "error")
        }
      },
      async remove() {
        try {
          if (await Pop.confirm()) {
            await warehouseProductsService.remove(props.product.warehouseProductId)
            Pop.toast("Gone Baby, Gone!")
          }
        } catch (error) {
          logger.error(error)
          Pop.toast("Something Failed", "error")
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>