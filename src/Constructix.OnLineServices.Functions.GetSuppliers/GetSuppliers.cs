using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Constructix.OnLineServices.Functions.GetSuppliers
{
    public class GetSuppliers
    {
        private readonly ILogger _logger;

        public GetSuppliers(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetSuppliers>();
        }

        [Function("GetSuppliers")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            //response.Headers.Add("Content-Type", "application/json");

            //response.WriteString("Welcome to Azure Functions!");

            var allSuppliers = GetAllSuppliers();
            var getAllSuppliersResponse = new GetAllSuppliersResponse(allSuppliers.Count,  1,  1,  allSuppliers);
            response.WriteAsJsonAsync(getAllSuppliersResponse);

            return response;
        }
        
        private List<Supplier> GetAllSuppliers()
        {
            return new List<Supplier>
            {
                new Supplier("Builders Discount Warehouse", new Address("3437 Pacific Hwy", "Slacks Creek", "4127"), "(07)32082240",
                    "https://buildersdiscountwarehouse.com.au/", string.Empty),
                new Supplier("Brisbane Building Products", new Address("Unit 2/2083 Sandgate Rd", "Virginia ", "4014"), "(07)30735325",
                    "https://brisbanebuildingproducts.com.au/", "Hours: \nSunday\tClosed\nMonday\t5:30\u202fam–3\u202fpm\nTuesday\t5:30\u202fam–3\u202fpm\nWednesday\t5:30\u202fam–3\u202fpm\nThursday\t5:30\u202fam–3\u202fpm\nFriday\t5:30–10:30\u202fam\nSaturday\tClosed")
                
            };
        }
    }
}

public record GetAllSuppliersResponse(int Total, int TotalPages, int PageNum, List<Supplier> Suppliers);


public record Supplier(string Name, Address Address, string Phone, string WebAddress, string Notes);
public record Address (string Street, string Suburb, string Postcode);

public record Person(string FirstName, string LastName);
public record Contact(string FirstName, string LastName, string Email, string Phone);


