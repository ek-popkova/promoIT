
namespace promoit_backend_cs_api.Models
{
    public partial class Bcr
    {
        public Bcr()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CreateUserId { get; set; }
        public string UpdateUserId { get; set; }
        public int StatusId { get; set; }

        public virtual Status Status { get; set; } = null!;
        public virtual ICollection<Product> Products { get; set; }
    }
}
