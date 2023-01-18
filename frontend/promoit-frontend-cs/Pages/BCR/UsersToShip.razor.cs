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

		private IEnumerable<SaTransactionSharedSAInfo> transactionsByBcrId = System.Array.Empty<SaTransactionSharedSAInfo>();

        private bool showTransactionForm { get; set; } = false;
        private int BCR_id { get; set; } = 2;

        protected override async Task OnInitializedAsync() { }

        private async Task<IEnumerable<SaTransactionSharedSAInfo>> GetTransactionWithSAInfo(int id)
        {
            showTransactionForm = !showTransactionForm;
            return transactionsByBcrId = await _businessCompanyRepresentativeService.GetTransactionWithSAInfo(BCR_id);
        }

        private async Task SendProduct(SaTransactionSharedSAInfo saTransactionSharedSAInfo)
        {
            var result = await popupService.ShowConfirm();
            if (result)
            {
                saTransactionSharedSAInfo.transaction_status_id = 2;
                await businessCompanyRepresentativeService.SendProductToSocialActivist(saTransactionSharedSAInfo.Id);
            }
        }
    }
}
