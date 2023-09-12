using System;
using System.Threading.Tasks;

namespace LIBUtilities.Core
{
    public class MessagesHelper
    {
        public static IMessage IMessage;

        public static void SetIntance(IMessage IMessage)
        {
            MessagesHelper.IMessage = IMessage;
        }

        public static void Show(string ex)
        {
            if (IMessage == null)
                return;

            IMessage.Show(ex);
        }

        public static void Show(Exception ex)
        {
            if (IMessage == null)
                return;

            IMessage.Show(ex);
        }

        public static async Task AsyncShow(string ex)
        {
            if (IMessage == null)
                return;

            await IMessage.AsyncShow(ex);
        }

        public static async Task AsyncShow(Exception ex)
        {
            if (IMessage == null)
                return;

            await IMessage.AsyncShow(ex);
        }

        public static async Task<object> AskAsync(string ex)
        {
            if (IMessage == null)
                return false;

            return await IMessage.AsyncShow(ex, Message.QUESTION);
        }
    }

    public enum Message { MESSAGE, QUESTION };

    public interface IMessage
    {
        object Show(object message, Message type = Message.MESSAGE);
        Task<object> AsyncShow(object message, Message type = Message.MESSAGE);
    }
}