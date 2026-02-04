namespace Constructix.OnlineServices.Models
{
    public record Supplier(string Name, Address Address, string Phone, string WebAddress, string Notes);
    public record Address(string Street, string Suburb, string Postcode);

    public record Person(string FirstName, string LastName);
    public record Contact(string FirstName, string LastName, string Email, string Phone);
}
