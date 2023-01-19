using System.ComponentModel.DataAnnotations;

namespace promoit_backend_cs_api.ModelsDTO
{
    public partial class ProductToCampaignDTO
    {
        [Key]
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int ProductId { get; set; }
        public int InititalNumber { get; set; }
        public int BoughtNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CreateUserId { get; set; }
        public string UpdateUserId { get; set; }
        public int StatusId { get; set; }
/*
        public ProductDTO productDTO { get; set; }

        public CampaignDTO campaignDTO { get; set; }*/

    }
}
