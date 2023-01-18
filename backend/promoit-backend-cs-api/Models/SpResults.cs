using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace promoit_backend_cs_api.Models
{
	[Keyless]
	public partial class SpResults
	{
		public int id { get; set; }

		public int social_activist_id { get; set; }

		public int campaign_id { get; set; }

		public string campaignName { get; set; }

		public int money { get; set; }
	}
}
