using Carter;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PCH.NFP.Shared;
using PCH.NFP.API.Entities;
using PCH.NFP.Shared.Models;

namespace PCH.NFP.API.Features;

public record RegisterUserCommand(string Username, string FirstName, string LastName, string NationalCode, string Password, string Email) : IRequest<ApiResponse<string>>;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("نام کاربری را وارد کنید");
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.NationalCode).NotEmpty().WithMessage("کد ملی وارد نشده است").MaximumLength(10).WithMessage("حداکثر 10 رقم وارد کنید");
        RuleFor(x => x.Password).MinimumLength(6);
        RuleFor(x => x.Email).EmailAddress();
    }
}

internal class RegisterUserHandler : IRequestHandler<RegisterUserCommand, ApiResponse<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IValidator<RegisterUserCommand> _validator;

    public RegisterUserHandler(UserManager<ApplicationUser> userManager, IValidator<RegisterUserCommand> validator)
    {
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<ApiResponse<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return  ApiResponse<string>.FailureResponse(string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        var user = new ApplicationUser { UserName = request.Username, FirstName = request.FirstName, LastName = request.LastName, NationalCode = request.NationalCode, Email = request.Email };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {

            return  ApiResponse<string>.FailureResponse( string.Join(", ", result.Errors.Select(e => e.Description)));
        }
        return new ApiResponse<string>(data: user.Id);
    }
}


public class RegisterUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/register", async (RegisterUserCommand request, ISender sender) =>
        {
            var userId = await sender.Send(request);
            return Results.Ok(new { UserId = userId });
        });
    }
}