using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public partial class NonProfitRepresentativeDTO
{

        public int Id { get; set; }
    
        [Required]
        [EmailAddress(ErrorMessage = "Enter the valid e-mail address.")]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(50, ErrorMessage = "Organisation name is too long, 50 charachters maximum.")]
        public string OrganizationName { get; set; } = null!;
    
        [Required]
        [MaxLength(250, ErrorMessage = "Link is too long, 250 charachters maximum.")]
        public string OrganizationLink { get; set; } = null!;
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CreateUserId { get; set; }
        public string UpdateUserId { get; set; }
        public int StatusId { get; set; }

    }

