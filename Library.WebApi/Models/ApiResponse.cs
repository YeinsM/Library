namespace Library.WebApi.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public string? Code { get; set; }

        public static ApiResponse<T> Ok(T data, string message = "Operación satisfactoria!") =>
            new()
            {
                Success = true,
                Message = message,
                Data = data,
                Code = "200"
            };

        public static ApiResponse<T> Fail(string message, List<string>? errors = null, string? code = null) =>
            new()
            {
                Success = false,
                Message = message,
                Errors = errors,
                Code = code
            };



    }
}
