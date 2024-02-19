using MediatR;
using Tekton.API.Infrastructure;
using Tekton.Application.Products.Commands.CreateProduct;
using Tekton.Application.Products.Queries.GetById;

namespace API.Web.Endpoints;

public class Products : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetProductById, "{id}")
            .MapPost(CreateProduct)
            .MapPut(UpdateProduct, "{id}");
    }

    public async Task<IResult> GetProductById(
        ISender sender,
        [AsParameters] GetProductByIdQuery query)
    {
        var result = await sender.Send(query);

        if (result is null)
            return Results.NotFound();

        return Results.Ok(result);
    }

    public async Task<IResult> CreateProduct(
        ISender sender,
        CreateProductCommand command)
    {
        var result = await sender.Send(command);
        return Results.Created("", result);
    }

    public async Task<IResult> UpdateProduct(
        ISender sender,
        int id,
        UpdateProductCommand command)
    {
        command.Id = id;
        var result = await sender.Send(command);

        return Results.Ok(result);
    }
}
