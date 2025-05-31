using PCH.NFP.UI.Client.Models;
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
                return ApiResponse<long>.Failure(error);
            }
        }
    }
}
