namespace PCH.NFP.UI.Client.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ApiResponse<T> Failure(string message) => new ApiResponse<T> { Success = false, Message = message };
        public static ApiResponse<T> SuccessResult(T data, string message = "") => new ApiResponse<T> { Success = true, Data = data, Message = message };
    }
}
