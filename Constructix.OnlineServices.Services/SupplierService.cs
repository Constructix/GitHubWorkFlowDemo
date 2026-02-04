using Constructix.OnlineServices.Models;

public class SupplierService(List<Supplier> suppliers) : ISupplierService
{ 
    public async Task<List<Supplier>> GetAllSuppliersAsync() => await Task.FromResult(suppliers);
    
}