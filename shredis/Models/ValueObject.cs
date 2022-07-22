namespace shredis.Models
{
    [Microsoft.AspNetCore.Mvc.ApiExplorerSettings(IgnoreApi = true)]
    public class ValueObject
    {
        public string? Type { get; set; }
        public object? Value { get; set; }
    }
}
