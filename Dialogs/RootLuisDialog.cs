
#define useSampleModel



namespace ZplusBot
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

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.IO;
    using System.Text;
    using System.Web.Script.Serialization;

    using System.Net;
    using System.Net.Http;

    using Services;
    using System.Web.UI.WebControls;
    using System.Net.Http.Headers;
    using System.Web.Configuration;
    using System.Text.RegularExpressions;

    [Serializable]
#if useSampleModel
    [LuisModel("77d5c1ae-f65d-48b7-bb8b-6ab96e400b17", "e7d8dc85a7fb478a8d7642f8ddbdc34b")]
#else
    [LuisModel("YourModelId", "YourSubscriptionKey")]
#endif
    public class RootLuisDialog : LuisDialog<object>
    {
        protected int count = 1;

        protected string gUSERID = "";

        protected string Pass1 = "";
        protected string Pass2 = "";

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = "Sorry, I did not understand. Type 'help' if you need assistance.";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("HELP")]
        [LuisIntent("Help")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            var msg = "Sure.. I am here to help you out with your queries. Currently, we are supporting these features: ";
            msg += "Reset Password. ";
            msg += "You can type 'Quit' to close the conversation. How May I help you?  ";

            await context.PostAsync(msg);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Escalation")]
        public async Task Escalation(IDialogContext context, LuisResult result)
        {
            var msg = "You can further escalate your issues and queries to customer care directly at 1800-00-1232 or email us at customer@insureplus.com ";

            await context.PostAsync(msg);
            context.Wait(this.MessageReceived);
        }


        [LuisIntent("AboutUs")]
        public async Task AboutUs(IDialogContext context, LuisResult result)
        {
            var msg = "The CSCInsure+ was formed in 1955 at the initiative of the World Bank, the Government of India and representatives of Indian industry. We are a private sector bank and offer various banking services. We operate in 17 countries including India.";

            await context.PostAsync(msg);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("DoingGood")]
        public async Task DoingGood(IDialogContext context, LuisResult result)
        {

            var msg = "Glad to hear from you. How may I help you?";
            await context.PostAsync(msg);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("NotDoingGood")]
        public async Task NotDoingGood(IDialogContext context, LuisResult result)
        {

            var msg = "Sorry to hear that. I completely empathize with you. How may I help you?";
            await context.PostAsync(msg);

            context.Wait(this.MessageReceived);
        }


        [LuisIntent("Howareyou")]
        [LuisIntent("HowAreYou")]
        public async Task Howareyou(IDialogContext context, LuisResult result)
        {
            var msg = "I am doing great! How are you?";

            await context.PostAsync(msg);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Thankyou")]
        [LuisIntent("ThankYou")]
        public async Task ThankYou(IDialogContext context, LuisResult result)
        {
           // var msg = "Thank you too! Is there anything else I can help you with?";

          //  await context.PostAsync(msg);
          //  context.Wait(this.MessageReceived);

            var msg = "Thank you too! Is there anything else I may help you with?\n\nType '**Yes**' or '**No**'";

            PromptDialog.Confirm(
            context,
            AfterConfirming_Exit,
            msg,
            "Didn't get your answer! Type '**Yes**' or '**No**'",
            promptStyle: PromptStyle.None);


        }

        public async Task AfterConfirming_Exit(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm)
            {
                await context.PostAsync("Sure. How may I help you?");

            }
            else
            {
                await context.PostAsync("It was nice talking to you. Thank you for contacting CSCInsure+ Chatbot services. Good Bye!");

            }
            context.Wait(this.MessageReceived);
            // 
        }


        [LuisIntent("WhoAreYou")]
        public async Task WhoAreYou(IDialogContext context, LuisResult result)
        {
            var msg = "My name is Annie. I am here to help you with any queries about CSCInsure+.";

            await context.PostAsync(msg);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("OutOfScope")]
        public async Task OutOfScope(IDialogContext context, LuisResult result)
        {
            var msg = "Sorry, I did not understand that. Can you please ask me something I can answer?";

            await context.PostAsync(msg);
            context.Wait(this.MessageReceived);
        }


        [LuisIntent("PasswordReset")]
        public async Task PasswordReset(IDialogContext context, LuisResult result)
        {
            var form = new UserDetailsFormFlow();
            var formDialog = new FormDialog<UserDetailsFormFlow>(form, UserDetailsBuildForm, FormOptions.PromptInStart);
            context.Call(formDialog, UserDetailsAuthenticate);
        }


        [LuisIntent("Exit")]
        [LuisIntent("CloseConversation")]
        public async Task CloseConversation(IDialogContext context, LuisResult result)
        {
            var msg = "It was nice talking to you. Thank you for contacting CSCInsure+ Chatbot services. Good Bye!";

            await context.PostAsync(msg);
            context.Wait(this.MessageReceived);
        }



        [LuisIntent("Welcome")]
        public async Task StartConversation(IDialogContext context, LuisResult result)
        {
            var msg = "";

            string welcomeString = "";

            if (DateTime.Now.Hour < 12)
            {
                welcomeString = "Good Morning";
            }
            else if (DateTime.Now.Hour < 17)
            {
                welcomeString = "Good Afternoon";
            }
            else
            {
                welcomeString = "Good Evening";
            }


            msg = "Hello! " + welcomeString + ", This is Annie, CSCInsure+ Automated Chatbot. Currently, we are supporting these features: Reset Password. You can type ‘Quit’ to close the conversation. How May I help you?";
            
          
            await context.PostAsync(msg);
            context.Wait(this.MessageReceived);



        }


        public async Task UserDetailsAuthenticate(IDialogContext context, IAwaitable<UserDetailsFormFlow> result)
        {
            try
            {

                var res = await result;

            var msg = "";

            string USERID = res.USERID;
            string AssetID = res.AssetID;
            UserDetailsresults userDetailsResult = await GetUserDetailsAsync(USERID, "");

               if(userDetailsResult !=  null)
            {
                UserDetails userDetails = userDetailsResult.result;

                if (userDetails.assetId.IndexOf(AssetID) > -1 && userDetails.userName.IndexOf(USERID) > -1)
                {
                    gUSERID = USERID;
                    var form = new QnAFormFlow();
                    var formDialog = new FormDialog<QnAFormFlow>(form, QnABuildForm, FormOptions.PromptInStart);
                    context.Call(formDialog, QnAAuthenticate);


                }
                else
                {
                    msg = "Sorry, we couldn't authenticate you. Please check your details and contact us again. Have a good day.";
                    await context.PostAsync(msg);
                    context.Wait(this.MessageReceived);

                }
            }
            else
            {
                msg = "Sorry, we couldn't authenticate you. Please check your details and contact us again. Have a good day.";
                await context.PostAsync(msg);
                context.Wait(this.MessageReceived);

            }
            }
            catch (FormCanceledException<UserDetailsFormFlow> e)
            {
                string reply;

                if (e.InnerException == null)
                {
                    reply = "OK. You have cancelled the dialog. Type 'Password Reset' to again start the dialog flow";
                }
                else
                {
                    reply = $"Oops! Something went wrong :(. Technical Details: {e.InnerException.Message}";
                }

                await context.PostAsync(reply);
                context.Wait(this.MessageReceived);
            }
           
        }

        public async Task QnAAuthenticate(IDialogContext context, IAwaitable<QnAFormFlow> result)
        {
            try
            {

                var res = await result;

            var msg = "";

            string Question1 = res.Question1;
            string Question2 = res.Question2;
            string Question3 = res.Question3;
            string Question4 = res.Question4;

            QnADetails qnADetails = await GetQnADetailsAsync(gUSERID);
      

            var msg1 = "{ ";
            msg1 += " \"userName\": \"" + gUSERID + "\",";
            msg1 += "  \"questionNAnswers\": [";
            msg1 += "         { ";
            msg1 += "     \"question\": \"Make of the first car?\",";
            msg1 += "     \"answer\": \"" + Question1 + "\"";
            msg1 += "         },";
            msg1 += "   { ";
            msg1 += "     \"question\": \"Name of the first pet?\",";
            msg1 += "     \"answer\": \"" + Question2 + "\"";
            msg1 += "         },";
            msg1 += "   {  ";
            msg1 += "     \"question\": \"Mother's maiden name?\",";
            msg1 += "     \"answer\": \"" + Question3 + "\"";
            msg1 += "         },";
            msg1 += "   {";
            msg1 += "    \"question\": \"Best friend in school?\",";
            msg1 += "    \"answer\": \"" + Question4 + "\"";
            msg1 += "         } ";
            msg1 += "  ]";
            msg1 += " }";
            
            QnAResult qnARes = await VerifyAnswersAsync(msg1);


            if (qnARes.result.IndexOf("true") > -1)
            {

            

              /*  var link = "http://zinsureplus.mybluemix.net/#/passwordreset";

                msg = "You have been authenticated successfully\n\nPlease click this [link](" + link + ")  to reset your password\n\n";
                await context.PostAsync(msg);
                context.Wait(this.MessageReceived);
                */
                msg = "You have been authenticated successfully\n\n";
                msg += "Do you want to reset the password now?\n\n Type '**Yes**' or '**No**' ";

                   /*
                PromptDialog.Choice(
             context,
             AfterSuccessQnAAsync,
             msg,
             "Didn't get your answer!",
             promptStyle: PromptStyle.None);

                    */
                    PromptDialog.Confirm(
                   context,
                   AfterSuccessQnAAsync,
                   msg,
                   "Didn't get your answer! Type '**Yes**' or '**No**'",
                   promptStyle: PromptStyle.None);

                    /*
                   var PromptOptions = new string[] { "Yes", "No" };

                   PromptDialog.Choice(context, AfterSuccessQnAAsync, PromptOptions, msg, "Didnt get your anser", promptStyle: PromptStyle.Auto);
                     */
                }
                else
            {
                msg = "Sorry, we couldn't authenticate you. Please check your details and contact us again. Have a good day.";
                await context.PostAsync(msg);
                context.Wait(this.MessageReceived);

            }
            }
            catch (FormCanceledException<QnAFormFlow> e)
            {
                string reply;

                if (e.InnerException == null)
                {
                    reply = "OK. You have cancelled the dialog. Type 'Password Reset' to again start the dialog flow";
                }
                else
                {
                    reply = $"Oops! Something went wrong :(. Technical Details: {e.InnerException.Message}";
                }

                await context.PostAsync(reply);
                context.Wait(this.MessageReceived);
            }

        }





    public async Task AfterSuccessQnAAsync(IDialogContext context, IAwaitable<bool> argument)
    {
            var msg = "Please enter your new password";
        var confirm = await argument;
            if (confirm)
            {
                
                    PromptDialog.Text(
                      context,
                      AfterConfirmPassAsync,
                      msg,
                      "Didn't get your answer!");
               
            }
            else
            {
                await context.PostAsync("You have not confirmed to reset the password.  Thank you for contacting us. Have a good day.");
                context.Wait(this.MessageReceived);
            }
        // 
    }

        public async Task AfterConfirmPassAsync(IDialogContext context, IAwaitable<string> argument)
        {
            var msg = "Please confirm new password";
            var confirm = await argument;
            Pass1 = confirm;
            if (confirm.Length > 0)
            {
                PromptDialog.Text(
                  context,
                  AfterValidatePassAsync,
                  msg,
                  "Didn't get your answer!");

            }
            else
            {
                await context.PostAsync("You have entered empty password.");
                context.Wait(this.MessageReceived);
            }
            // 
        }

        public async Task AfterValidatePassAsync(IDialogContext context, IAwaitable<string> argument)
        {
            var msg = "";
            var confirm = await argument;
            Pass2 = confirm;

            string Rand1 = "";
            string Rand2 = "";
            string Rand3 = "";
            string Rand4 = "";
            string Rand5 = "";

            string stringArr1 = "";
            string stringArr2 = "";
            string stringArr3 = "";
            string stringArr4 = "";
            string stringArr5 = "";



            Random random = new Random((int)DateTime.Now.Ticks);
            //ar chars;
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
                var chars = Enumerable.Range(0, 6)
                                  .Select(x => input[random.Next(0, input.Length)]);
                stringArr1 = new string(chars.ToArray());


            chars = Enumerable.Range(0, 6)
                                  .Select(x => input[random.Next(0, input.Length)]);
            stringArr2 = new string(chars.ToArray());

            chars = Enumerable.Range(0, 6)
                                  .Select(x => input[random.Next(0, input.Length)]);
            stringArr3 = new string(chars.ToArray());

            chars = Enumerable.Range(0, 6)
                                  .Select(x => input[random.Next(0, input.Length)]);
            stringArr4 = new string(chars.ToArray());

            chars = Enumerable.Range(0, 6)
                                  .Select(x => input[random.Next(0, input.Length)]);
            stringArr5 = new string(chars.ToArray());

            //   var chars = Enumerable.Range(0, 6)
            //                        .Select(x => input[random.Next(0, input.Length)]);
            // Rand1 = new string(chars.ToArray());

            if (Pass1.Length == Pass2.Length)
            {
                if (Pass1.IndexOf(Pass2) > -1)
                { 
                    msg = "Your password reset is successful. \n\n";
                    //    msg += "Request you to use the Token : **" + Rand1.ToUpper() + "** and newly reset password to login to your system. \n\n";
                    msg += "Request you to use the Token : **";
                    msg += stringArr1.ToUpper() + "-" + stringArr2.ToUpper() + "-" + stringArr3.ToUpper() + "-" + stringArr4.ToUpper() + "-"  + stringArr5.ToUpper();
                    msg += "** and newly reset password to login to your system. \n\n";
                    msg += "Thank you for contacting us. Have a good day. ";

                    await context.PostAsync(msg);
                    context.Wait(this.MessageReceived);

                }
                else
                {

                    msg = "The passwords you have entered are not matching\n\nDo you want to reset the password again now?\n\n Type '**Yes**' or '**No**'";

                    /*    var PromptOptions = new string[] { "Yes", "No" };

                      PromptDialog.Choice(context, AfterSuccessQnAAsync, PromptOptions, msg, "Didnt get your anser", promptStyle: PromptStyle.Auto);
                        */

                    PromptDialog.Confirm(
                   context,
                   AfterSuccessQnAAsync,
                   msg,
                   "Didn't get your answer! Type '**Yes**' or '**No**'",
                   promptStyle: PromptStyle.None);

                }


            }
            else
            {
                msg = "The passwords you have entered are not matching\n\nDo you want to reset the password again now?\n\n Type '**Yes**' or '**No**'";
                /*
                var PromptOptions = new string[] { "Yes", "No" };

                PromptDialog.Choice(context, AfterSuccessQnAAsync, PromptOptions, msg, "Didnt get your anser", promptStyle: PromptStyle.Auto);
               */

                PromptDialog.Confirm(
             context,
             AfterSuccessQnAAsync,
             msg,
             "Didn't get your answer!  Type '**Yes**' or '**No**'",
             promptStyle: PromptStyle.None);
               
            }



        }

        /************* UserDetails FormFlow ******************/

        [Serializable]
        public class UserDetailsFormFlow
        {

            [Prompt("Please enter your USERID? {||}", AllowDefault = BoolDefault.True)]
            public string USERID { get; set; }

            [Prompt("Please enter your assetID? {||}", AllowDefault = BoolDefault.True)]
            public string AssetID { get; set; }

        }

        public static IForm<UserDetailsFormFlow> UserDetailsBuildForm()
        {
            /*
            var builder = new FormBuilder<UserDetailsFormFlow>();

            builder.Configuration.DefaultPrompt.ChoiceStyle = ChoiceStyleOptions.Auto;

            builder.Configuration.Yes = new string[] { "yes", "Ok", "ok" };
            builder.Configuration.No = new string[] { "No", "no", "na" };

            builder.Message("Please answer few questions for authentication!");
            builder.Field(nameof(UserDetailsFormFlow.USERID));
            builder.Field(nameof(UserDetailsFormFlow.AssetID));
            builder.Confirm("Is this your selection? User ID: {USERID}, Asset ID: {AssetID}");
            builder.Build();

        */

         /*   return new FormBuilder<UserDetailsFormFlow>()
                    .Message("Please answer few questions for authentication!")
                    .Field(nameof(UserDetailsFormFlow.USERID))
                    .Field(nameof(UserDetailsFormFlow.AssetID))
                    .Confirm("Is this your selection? \n\n User ID: {USERID} \n\n Asset ID: {AssetID} \n\n Type '**Yes**' or '**No**' ")
                    .Build();
*/
            return CreateCustomForm<UserDetailsFormFlow>()
                 .Message("Please answer few questions for authentication!")
                 .Field(nameof(UserDetailsFormFlow.USERID))
                 .Field(nameof(UserDetailsFormFlow.AssetID))
                 .Confirm("Is this your selection? \n\n User ID: {USERID} \n\n Asset ID: {AssetID} \n\n Type '**Yes**' or '**No**' ")
                 .Build();




        }

        public static IFormBuilder<T> CreateCustomForm<T>() where T : class
        {
            var form = new FormBuilder<T>();
            var yesTerms = form.Configuration.Yes.ToList();
            yesTerms.Add("Yes");
            yesTerms.Add("ya");
            yesTerms.Add("ok");

            var noTerms = form.Configuration.No.ToList();
            noTerms.Add("No");
            noTerms.Add("na");
            form.Configuration.Yes = yesTerms.ToArray();
            form.Configuration.No = noTerms.ToArray();

            return form;

        }

        /************* QnA FormFlow ******************/

        [Serializable]
        public class QnAFormFlow
        {

            [Prompt("Make of the first car? {||}", AllowDefault = BoolDefault.True)]
            public string Question1 { get; set; }

            [Prompt("Name of the first pet? {||}", AllowDefault = BoolDefault.True)]
            public string Question2 { get; set; }

            [Prompt("Mother's maiden name? {||}", AllowDefault = BoolDefault.True)]
            public string Question3 { get; set; }

            [Prompt("Best friend in school? {||}", AllowDefault = BoolDefault.True)]
            public string Question4 { get; set; }


        }

        public static IForm<QnAFormFlow> QnABuildForm()
        {

            return new FormBuilder<QnAFormFlow>()
                    .Message("Please answer few security questions for authentication!")
                    .Field(nameof(QnAFormFlow.Question1))
                    .Field(nameof(QnAFormFlow.Question2))
                    .Field(nameof(QnAFormFlow.Question3))
                    .Field(nameof(QnAFormFlow.Question4))
                    .Confirm("Is this your selection? \n\nMake of the first car: {Question1} \n\nName of the first pet: {Question2} \n\nMother's maiden name: {Question3} \n\nBest friend in school: {Question4}  \n\n Type '**Yes**' or '**No**'")
                    .Build();


        }








        /*********** function to get userdetails ******************************/
        public static async Task<UserDetailsresults> GetUserDetailsAsync(string USERID, string AssetID)
        {
            UserDetailsresults jsonResponse = null;
            try
            {

                string ServiceURL = "";

                if (USERID != "")
                {
                    ServiceURL = $"http://ec2-52-77-250-97.ap-southeast-1.compute.amazonaws.com:8080/api/userinfo/getbyusername?username=" + USERID;
                }

                if (AssetID != "")
                {
                    ServiceURL = $"http://ec2-52-77-250-97.ap-southeast-1.compute.amazonaws.com:8080/api/userinfo/getbyassetid?assetid=" + AssetID;
                }

                if (ServiceURL.Length > 0)
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ServiceURL);
                    request.ContentType = "application/json; charset=utf-8";
                    request.PreAuthenticate = true;
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    string responseFromServer = "";

                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                        responseFromServer = reader.ReadToEnd();
                        Console.WriteLine(responseFromServer);
                    }
                    responseFromServer = Regex.Replace(responseFromServer, @"\t|\n|\r", "");

                    try
                    {

                        jsonResponse = JsonConvert.DeserializeObject<UserDetailsresults>(responseFromServer);
                    }
                    catch (Exception e)
                    {
                        //add code to handle exceptions while calling the REST endpoint and/or deserializing the object
                    }
                }
            }
            catch (Exception e)
            {
                //add code to handle exceptions while calling the REST endpoint and/or deserializing the object
            }
            return jsonResponse;

        }


        /************ get question and answers *******************************/
        public static async Task<QnADetails> GetQnADetailsAsync(string USERID)
        {
            QnADetailsresults jsonResponse = null;
            try
            {

                string ServiceURL = "";
                if (USERID != "")
                {
                    ServiceURL = $"http://ec2-52-77-250-97.ap-southeast-1.compute.amazonaws.com:8080/api/usersecurityinfo/getqnaforauthorization?username=" + USERID;
                }

                if (ServiceURL.Length > 0)
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ServiceURL);
                    request.ContentType = "application/json; charset=utf-8";
                    request.PreAuthenticate = true;
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    string responseFromServer = "";
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                        responseFromServer = reader.ReadToEnd();
                        Console.WriteLine(responseFromServer);
                    }
                    responseFromServer = Regex.Replace(responseFromServer, @"\t|\n|\r", "");


                    try
                    {

                        jsonResponse = JsonConvert.DeserializeObject<QnADetailsresults>(responseFromServer);
                    }
                    catch (Exception e)
                    {
                        //add code to handle exceptions while calling the REST endpoint and/or deserializing the object
                    }
                }
            }
            catch (Exception e)
            {
                //add code to handle exceptions while calling the REST endpoint and/or deserializing the object
            }
            return jsonResponse.result;

        }


        /*************** verify answers are correct ********/
        /*********** function to get userdetails ******************************/


        public static async Task<QnAResult> VerifyAnswersAsync(string reqObj)
        {
            QnAResult jsonResponse = null;
            try
            {

                string ServiceURL = "";
                ServiceURL = $"http://ec2-52-77-250-97.ap-southeast-1.compute.amazonaws.com:8080/api/usersecurityinfo/isanswerscorrect";


                if (reqObj.Length > 0)
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ServiceURL);
                    request.ContentType = "application/json; charset=utf-8";
                    request.PreAuthenticate = true;
                    request.Method = "POST";
                    byte[] byteArray = Encoding.UTF8.GetBytes(reqObj);
                    request.ContentLength = byteArray.Length;

                    Stream dataStream = request.GetRequestStream();
                    // Write the data to the request stream.  
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    // Close the Stream object.  
                    dataStream.Close();
                    // Get the response.  

                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    string responseFromServer = "";

                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                        responseFromServer = reader.ReadToEnd();
                        Console.WriteLine(responseFromServer);
                    }
                    responseFromServer = Regex.Replace(responseFromServer, @"\t|\n|\r", "");

                    try
                    {

                        jsonResponse = JsonConvert.DeserializeObject<QnAResult>(responseFromServer);
                    }
                    catch (Exception e)
                    {
                        //add code to handle exceptions while calling the REST endpoint and/or deserializing the object
                    }
                }
            }
            catch (Exception e)
            {
                //add code to handle exceptions while calling the REST endpoint and/or deserializing the object
            }
            return jsonResponse;

        }





}
}
