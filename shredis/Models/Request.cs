using System.ComponentModel.DataAnnotations;

namespace shredis.Models
{
    public class Request
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public object Value { get; set; }
    }
}
