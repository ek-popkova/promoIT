namespace Shared
{
    public partial class CampaignShared
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string Link { get; set; } = null!;
            public string Hashtag { get; set; } = null!;
            public int NprId { get; set; }
            public DateTime CreateDate { get; set; }
            public DateTime UpdateDate { get; set; }
            public string CreateUserId { get; set; }
            public string UpdateUserId { get; set; }
            public int StatusId { get; set; }

        }

} 

