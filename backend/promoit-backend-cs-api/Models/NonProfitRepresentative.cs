
namespace promoit_backend_cs_api.Models
{
    public partial class NonProfitRepresentative
    {
        public NonProfitRepresentative()
        {
            Campaigns = new HashSet<Campaign>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string OrganizationName { get; set; } = null!;
        public string OrganizationLink { get; set; } = null!;
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CreateUserId { get; set; }
        public string UpdateUserId { get; set; }
        public int StatusId { get; set; }

        public virtual Status Status { get; set; } = null!;
        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}
