using AutoMapper;
 
using PCH.NFP.API.Entities;
using PCH.NFP.Shared.Models;

namespace PCH.NFP.API.Features;

public class AuthProfile: Profile
{
    public AuthProfile()
    {
        CreateMap<ApplicationUser, ApplicationUserDisplayDto>().ReverseMap();
    }
}