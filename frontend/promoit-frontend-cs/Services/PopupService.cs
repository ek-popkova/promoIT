using Microsoft.JSInterop;

namespace promoit_frontend_cs.Services
{
    public class PopupService 
    {
        public readonly IJSRuntime _jSRuntime;
        public PopupService(IJSRuntime JSRuntime) 
        {
            _jSRuntime= JSRuntime;
        }

        public async Task ShowPopupNoProduct(int leftover, string product)
        {
            await _jSRuntime.InvokeAsync<object>("alert", $"Sorry, we have only {leftover} {product}'s left");
        }
        public async Task ShowPopupException(string exception)
        {
            await _jSRuntime.InvokeAsync<object>("alert", exception);
        }

        public async Task ShowPopupNoData()
        {
            await _jSRuntime.InvokeAsync<object>("alert", "Please, select a campaign you want to donate to and a product you want to donate");
        }

        public async Task ShowPopupNoMoney()
		{
			await _jSRuntime.InvokeAsync<object>("alert", "Oooops... not enough money :(");
		}

        public async Task ShowPopupChooseCampaign()
		{
			await _jSRuntime.InvokeAsync<object>("alert", "Please, choose a campaign first!");
		}

        public async Task ShowPopupThanks(string campaignName)
		{
			await _jSRuntime.InvokeAsync<object>("alert", $"Thank you very much for this donation! '{campaignName}' appreciate it!");
		}

        public async Task ShowPopupBought()
		{
			await _jSRuntime.InvokeAsync<object>("alert", $"Thank you very much! Our manager will call you soon :)");
		}

		public async Task ShowPopupWrongNumber()
		{
			await _jSRuntime.InvokeAsync<object>("alert", $"Please, choose another number of input");
		}

		public async Task<bool> ShowConfirm()
        {
            return await _jSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to ship this product?");
            //var result = confirmed ? "Shipped" : "Okay, maybe later!";
        }

        public async Task ShowPopupChooseAnotherCampaign(string campaignName)
        {
            await _jSRuntime.InvokeAsync<object>("alert", $"money earned on campaign '{campaignName}' can only be spent on this campaign");
        }

    }
}
