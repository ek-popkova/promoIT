using Microsoft.AspNetCore.Components;
//using promoit_backend_cs.Services;
using promoit_frontend_cs.Services;
using Shared;

namespace promoit_frontend_cs.Pages.SocialActivist
{
    public partial class BalanceAndProducts
    {
        [Inject]
        BusinessCompanyRepresentativeService businessCompanyRepresentativeService { get; set; }
        [Inject]
		Services.CampaignService campaignService { get; set; }
        [Inject]
        PopupService popupService { get; set; }
        [Inject]
        SocialActivistService socialActivistService { get; set; }
		[Inject]
		AuthService authService { get; set; }
		[Inject]
		IHttpContextAccessor HttpContextAccessor { get; set; }

		private IEnumerable<ProductsAndCampaignsShared> productsAndCampaigns = Array.Empty<ProductsAndCampaignsShared>();
        private IEnumerable<CampaignShared> allCampaigns = Array.Empty<CampaignShared>();
        private IEnumerable<SpResults> campaignsAndMoney = Array.Empty<SpResults>();

        private SaTransactionShared saTransactionShared = new SaTransactionShared();
        private SaToCampaignShared saToCampaignShared = new SaToCampaignShared();
        private CampaignShared chosenCampaign = new CampaignShared();
        private ProductsAndCampaignsShared productFromForeach;
        private SpResults campaignFromForeach;

        private bool ShowTableProductsAndCampaigns { get; set; } = false;
        private bool ShowTableCampaignsAndMoney { get; set; } = false;
        private bool ShowFormDonate { get; set; } = false;
        private string selectedCampaignName { get; set; }
        private bool ShowFormBuy { get; set; } = false;
        private int boughtNumber { get; set; } = 0;
        private int socialActId { get; set; }
        private string user_id { get; set; }
        protected override async Task OnInitializedAsync() 
        {
			user_id = HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == "id").Value;
			socialActId = await socialActivistService.GetSocialActivistById(user_id);
            Console.WriteLine($"{socialActId}");
        }

        private async Task GetCampaignsAndMoney()
        {
			ShowTableCampaignsAndMoney = !ShowTableCampaignsAndMoney;

            campaignsAndMoney = await socialActivistService.GetCampaignsAndMoney(socialActId);
        }

        private async Task<IEnumerable<ProductsAndCampaignsShared>> GetProductsAndCampaignsNames()
        {
            ShowTableProductsAndCampaigns = !ShowTableProductsAndCampaigns;
            ShowFormBuy = false;
            ShowFormDonate = false;

            return productsAndCampaigns = await campaignService.GetProductsAndCampaignsNames();
        }

        private async Task<IEnumerable<CampaignShared>> GetAllCampaigns()
        {
            ShowFormDonate = !ShowFormDonate;
            return allCampaigns = await campaignService.GetAllCampaigns();
        }

        private async Task ChooseCampaign(SpResults campaign) => campaignFromForeach = campaign;

        private async void EditProduct()
        {
            if (campaignFromForeach == null)
            {
                await popupService.ShowPopupChooseCampaign();
            }
            else
            {
                ShowFormBuy = !ShowFormBuy;
                ShowFormDonate = false;
                saTransactionShared.BCR_id = productFromForeach.BCRid;
                saTransactionShared.product_id = productFromForeach.productId;
                saTransactionShared.SA_id = campaignFromForeach.social_activist_id;
            }
        }

        private async Task BuyProduct(SaTransactionShared saTransactionShared, ProductsAndCampaignsShared productsAndCampaigns)
        {
            if (campaignFromForeach.campaign_id == productsAndCampaigns.campaignId)
            {
                saToCampaignShared.social_activist_id = saTransactionShared.SA_id;
                saToCampaignShared.campaign_id = productsAndCampaigns.campaignId;
                saTransactionShared.price = productFromForeach.productValue;
                saToCampaignShared.money = campaignFromForeach.money - productFromForeach.productValue * saTransactionShared.products_number;
                saTransactionShared.SA_id = campaignFromForeach.social_activist_id;
                saTransactionShared.create_user_id = user_id;
                saTransactionShared.update_user_id = user_id;

			    if (saToCampaignShared.money < 0)
                {
                    await popupService.ShowPopupNoMoney();
                }
                else
                {
                    campaignFromForeach.money = (int)saToCampaignShared.money;
                    var updateMoney = await socialActivistService.UpdateMoney(campaignFromForeach.id, saToCampaignShared);
                    var addTransaction = await businessCompanyRepresentativeService.AddSocialActivistTransaction(saTransactionShared);
                    await popupService.ShowPopupBought();
                }
            } else
            {
                await popupService.ShowPopupChooseAnotherCampaign(campaignFromForeach.campaignName);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        private async Task SelectProductToDonate()
        {
            if (campaignFromForeach == null)
            {
                await popupService.ShowPopupChooseCampaign();
            }
            else
            {
                ShowFormBuy = false;
            }
        }

        private async Task ChooseCampaignFromDropDown(string selectedCampaignName)
        {
            if (selectedCampaignName == null)
            {
                await popupService.ShowPopupChooseCampaign();
            }
            else
            {
                chosenCampaign = allCampaigns.Where(x => x.Name == selectedCampaignName).FirstOrDefault();
            }
        }


        private async Task AnalizeProductAndCampaignAndDonate(string selectedCampaignName, int boughtNumber)
        {
            int leftover = productFromForeach.InititalNumber - productFromForeach.BoughtNumber;
            int money = campaignFromForeach.money - productFromForeach.productValue* boughtNumber;
            if (boughtNumber < 0)
            {
                await popupService.ShowPopupWrongNumber();
            }
            else if ((leftover - boughtNumber) < 0){
                await popupService.ShowPopupNoProduct(leftover, productFromForeach.productName);
            }
            else if (money < 0) {
                await popupService.ShowPopupNoMoney();
            }
            else if (productFromForeach.campaignId != campaignFromForeach.campaign_id)
            {
                await popupService.ShowPopupChooseAnotherCampaign(campaignFromForeach.campaignName);
            }
            else {
                await socialActivistService.AnalyseAndDonate(user_id, selectedCampaignName, boughtNumber, productFromForeach);
                await popupService.ShowPopupThanks(selectedCampaignName);
            }

        }
    }
}
