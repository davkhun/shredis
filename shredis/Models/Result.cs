namespace shredis.Models
{
    public class Result
    {
        public int Code { get; set; }
        public string? Message { get; set; }
        public ValueObject? Value { get; set; } = null;
    }
}
