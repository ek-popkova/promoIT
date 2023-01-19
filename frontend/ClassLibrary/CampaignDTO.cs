using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
public partial class CampaignDTO
{
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name is too long, 50 charachters maximum.")]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(250, ErrorMessage = "Link is too long, 250 charachters maximum.")]
        [DataType(DataType.Url)]
        public string Link { get; set; } = null!;
        [Required]
        [MaxLength(50, ErrorMessage = "Hashtag is too long, 50 charachters maximum.")]
        public string Hashtag { get; set; } = null!;
        public int NprId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CreateUserId { get; set; }
        public string UpdateUserId { get; set; }
        public int StatusId { get; set; }
    }
