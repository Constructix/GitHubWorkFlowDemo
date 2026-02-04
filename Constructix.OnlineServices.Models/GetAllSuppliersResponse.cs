namespace Constructix.OnlineServices.Models;

public record GetAllSuppliersResponse(int Total, int TotalPages, int PageNum, List<Supplier> Suppliers);