using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public partial class BcrDTO
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Address is too long, 50 charachters maximum.")]
        public string CompanyName { get; set; } = null!;
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CreateUserId { get; set; }
        public string UpdateUserId { get; set; }
        public int StatusId { get; set; }

    }

