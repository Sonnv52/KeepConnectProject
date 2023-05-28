namespace Chat.Api.Respones.Common
{
    public class ResponseBase<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
