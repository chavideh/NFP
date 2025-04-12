using Carter;
using MediatR;

namespace PCH.NFP.API.Features;

public class ProductEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/products", async (CreateProductCommand request, ISender sender) => Results.Ok(await sender.Send(request)));
        app.MapGet("/api/products/{id}", async (long id, ISender sender) => Results.Ok(await sender.Send(new GetProductByIdQuery(id))));
        app.MapGet("/api/products", async (int page, int pageSize, string? code, string? title, ISender sender) => Results.Ok(await sender.Send(new GetProductListQuery(page, pageSize, code, title))));
        app.MapPut("/api/products", async (UpdateProductCommand request, ISender sender) => Results.Ok(await sender.Send(request)));
        app.MapDelete("/api/products/{id}", async (long id, ISender sender) => Results.Ok(await sender.Send(new DeleteProductCommand(id))));
    }
}