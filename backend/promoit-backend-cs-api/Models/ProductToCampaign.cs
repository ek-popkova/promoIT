
using System.ComponentModel.DataAnnotations;

namespace promoit_backend_cs_api.Models
{
    public partial class ProductToCampaign
    {
        [Key]
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int ProductId { get; set; }
        public int InititalNumber { get; set; }
        public int BoughtNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }
        public int StatusId { get; set; }

        public virtual Campaign Campaign { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
    }
}
