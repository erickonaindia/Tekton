using Tekton.Application.Common.Exceptions;
using Tekton.Application.Products.Commands.CreateProduct;
using Tekton.Domain.Entities;

namespace Tekton.Application.FunctionalTests.Products.Commands;

using static Tekton.Application.FunctionalTests.Testing;
public class CreateProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateProductCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateProduct()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateProductCommand
        {
            Description = "TestDescription",
            Name = "TestName",
            Price = 1,
            StatusName = "Active",
            Stock = 1
        };

        var product = await SendAsync(command);

        var productFound = await FindAsync<Product>(product.Id);

        productFound.Should().NotBeNull();
        productFound!.Name.Should().Be(command.Name);
        productFound.CreatedBy.Should().Be(userId);
    }
}
