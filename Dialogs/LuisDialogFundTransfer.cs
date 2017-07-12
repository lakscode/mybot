
#define useSampleModel



namespace HealthcareBot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.FormFlow;
    using Microsoft.Bot.Builder.Luis;
    using Microsoft.Bot.Builder.Luis.Models;
    using Microsoft.Bot.Connector;

    [Serializable]
#if useSampleModel
    [LuisModel("891763e5-d75d-440b-8e7c-2ed1efecd08d", "e7d8dc85a7fb478a8d7642f8ddbdc34b")]
#else
    [LuisModel("YourModelId", "YourSubscriptionKey")]
#endif



    public class LuisDialogFundTransfer : LuisDialog<object>
    {
   

        /// <summary>
        /// For fund transfer
        /// 
        /// </summary>
        public string fundPercent;
        public string fundFrom;
        public string fundTo;

        public async Task FundTransfers(IDialogContext context, LuisResult result)
        {
            PromptDialog.Choice(
                context: context,
                resume: GetFromFund,
                options: new string[] { "ABC", "XYZ" },
                prompt: "From Which Fund you want to Transfer?",
                retry: "I didn't understand. Please try again.");
        }

        public async Task GetFromFund(IDialogContext context, IAwaitable<string> argument)
        {
            fundFrom = await argument;

            PromptDialog.Choice(
                 context: context,
                 resume: GetToFund,
                 options: new string[] { "ABC-To", "XYZ-To" },
                 prompt: "To Which Fund you want to Transfer?",
                 retry: "I didn't understand. Please try again.");

        }


        public async Task GetToFund(IDialogContext context, IAwaitable<string> argument)
        {
            fundTo = await argument;

            PromptDialog.Text(context: context, resume: FinishFundTransfer, prompt: "Please enter %(percent) of transfer");

        }


        public async Task FinishFundTransfer(IDialogContext context, IAwaitable<string> argument)
        {
            fundPercent = await argument;

            await context.PostAsync(fundPercent + " percent From Fund  " + fundFrom + " to " + fundTo + " transfer is initiated. ANything else I can help you?");
            context.Wait(this.MessageReceived);

        }


        

    }
}
