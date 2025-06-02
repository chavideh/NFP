using FluentValidation;
using MediatR;
using PCH.NFP.API.Data;
using PCH.NFP.Shared.Models;

namespace PCH.NFP.API.Features;

public record UpdateProductCommand(long Id, string Code, string Title, string IranCode, string SepidarCode, int Quantity, string Description, bool Publish) : IRequest<ApiResponse<bool>>;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
    }
}

internal class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ApiResponse<bool>>
{
    private readonly AppDbContext _db;
    private readonly IValidator<UpdateProductCommand> _validator;

    public UpdateProductHandler(AppDbContext db, IValidator<UpdateProductCommand> validator)
    {
        _db = db;
        _validator = validator;
    }

    public async Task<ApiResponse<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return ApiResponse<bool>.FailureResponse(string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage)));

        var product = await _db.Products.FindAsync(request.Id);
        if (product == null)
            return ApiResponse<bool>.FailureResponse("محصول یافت نشد");

        product.Code = request.Code;
        product.Title = request.Title;
        product.IranCode = request.IranCode;
        product.SepidarCode = request.SepidarCode;
        product.Quantity = request.Quantity;
        product.Description = request.Description;
        product.Publish = request.Publish;

        await _db.SaveChangesAsync(cancellationToken);
        return ApiResponse<bool>.SuccessResponse(true, "محصول با موفقیت بروزرسانی شد");
    }
}