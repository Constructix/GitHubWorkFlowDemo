using System.Net;
using Constructix.OnlineServices.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Constructix.OnLineServices.Functions.GetSuppliers
{
    public class GetSuppliers(ISupplierService supplierService, ILogger<GetSuppliers> _logger)
    {
        [Function("GetSuppliers")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("GetSuppliers HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            var allSuppliers = await supplierService.GetAllSuppliersAsync(); 
            var getAllSuppliersResponse = new GetAllSuppliersResponse(allSuppliers.Count,  1,  1,  allSuppliers);
            await response.WriteAsJsonAsync(getAllSuppliersResponse);

            return response;
        }
    }
}