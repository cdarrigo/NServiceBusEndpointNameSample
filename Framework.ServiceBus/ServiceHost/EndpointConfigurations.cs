using Framework.ServiceBus.Authentication;
using Framework.ServiceBus.Util;
using NServiceBus;

namespace Framework.ServiceBus.ServiceHost
{
    public abstract class EndpointConfigurations
    {

        public abstract class ServerEndpoint : BaseEndpointConfiguration, AsA_Server
        {

        }

        public abstract class EventPublisherEndpointConfig : BaseEndpointConfiguration, AsA_Publisher
        {
        }
        public abstract class BaseEndpointConfiguration : IConfigureThisEndpoint, IWantCustomInitialization, ISpecifyMessageHandlerOrdering
        {

            private readonly string _EndpointName;

            #region Constructors
            protected BaseEndpointConfiguration(string endpointName = null)
            {
                _EndpointName = endpointName;
            }

            protected BaseEndpointConfiguration()
            {
                _EndpointName = EndpointNameResolver.Instance.Resolve(this);
            }
            #endregion

            public virtual void SpecifyOrder(Order order)
            {
                order.SpecifyFirst<AuthenticationHandler>();
            }

            public virtual void Init()
            {
                BusFactory.ConfigureBus(_EndpointName);
            }
        }

    }
}
