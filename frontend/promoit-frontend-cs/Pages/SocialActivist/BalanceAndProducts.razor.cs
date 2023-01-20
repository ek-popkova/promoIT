using Microsoft.AspNetCore.Components;
using promoit_backend_cs.Services;
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

        protected override async Task OnInitializedAsync() { }

        private async Task GetCampaignsAndMoney()
        {
        
			user_id = HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == "id").Value;
			socialActId = await socialActivistService.GetSocialActivistById(user_id);
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

        private async void EditProduct(ProductsAndCampaignsShared productsAndCampaigns)
        {
            if (campaignFromForeach == null)
            {
                await popupService.ShowPopupChooseCampaign();
            }
            else
            {
                ShowFormBuy = !ShowFormBuy;
                ShowFormDonate = false;
                productFromForeach = productsAndCampaigns;
                saTransactionShared.BCR_id = productsAndCampaigns.BCRid;
                saTransactionShared.product_id = productsAndCampaigns.productId;
                saTransactionShared.SA_id = campaignFromForeach.social_activist_id;
            }
        }

        private async Task BuyProduct(SaTransactionShared saTransactionShared, ProductsAndCampaignsShared productsAndCampaigns)
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
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        private async Task SelectProductToDonate(ProductsAndCampaignsShared productsAndCampaigns)
        {
            if (campaignFromForeach == null)
            {
                await popupService.ShowPopupChooseCampaign();
            }
            else
            {
                ShowFormBuy = false;
                productFromForeach = productsAndCampaigns;
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

        private async Task AnalizeProductAndCampaignAndDonate(IEnumerable<ProductsAndCampaignsShared> productsAndCampaigns, CampaignShared ChosenCampaign, int boughtNumber)
        {
            saToCampaignShared.social_activist_id = socialActId;

            if (productsAndCampaigns.Any(x => x.campaignId == ChosenCampaign.Id && x.productId == productFromForeach.productId))
            {
                var pac = productsAndCampaigns.Where(x => x.campaignId == ChosenCampaign.Id && x.productId == productFromForeach.productId).FirstOrDefault();
                var ptc = new ProductToCampaignDTOShared()
                {
                    Id = pac.Id,
                    CampaignId = pac.campaignId,
                    ProductId = pac.productId,
                    InititalNumber = pac.InititalNumber,
                    BoughtNumber = pac.BoughtNumber + boughtNumber,
                    UpdateUserId = user_id
				};
                if (boughtNumber < 0)
                {
                    await popupService.ShowPopupWrongNumber();
                }
                else
                {
                    saToCampaignShared.campaign_id = pac.campaignId;
                    saToCampaignShared.money = campaignFromForeach.money - boughtNumber * pac.productValue;

                    if (saToCampaignShared.money < 0)
                    {
                        await popupService.ShowPopupNoMoney();
                    }
                    else
                    {
                        campaignFromForeach.money = (int)saToCampaignShared.money;
                        var updateMoney = await socialActivistService.UpdateMoney(campaignFromForeach.id, saToCampaignShared);
                        var putProductToCampaign = await campaignService.PutProductToCampaign(pac.Id, ptc);
                        //await popupService.ShowPopupThanks(pac.campaignName);
                    }
                }
            }
            else
            {
                var ptc = new ProductToCampaignDTOShared()
                {
                    CampaignId = ChosenCampaign.Id,
                    ProductId = productFromForeach.productId,
                    InititalNumber = boughtNumber,
                    BoughtNumber = boughtNumber,
					CreateUserId = user_id,
					UpdateUserId = user_id
				};
                if (boughtNumber <= 0)
                {
                    await popupService.ShowPopupWrongNumber();

                }
                else
                {
                    saToCampaignShared.campaign_id = ChosenCampaign.Id;
                    saToCampaignShared.money = campaignFromForeach.money - boughtNumber * productFromForeach.productValue;

                    if (saToCampaignShared.money < 0)
                    {
                        await popupService.ShowPopupNoMoney();

                    }
                    else
                    {
                        var updateMoney = await socialActivistService.UpdateMoney(campaignFromForeach.id, saToCampaignShared);
                        var postPtoductToCampaign = await campaignService.PostProductToCampaign(ptc);
                        await popupService.ShowPopupThanks(ChosenCampaign.Name);
                    }
                }
            }
        }
    }
}
