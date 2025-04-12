using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PCH.NFP.API.Entities;

public class ApplicationUser: IdentityUser
{
    [MaxLength(10)]
    public string NationalCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}