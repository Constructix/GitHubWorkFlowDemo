using Constructix.OnlineServices.Models;

public interface ISupplierService
{
    Task<List<Supplier>> GetAllSuppliersAsync();
}