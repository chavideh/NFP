using Carter;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PCH.NFP.API.Models;
using PCH.NFP.API.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PCH.NFP.API.Features;

public record LoginUserCommand(string Username, string Password) : IRequest<ApiResponse<string>>;

public class LoginUserValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}

internal class LoginUserHandler : IRequestHandler<LoginUserCommand, ApiResponse<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public LoginUserHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<ApiResponse<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return new ApiResponse<string>("Invalid username or password");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new ApiResponse<string>(data: tokenHandler.WriteToken(token));  // Success = true
    }
}

public class LoginUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/login", async (LoginUserCommand request, ISender sender) =>
        {
            var token = await sender.Send(request);
            return Results.Ok(new { Token = token });
        });
    }
}