using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCH.NFP.API.Data;
using PCH.NFP.API.Models;
using PCH.NFP.API.Helpers;

namespace PCH.NFP.API.Features;

public record GetProductListQuery(int Page, int PageSize, string? Code, string? Title) : IRequest<ApiResponse<PagedResult<ProductDto>>>;

public class GetProductListValidator : AbstractValidator<GetProductListQuery>
{
    public GetProductListValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
    }
}

internal class GetProductListHandler : IRequestHandler<GetProductListQuery, ApiResponse<PagedResult<ProductDto>>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    private readonly IValidator<GetProductListQuery> _validator;

    public GetProductListHandler(AppDbContext db, IMapper mapper, IValidator<GetProductListQuery> validator)
    {
        _db = db;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ApiResponse<PagedResult<ProductDto>>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return ApiResponse<PagedResult<ProductDto>>.FailureResponse(string.Join(Environment.NewLine,
                validationResult.Errors.Select(e => e.ErrorMessage)));
        }


        var query = _db.Products.AsNoTracking();

        if (!string.IsNullOrEmpty(request.Code))
            query = query.Where(x => x.Code == request.Code);

        if (!string.IsNullOrEmpty(request.Title))
            query = query.Where(x => x.Title.Contains(request.Title));

        var result = await query.ToPagedResultAsync<Entities.Product, ProductDto>(_mapper, request.Page, request.PageSize);
        return ApiResponse<PagedResult<ProductDto>>.SuccessResponse(result);
    }
}
