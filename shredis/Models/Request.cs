using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace shredis.Models
{
    public class Request
    {
        [Required]
        public string Key { get; set; } = string.Empty;
        [Required]
        public object Value { get; set; } = new object();
        [DefaultValue(null)]
        [Description("Key lifetime in minutes")]
        public int? ExpirationTimeInMinutes { get; set; } = null;
    }
}
