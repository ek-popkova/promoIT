
namespace Shared
{
    public partial class ProductToCampaignShared
    {

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

        public virtual CampaignShared Campaign { get; set; } = null!;
        public virtual ProductShared Product { get; set; } = null!;

    }
}
