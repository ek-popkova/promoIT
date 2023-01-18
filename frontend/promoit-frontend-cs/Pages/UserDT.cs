using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorWeb.Models.DataTransferModels
{
	public class UserDT
	{
		public int? Id { get; set; }

		[Required]
		[MinLength(2, ErrorMessage = "Name is too short, 3 charachters minimum.")]
		public string FirstName { get; set; } = null!;

		[Required]
		[MinLength(2, ErrorMessage = "Name is too short, 3 charachters minimum.")]
		public string LastName { get; set; } = null!;

		[Required]
		[EmailAddress(ErrorMessage = "Enter the valid e-mail address.")]
		public string Email { get; set; } = null!;
		[PasswordPropertyText]
		public string? Password { get; set; }
		public int RoleId { get; set; }
		[Required]
		public int? PointsSummary { get; set; }
	}
}