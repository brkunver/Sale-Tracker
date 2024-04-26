namespace SaleTrackerBackend.Dto;

public class ResponseDto<T>
{
    public bool Success { get; set; } = true;
    public string? Message { get; set; } = string.Empty;
    public T? Data { get; set; }
}