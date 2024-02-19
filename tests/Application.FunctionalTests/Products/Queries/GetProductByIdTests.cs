using Tekton.Application.Products.Queries.GetById;
using Tekton.Domain.Entities;
using static Tekton.Application.FunctionalTests.Testing;

namespace Tekton.Application.FunctionalTests.Products.Queries;
public class GetProductByIdTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnProductById()
    {
        await RunAsDefaultUserAsync();

        await AddAsync(new Product
        {
            Description = "Description",
            Discount = 1,
            FinalPrice = 1,
            Name = "Name",
            Price = 1,
            Status = 1,
            Stock = 1,
        });

        var query = new GetProductByIdQuery() { Id = 1};

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Name.Should().Be("Name");
    }
}
