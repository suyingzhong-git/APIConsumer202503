using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace APIConsumer.Models
{
    public class Lock
    {
        [Key]
        public long id { get; set; }
        
        public string name { get; set; }

        public decimal price { get; set; }

        [DisplayName("Image URLs")]
        public string? image_urls { get; set; }

        [DisplayName("Quantity")]
        public int quantity { get; set; }

        public bool is_active { get; set; }

        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }

    }
}
