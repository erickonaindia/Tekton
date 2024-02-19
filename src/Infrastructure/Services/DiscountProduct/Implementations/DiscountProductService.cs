using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Tekton.Application.Common.Interfaces;
using Tekton.Application.Common.Models;
using Tekton.Infrastructure.Services.DiscountProduct.Model;

namespace Tekton.Infrastructure.Services.DiscountProduct.Implementations;
public class DiscountProductService : IDiscountProductService
{
    public async Task<decimal> GetProductDiscountAsync(int productId)
    {
        string apiUrl = "https://65cfd204bdb50d5e5f5bdd87.mockapi.io/api/v1";
        decimal result = 0;
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"{apiUrl}/product/{productId}");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var discountResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
                    result = (decimal)discountResponse!.PercentageDiscount;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request Error: {ex.Message}");
            }
            return result;
        }
    }
}
