using PCH.NFP.Shared.Models;
using System.Net.Http.Json;

namespace PCH.NFP.UI.Client
{
    public class ProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ApiResponse<long>> CreateProductAsync(CreateProductRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/products", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<long>>();
                return result!;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return ApiResponse<long>.FailureResponse(error);
            }
        }

        public async Task<ApiResponse<PagedResult<ProductDto>>> GetProductsAsync(int page, int pageSize, string? code = null, string? title = null)
        {
            // ساخت کوئری پارامترها
            var queryParams = new Dictionary<string, string?>
            {
                ["page"] = page.ToString(),
                ["pageSize"] = pageSize.ToString(),
                ["code"] = code,
                ["title"] = title
            };

            var queryString = string.Join("&", queryParams
                .Where(kvp => !string.IsNullOrWhiteSpace(kvp.Value))
                .Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value!)}"));

            var response = await _http.GetFromJsonAsync<ApiResponse<PagedResult<ProductDto>>>($"api/products?{queryString}");

            return response ?? ApiResponse<PagedResult<ProductDto>>.FailureResponse("No response from server.");
        }

        public async Task<ApiResponse<bool>> UpdateProductAsync(ProductDto product)
        {
            var command = new
            {
                product.Id,
                product.Code,
                product.Title,
                product.IranCode,
                product.SepidarCode,
                product.Quantity,
                product.Description,
                product.Publish
            };

            var response = await _http.PutAsJsonAsync("api/products", command);

            if (!response.IsSuccessStatusCode)
                return ApiResponse<bool>.FailureResponse("خطا در ارسال درخواست به سرور");

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
            return result ?? ApiResponse<bool>.FailureResponse("پاسخی از سرور دریافت نشد");
        }
        public async Task<ApiResponse<bool>> DeleteProductAsync(long productId)
        {
            var response = await _http.DeleteAsync($"api/products/{productId}");
            if (response.IsSuccessStatusCode)
            {
                return ApiResponse<bool>.SuccessResponse(true, "محصول با موفقیت حذف شد");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return ApiResponse<bool>.FailureResponse(error);
            }
        }

    }
}
