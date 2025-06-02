namespace PCH.NFP.Shared.Models;

public class ApplicationUserDisplayDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Enable { get; set; }
    public string NationalCode { get; set; }
    public string PhoneNumber { get; set; }
}