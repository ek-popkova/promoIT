
namespace promoit_backend_cs_api.Models
{
    public partial class SaTransaction
    {
        public int Id { get; set; }
        public int SaId { get; set; }
        public int BcrId { get; set; }
        public int ProductId { get; set; }
        public int ProductsNumber { get; set; }
        public int Price { get; set; }
        public int TransactionStatusId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }
        public int StatusId { get; set; }

        public virtual Bcr Bcr { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual Sa Sa { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
        public virtual TransactionStatus TransactionStatus { get; set; } = null!;
    }
}
