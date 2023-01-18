
using System.ComponentModel.DataAnnotations;

namespace promoit_backend_cs_api.Models
{
    public partial class BcrShip
    {
        [Key]
        public int Id { get; set; }
        public int BcrId { get; set; }
        public int CampaignId { get; set; }
        public int ProductId { get; set; }
        public int ProductReady { get; set; }
        public int ProductBought { get; set; }
        public int ProductPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }

        public virtual Bcr Bcr { get; set; } = null!;
        public virtual Campaign Campaign { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
