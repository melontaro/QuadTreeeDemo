using UniTaskPubSub;

namespace Coo
{
    public class UniTaskAsyncMessageBus
    {

        private static  AsyncMessageBus messageBus;
        public static AsyncMessageBus Inst {
            get
            {
                if (messageBus==null)
                {
                    messageBus = new AsyncMessageBus();
                }
                return messageBus;
            }
        }
    }
}