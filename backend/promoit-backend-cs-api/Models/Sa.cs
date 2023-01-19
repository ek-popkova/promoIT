
namespace promoit_backend_cs_api.Models
{
    public partial class Sa
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Twitter { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CreateUserId { get; set; }
        public string UpdateUserId { get; set; }
        public int StatusId { get; set; }

        public virtual Status Status { get; set; } = null!;
    }
}
