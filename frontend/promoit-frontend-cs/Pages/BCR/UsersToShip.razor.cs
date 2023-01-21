using Microsoft.AspNetCore.Components;
using promoit_frontend_cs.Services;
using Shared;

namespace promoit_frontend_cs.Pages.BCR
{
    public partial class UsersToShip
    {
        [Inject]
        PopupService popupService { get; set; }
        [Inject]
        BusinessCompanyRepresentativeService businessCompanyRepresentativeService { get; set; }
		[Inject]
		AuthService authService { get; set; }
		[Inject]
		IHttpContextAccessor HttpContextAccessor { get; set; }

		private IEnumerable<SaTransactionSharedSAInfo> transactionsByBcrId = System.Array.Empty<SaTransactionSharedSAInfo>();

        private bool showTransactionForm { get; set; } = false;
		private string user_id { get; set; }
		private int bcr_id { get; set; }
        protected override async Task OnInitializedAsync() 
        {
			user_id = HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == "id").Value;
			bcr_id = await businessCompanyRepresentativeService.GetBCRidByUserId(user_id);
		}

        private async Task<IEnumerable<SaTransactionSharedSAInfo>> GetTransactionWithSAInfo(int id)
        {
            showTransactionForm = !showTransactionForm;

            return transactionsByBcrId = await _businessCompanyRepresentativeService.GetTransactionWithSAInfo(bcr_id);
        }

        private async Task SendProduct(SaTransactionSharedSAInfo saTransactionSharedSAInfo)
        {
            var result = await popupService.ShowConfirm();
            if (result)
            {
                saTransactionSharedSAInfo.transaction_status_id = 2;
                await businessCompanyRepresentativeService.SendProductToSocialActivist(saTransactionSharedSAInfo.Id, user_id);
            }
        }
    }
}
