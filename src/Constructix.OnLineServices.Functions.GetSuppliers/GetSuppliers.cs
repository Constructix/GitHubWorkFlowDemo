using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Constructix.OnLineServices.Functions.GetSuppliers
{
    public class GetSuppliers(ISupplierService supplierService, ILogger<GetSuppliers> _logger)
    {
        
        //public GetSuppliers(ILoggerFactory loggerFactory)
        //{
        //    _logger = loggerFactory.CreateLogger<GetSuppliers>();
        //}

        [Function("GetSuppliers")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            //response.Headers.Add("Content-Type", "application/json");

            //response.WriteString("Welcome to Azure Functions!");

            var allSuppliers = await supplierService.GetAllSuppliersAsync(); 
            var getAllSuppliersResponse = new GetAllSuppliersResponse(allSuppliers.Count,  1,  1,  allSuppliers);
            response.WriteAsJsonAsync(getAllSuppliersResponse);

            return response;
        }
        
       
    }
}

public interface ISupplierService
{
    Task<List<Supplier>> GetAllSuppliersAsync();
}

public class SupplierService(List<Supplier> suppliers) : ISupplierService
{


    public async Task<List<Supplier>> GetAllSuppliersAsync()
    {
        return await Task.FromResult(suppliers);
    }
}


public record GetAllSuppliersResponse(int Total, int TotalPages, int PageNum, List<Supplier> Suppliers);


public record Supplier(string Name, Address Address, string Phone, string WebAddress, string Notes);
public record Address (string Street, string Suburb, string Postcode);

public record Person(string FirstName, string LastName);
public record Contact(string FirstName, string LastName, string Email, string Phone);


