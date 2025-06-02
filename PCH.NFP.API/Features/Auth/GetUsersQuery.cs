using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PCH.NFP.Shared.Models;
using PCH.NFP.API.Entities;


namespace PCH.NFP.API.Features;

public record GetUsersQuery(int Page, int PageSize) : IRequest<ApiResponse<PagedResult<ApplicationUserDisplayDto>>>;


internal class GetUsersHandler : IRequestHandler<GetUsersQuery, ApiResponse<PagedResult<ApplicationUserDisplayDto>>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public GetUsersHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ApiResponse<PagedResult<ApplicationUserDisplayDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var usersQuery = _userManager.Users.Select(user => _mapper.Map<ApplicationUserDisplayDto>(user));
        var pagedResult = usersQuery.ToPagedResult(request.Page, request.PageSize);

        return new ApiResponse<PagedResult<ApplicationUserDisplayDto>>(data: pagedResult);
    }


}

public class GetUsersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users", async (int page, int pageSize, ISender sender) =>
        {
            var result = await sender.Send(new GetUsersQuery(page, pageSize));
            return Results.Ok(result);
        });
    }
}
