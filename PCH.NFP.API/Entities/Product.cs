using System.ComponentModel.DataAnnotations;

namespace PCH.NFP.API.Entities;

public class Product
{
 
    public long Id { get; set; }
    [Required]
    public string Code { get; set; }

    [Required]
    public string Title { get; set; }
    public string IranCode { get; set; }
    public string SepidarCode { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public bool Publish { get; set; }
}