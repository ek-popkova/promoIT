using Microsoft.AspNetCore.Components;
using promoit_frontend_cs.Services;
using Shared;
using System;

namespace promoit_frontend_cs.Pages.NPCR
{
    public partial class Campaigns
    {
        [Inject]
        CampaignService campaignService { get; set; }
        [Inject]
        NonProfitRepresentativeService nonProfitRepresentativeService { get; set; }
        [Inject]
        PopupService popupService { get; set; }
        [Inject]
        Microsoft​.AspNetCore​.Http.IHttpContextAccessor HttpContextAccessor { get; set; }

        private IEnumerable<CampaignDTO> listOfCampaigns = System.Array.Empty<CampaignDTO>();

        private CampaignDTO newCampaign = new();

        private bool showTableCampaigns { get; set; } = false;
        private bool showAddCampaignForm { get; set; } = false;

        private string campaign_respond { get; set; }

        private bool campaign_flag { get; set; } = false;


        protected override async Task OnInitializedAsync() { }

        private async Task<IEnumerable<CampaignDTO>> GetCampaigns()
        {
            showTableCampaigns = !showTableCampaigns;
            string user_id = HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == "id").Value;
            var npr_id = await nonProfitRepresentativeService.GetNPCRidByUserId(user_id);
            return listOfCampaigns = await campaignService.GetCampaignsByNPCRId(npr_id);
        }



        private void ShowProductForm()
        {
            showAddCampaignForm = !showAddCampaignForm;
            campaign_flag = false;
        }

        private async Task sendNewCampaign()
        {
            string user_id = HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == "id").Value;
            var npr_id = await nonProfitRepresentativeService.GetNPCRidByUserId(user_id);
            newCampaign.NprId = npr_id;
            newCampaign.CreateUserId = user_id;
            newCampaign.UpdateUserId = user_id;
            var message = await campaignService.AddNewCampaign(newCampaign);
            if (message.ReasonPhrase == "Created")
            {
                campaign_respond = "Great! You have successfully created a campaign, social activists will promote it on twitter.";
            }
            else
            {
                campaign_respond = message.ReasonPhrase;
            }
            campaign_flag = true;
        }
    }
}
