using LIBUtilities.Core;
using System.Threading.Tasks;

namespace XAM_ProyectITM.Core
{
    public class MessageXAM : IMessage
    {
        public async Task<object> AsyncShow(object message, Message type = Message.MESSAGE)
        {
            if (message == null)
                return false;

            if (GlobalData.MasterDetailPage == null)
            {
                if (type == Message.QUESTION)
                    return await GlobalData.Application.MainPage.DisplayAlert(
                                    "Message", message.ToString(), "Accept", "Cancel");

                await GlobalData.Application.MainPage.DisplayAlert("Message", message.ToString(), "Close");
                return true;
            }
            if (type == Message.QUESTION)
                return await GlobalData.MasterDetailPage.Detail.DisplayAlert(
                                "Message", message.ToString(), "Accept", "Cancel");

            await GlobalData.MasterDetailPage.Detail.DisplayAlert("Message", message.ToString(), "Close");
            return true;
        }

        public object Show(object message, Message type = Message.MESSAGE)
        {
            if (message == null)
                return false;

            if (GlobalData.MasterDetailPage == null)
            {
                if (type == Message.QUESTION)
                    return GlobalData.Application.MainPage.DisplayAlert(
                                    "Message", message.ToString(), "Accept", "Cancel");

                GlobalData.Application.MainPage.DisplayAlert("Message", message.ToString(), "Close");
                return true;
            }
            if (type == Message.QUESTION)
                return GlobalData.MasterDetailPage.Detail.DisplayAlert(
                                "Message", message.ToString(), "Accept", "Cancel");

            GlobalData.MasterDetailPage.Detail.DisplayAlert("Message", message.ToString(), "Close");
            return true;
        }
    }
}