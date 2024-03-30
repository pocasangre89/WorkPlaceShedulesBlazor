namespace WorkPlaceShedulesBlazor.DTO
{
    public class ResponseApiDTO<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
    }
}
