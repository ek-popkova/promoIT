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
        PopupService popupService { get; set; }
        [Inject]
        Microsoft​.AspNetCore​.Http.IHttpContextAccessor HttpContextAccessor { get; set; }


        private IEnumerable<ProductToCampaignDTOShared> allProductsAndCampaigns = System.Array.Empty<ProductToCampaignDTOShared>();
        private IEnumerable<ProductsAndBcrInfo> listOfProductsByBCR = System.Array.Empty<ProductsAndBcrInfo>();
        private IEnumerable<CampaignsAndNpr> listOfCampaignsAndNPR = System.Array.Empty<CampaignsAndNpr>();

        private ProductToCampaignDTOShared productToCampaign;
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

        protected override async Task OnInitializedAsync() { }

        private async Task<IEnumerable<CampaignsAndNpr>> GetCampaignsAndNPR()
        {
            showTableCampaignsAndNRP = !showTableCampaignsAndNRP;
            return listOfCampaignsAndNPR = await campaignService.GetCampaignsAndNpr();
        }

        private async Task<IEnumerable<ProductToCampaignDTOShared>> GetAllProductsToCampaigns()
        {
            return allProductsAndCampaigns = await campaignService.GetAllProductsToCampaigns();
        }

        private async Task SelectCampaignToDonateTo(CampaignsAndNpr campaign) => campaignToDonateTo = campaign;

        private async Task<IEnumerable<ProductsAndBcrInfo>> GetProductsAndBCRInfo()
        {
            
            showTableProductsAndBCRInfo = !showTableProductsAndBCRInfo;
            string user_id = HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == "id").Value;
            var bcr_id = await businessCompanyRepresentativeService.GetBCRidByUserId(user_id);
            return listOfProductsByBCR = await businessCompanyRepresentativeService.GetProductsAndBCRInfo(bcr_id);
        }

        private async Task SelectProductToDonate(ProductsAndBcrInfo product)
        {
            showDonationForm = !showDonationForm;
            productToDonate = product;
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
                    BoughtNumber = 0
                };
                if (product.InititalNumber <= 0)
                {
                    await popupService.ShowPopupWrongNumber();
                }
                else
                {
                    if (allProductsAndCampaigns.Any(x => x.ProductId == product.ProductId && x.CampaignId == product.CampaignId))
                    {
                        var pac = allProductsAndCampaigns.Where(x => x.ProductId == product.ProductId && x.CampaignId == product.CampaignId)
                                                            .FirstOrDefault();

                        pac.InititalNumber += initialNumber;
                        var putProductToCampaign = await campaignService.PutProductToCampaign(pac.Id, pac);
                        await popupService.ShowPopupThanks(campaignToDonateTo.OrganizationName);
                    }
                    else
                    {
                        var postProductToCampaign = await campaignService.PostProductToCampaign(product);
                        await popupService.ShowPopupThanks(campaignToDonateTo.OrganizationName);
                        showDonationForm = !showDonationForm;
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
            string user_id = HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == "id").Value;
            var bcr_id = await businessCompanyRepresentativeService.GetBCRidByUserId(user_id);
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
