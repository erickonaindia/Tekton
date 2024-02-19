namespace Tekton.Infrastructure.Services.DiscountProduct.Model;
public class ApiResponse
{
    public DateTime CreatedAt { get; set; }
    public int PercentageDiscount { get; set; }
    public string Id { get; set; } = string.Empty;
}
