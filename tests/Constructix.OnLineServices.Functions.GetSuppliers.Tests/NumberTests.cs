
/* 

*/
using FluentAssertions;

namespace Constructix.OnLineServices.Functions.GetSuppliers.Tests;

public class NumbersTests
{
    [Fact]
    public void AddNumbersTest()
    {
        int sum = 1 + 1;

        sum.Should().Be(2);
    }
    // more tests to be added later. 
}