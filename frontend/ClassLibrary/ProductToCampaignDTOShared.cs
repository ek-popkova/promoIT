using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public partial class ProductToCampaignDTOShared
    {
        [Key]
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int ProductId { get; set; }

		[Required]
		public int InititalNumber { get; set; }
		public int BoughtNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CreateUserId { get; set; }
        public string UpdateUserId { get; set; }
        public int StatusId { get; set; }

    }
}
