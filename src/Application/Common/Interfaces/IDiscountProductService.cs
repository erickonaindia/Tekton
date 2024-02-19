namespace Tekton.Application.Common.Interfaces;
public interface IDiscountProductService
{
    Task<decimal> GetProductDiscountAsync(int productId);
}
