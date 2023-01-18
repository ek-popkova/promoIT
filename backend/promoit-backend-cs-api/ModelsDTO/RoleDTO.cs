
namespace promoit_backend_cs_api.ModelsDTO
{
    public partial class RoleDTO
    {

        public int Id { get; set; }
        public string RoleName { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }
        public int StatusId { get; set; }

    }
}
