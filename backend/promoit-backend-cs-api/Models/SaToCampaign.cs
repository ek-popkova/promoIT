
using Microsoft.EntityFrameworkCore;

namespace promoit_backend_cs_api.Models
{
/*    [Keyless]
*/    public partial class SaToCampaign
    {
        public int Id { get; set; }
        public int SocialActivistId { get; set; }
        public int CampaignId { get; set; }
        public int? Money { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }
        public int StatusId { get; set; }

        public virtual Campaign Campaign { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
