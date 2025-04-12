namespace PCH.NFP.API.Models;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public ApiResponse(T data, bool success = true, string message = "")
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public static ApiResponse<T> SuccessResponse(T data, string message = "") =>
        new ApiResponse<T>(data, true, message);

    public static ApiResponse<T> FailureResponse(string message) =>
        new ApiResponse<T>(default, false, message);

}