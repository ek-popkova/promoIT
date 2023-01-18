
namespace Shared
{
    public partial class CampaignsAndNpr
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Link { get; set; } = null!;
        public string Hashtag { get; set; } = null!;
        public int NPR_Id { get; set; }
        public string NPR_email { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationLink { get; set; }

    }
}
