
using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public partial class SaTransactionSharedSAInfo
    {
        public int Id { get; set; }
        public int SA_id { get; set; }
        public int BCR_id { get; set; }
        public int product_id { get; set; }
        public int products_number { get; set; }
        public int price { get; set; }
        public int transaction_status_id { get; set; }
        public string productName { get; set; }
        public int productValue { get; set; }
        public virtual SocialActivistInfo socialAscivist { get; set; } = null!;
        public virtual BusinessCompanyRepresentativeInfo businessCompanyRepresentative { get; set; } = null!;

    }
}
