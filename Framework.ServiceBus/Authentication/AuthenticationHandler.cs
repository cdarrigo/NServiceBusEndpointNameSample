using Ninject;
using NServiceBus;

namespace Framework.ServiceBus.Authentication
{
    public class AuthenticationHandler : IHandleMessages<IMessage>
    {
        [Inject]
        public IBus Bus { get; set; }

        public void Handle(IMessage message)
        {
            // TODO -- add check for auth token in message header
        }
    }
}
