using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Application.Common.Exceptions;
using Tekton.Application.Products.Commands.CreateProduct;
using Tekton.Domain.Entities;

namespace Tekton.Application.FunctionalTests.Products.Commands;

using static Tekton.Application.FunctionalTests.Testing;
public class UpdateProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new UpdateProductCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireValidProduct()
    {
        var command = new UpdateProductCommand
        {
            Id = 1,
            Description = "TestDescription2",
            Name = "TestName2",
            Price = 12,
            StatusName = "Active",
            Stock = 1
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateProduct()
    {
        var userId = await RunAsDefaultUserAsync();

        var newProduct = await SendAsync(new CreateProductCommand
        {
            Description = "TestDescription",
            Name = "TestName",
            Price = 1,
            StatusName = "Active",
            Stock = 1
        });


        var command = new UpdateProductCommand
        {
            Id = 1,
            Description = "TestDescription2",
            Name = "TestName2",
            Price = 12,
            StatusName = "Active",
            Stock = 1
        };

        var product = await SendAsync(command);

        var productFound = await FindAsync<Product>(product.Id);

        productFound.Should().NotBeNull();
        productFound!.Name.Should().Be(command.Name);
        productFound!.LastModifiedBy.Should().NotBeNull();
        productFound.LastModifiedBy.Should().Be(userId);
    }
}
