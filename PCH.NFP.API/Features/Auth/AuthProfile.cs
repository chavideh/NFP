using AutoMapper;
using PCH.NFP.API.Models;
using PCH.NFP.API.Entities;

namespace PCH.NFP.API.Features;

public class AuthProfile: Profile
{
    public AuthProfile()
    {
        CreateMap<ApplicationUser, ApplicationUserDisplayDto>().ReverseMap();
    }
}