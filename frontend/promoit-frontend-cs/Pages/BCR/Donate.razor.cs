using Microsoft.AspNetCore.Components;
using promoit_frontend_cs.Services;
using Shared;
using System;

namespace promoit_frontend_cs.Pages.BCR
{
    public partial class Donate
    {
        [Inject]
        BusinessCompanyRepresentativeService businessCompanyRepresentativeService { get; set; }
		[Inject]
		CampaignService campaignService { get; set; }
		[Inject]
		AuthService authService { get; set; }
        [Inject]
        PopupService popupService { get; set; }
        [Inject]
        Microsoft​.AspNetCore​.Http.IHttpContextAccessor HttpContextAccessor { get; set; }


        private IEnumerable<ProductToCampaignDTOShared> allProductsAndCampaigns = System.Array.Empty<ProductToCampaignDTOShared>();
        private IEnumerable<ProductsAndBcrInfo> listOfProductsByBCR = System.Array.Empty<ProductsAndBcrInfo>();
        private IEnumerable<CampaignsAndNpr> listOfCampaignsAndNPR = System.Array.Empty<CampaignsAndNpr>();

        private CampaignsAndNpr campaignToDonateTo;
        private ProductsAndBcrInfo productToDonate;

        private ProductDTO newProduct = new();

        private bool showTableProductsAndBCRInfo { get; set; } = false;
        private bool showTableCampaignsAndNRP { get; set; } = false;
        private bool showDonationForm { get; set; } = false;
        private bool showAddProductForm { get; set; } = false;
        private int initialNumber { get; set; }
        private string product_respond { get; set; }
        private bool product_flag { get; set; } = false;
        private string user_id { get; set; }
        private int bcr_id { get; set; }
		protected override async Task OnInitializedAsync() {

			user_id = HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == "id").Value;
			bcr_id = await businessCompanyRepresentativeService.GetBCRidByUserId(user_id);
            GetAllProductsToCampaigns();
		}

		private async Task<IEnumerable<CampaignsAndNpr>> GetCampaignsAndNPR()
        {
			showTableCampaignsAndNRP = !showTableCampaignsAndNRP;

            var response = await campaignService.GetCampaignsAndNpr();
            return listOfCampaignsAndNPR = response;

		}

		private async Task<IEnumerable<ProductToCampaignDTOShared>> GetAllProductsToCampaigns()
        {
            var response = await campaignService.GetAllProductsToCampaigns();
            return allProductsAndCampaigns = response;

		}

        private async Task<IEnumerable<ProductsAndBcrInfo>> GetProductsAndBCRInfo()
        {
            showTableProductsAndBCRInfo = !showTableProductsAndBCRInfo;

			var response = await businessCompanyRepresentativeService.GetProductsAndBCRInfo(bcr_id);
            return listOfProductsByBCR = response;

		}

        private async Task ShowDonationForm()
        {
			showDonationForm = !showDonationForm;
		}

		private async Task CheckAndDonateProduct()
        {
            if (campaignToDonateTo == null || productToDonate == null)
            {
                await popupService.ShowPopupNoData();
            }
            else
            {
                var product = new ProductToCampaignDTOShared()
                {
                    CampaignId = campaignToDonateTo.Id,
                    ProductId = productToDonate.Id,
                    InititalNumber = initialNumber,
                    BoughtNumber = 0,
                    CreateUserId = user_id,
                    UpdateUserId = user_id
                };
                if (product.InititalNumber <= 0)
                {
                    await popupService.ShowPopupWrongNumber();
                }
                else
                {
                    try
                    {
                        await campaignService.AnalizeAndPutProductToCampaign(product);
                        await popupService.ShowPopupThanks(campaignToDonateTo.OrganizationName);
                        showDonationForm = !showDonationForm;
                    }
                    catch (Exception exception) {
                        await popupService.ShowPopupException(exception.Message);
                    }
                }
            }
        }

        private void ShowProductForm()
        {
            showAddProductForm = !showAddProductForm;
            product_flag = false;
        }

        private async Task sendNewProduct()
        {
            newProduct.BcrId = bcr_id;
            newProduct.CreateUserId = user_id;
            newProduct.UpdateUserId = user_id;
            var message = await businessCompanyRepresentativeService.AddNewProduct(newProduct);
            if (message.ReasonPhrase == "Created")
            {
                product_respond = "Great! You have successfully created a product and now can donate it to any campaign you want";
            }
            else
            {
                product_respond = message.ReasonPhrase;
            }
            product_flag = true;
        }
    }
}
