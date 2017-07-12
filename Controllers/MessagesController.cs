using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

using System.Diagnostics;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Autofac;
using Microsoft.Bot.Builder.FormFlow;

namespace ZplusBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        /// 
        public int count = 0;

        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {

            if (activity.Type == ActivityTypes.Message )
            {
                //   ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                // calculate something for us to return
                //  int length = (activity.Text ?? string.Empty).Length;

                // return our reply to the user
                //   Activity reply = activity.CreateReply($"You sent {activity.Text} which was {length} characters");
                //  await connector.Conversations.ReplyToActivityAsync(reply);

                await Conversation.SendAsync(activity, () => new RootLuisDialog());
            }
            else if (activity.Type == ActivityTypes.ConversationUpdate)
            {


                string replyMessage = "";
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

                replyMessage = "Hello! " + welcomeString + ", This is Annie, CSCInsure+ Automated Chatbot. Currently, we are supporting these features: Reset Password. You can type ‘Quit’ to close the conversation. How May I help you?";

                if (activity.MembersAdded.Any(o => o.Id == activity.Recipient.Id))
                {
                    var reply = activity.CreateReply(replyMessage);

                    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                    await connector.Conversations.ReplyToActivityAsync(reply);
                }


                //  await Conversation.SendAsync(activity, () => new RootLuisDialog(2));

            }
            else
            {
               HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);

           return response;
        }






        private async Task<Activity> HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels



            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened


            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }




    }
}