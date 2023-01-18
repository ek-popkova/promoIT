
using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public partial class SaTransactionShared
    {
        public int Id { get; set; }
        [Required]
        public int SA_id { get; set; }
        public int BCR_id { get; set; }
        [Required]
        public int product_id { get; set; }
        [Required]
        public int products_number { get; set; }
        [Required]
        public int price { get; set; }
        public int transaction_status_id { get ; set; }
    }
}
