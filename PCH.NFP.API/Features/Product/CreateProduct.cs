using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCH.NFP.API.Data;
using PCH.NFP.API.Entities;
using PCH.NFP.Shared.Models;

namespace PCH.NFP.API.Features;

public record CreateProductCommand(string Code, string Title, string IranCode, string SepidarCode, int Quantity, string Description, bool Publish) : IRequest<ApiResponse<long>>;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
    }
}

internal class CreateProductHandler : IRequestHandler<CreateProductCommand, ApiResponse<long>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateProductCommand> _validator;

    public CreateProductHandler(AppDbContext db, IMapper mapper, IValidator<CreateProductCommand> validator)
    {
        _db = db;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ApiResponse<long>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

        var validationResult = _validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return ApiResponse<long>.FailureResponse(string.Join(Environment.NewLine,
                validationResult.Errors.Select(e => e.ErrorMessage)));
        }


        bool exists = await _db.Set<Product>().AnyAsync(x => x.Code == request.Code, cancellationToken);
        if (exists)
        {
            return ApiResponse<long>.FailureResponse("کد وارد شده تکراری می‌باشد");
        }

        var product = _mapper.Map<Product>(request);
        await _db.Products.AddAsync(product, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        return ApiResponse<long>.SuccessResponse(product.Id, "محصول با موفقیت ایجاد شد.");
    }
}