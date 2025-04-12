using AutoMapper;
using MediatR;
using PCH.NFP.API.Data;
using PCH.NFP.API.Models;

namespace PCH.NFP.API.Features;

public record GetProductByIdQuery(long Id) : IRequest<ApiResponse<ProductDto>>;

internal class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ApiResponse<ProductDto>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetProductByIdHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _db.Products.FindAsync(request.Id);
        if (product == null)
            return ApiResponse<ProductDto>.FailureResponse("محصول یافت نشد");

        return ApiResponse<ProductDto>.SuccessResponse(_mapper.Map<ProductDto>(product));
    }
}