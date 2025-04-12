using MediatR;
using PCH.NFP.API.Data;
using PCH.NFP.API.Models;

namespace PCH.NFP.API.Features;

public record DeleteProductCommand(long Id) : IRequest<ApiResponse<bool>>;

internal class DeleteProductHandler : IRequestHandler<DeleteProductCommand, ApiResponse<bool>>
{
    private readonly AppDbContext _db;

    public DeleteProductHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ApiResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _db.Products.FindAsync(request.Id);
        if (product == null)
            return ApiResponse<bool>.FailureResponse("محصول یافت نشد");

        _db.Products.Remove(product);
        await _db.SaveChangesAsync(cancellationToken);
        return ApiResponse<bool>.SuccessResponse(true, "محصول با موفقیت حذف شد");
    }
}