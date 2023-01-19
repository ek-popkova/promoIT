
using Microsoft.EntityFrameworkCore;

namespace promoit_backend_cs_api.ModelsDTO
{
    [Keyless]
    public partial class SaToCampaignDTO
    {
        public int Id { get; set; }
        public int SocialActivistId { get; set; }
        public int CampaignId { get; set; }
        public int? Money { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CreateUserId { get; set; }
        public string UpdateUserId { get; set; }
        public int StatusId { get; set; }

    }
}
