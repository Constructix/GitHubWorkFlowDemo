using System.Runtime.CompilerServices;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults((hostContext, services) =>
    {
        
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddScoped<List<Supplier>>(x => BuildSupplierList());
        services.AddScoped<ISupplierService, SupplierService>();
    })
    .Build();


List<Supplier> BuildSupplierList()
{
    return
    [
        new Supplier("Builders Discount Warehouse", new Address("3437 Pacific Hwy", "Slacks Creek", "4127"),
            "(07)32082240",
            "https://buildersdiscountwarehouse.com.au/", string.Empty),

        new Supplier("Brisbane Building Products", new Address("Unit 2/2083 Sandgate Rd", "Virginia ", "4014"),
            "(07)30735325",
            "https://brisbanebuildingproducts.com.au/",
            "Hours: \nSunday\tClosed\nMonday\t5:30\u202fam–3\u202fpm\nTuesday\t5:30\u202fam–3\u202fpm\nWednesday\t5:30\u202fam–3\u202fpm\nThursday\t5:30\u202fam–3\u202fpm\nFriday\t5:30–10:30\u202fam\nSaturday\tClosed"),
        new Supplier("Lobb St Saw mill", new Address("Unit 2/2083 Sandgate Rd", "Virginia ", "4014"),
            "(07)30735325",
            "https://brisbanebuildingproducts.com.au/",
            "Hours: \nSunday\tClosed\nMonday\t5:30\u202fam–3\u202fpm\nTuesday\t5:30\u202fam–3\u202fpm\nWednesday\t5:30\u202fam–3\u202fpm\nThursday\t5:30\u202fam–3\u202fpm\nFriday\t5:30–10:30\u202fam\nSaturday\tClosed")

    ];
}






host.Run();


