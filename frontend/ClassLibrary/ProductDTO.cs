
using System.ComponentModel.DataAnnotations;


    public partial class ProductDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Address is too long, 50 charachters maximum.")]
        public string Name { get; set; } = null!;

        [Required]
        [Range(1, 1000,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Value { get; set; }
        public int BcrId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CreateUserId { get; set; }
        public string UpdateUserId { get; set; }
        public int StatusId { get; set; }

    }
