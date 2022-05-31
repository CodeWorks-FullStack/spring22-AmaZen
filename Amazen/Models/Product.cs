namespace Amazen.Models
{
  public class Product
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
  }

  public class WarehouseProductProductViewModel : Product
  {
    //  Id of the relationship
    public int WarehouseProductId { get; set; }
  }
}