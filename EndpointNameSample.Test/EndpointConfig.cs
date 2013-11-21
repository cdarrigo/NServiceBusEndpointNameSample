
using Framework.ServiceBus.ServiceHost;

namespace EndpointNameSample.Test
{
    using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    [EndpointName("VMBusiness")]
    public class EndpointConfig : EndpointConfigurations.EventPublisherEndpointConfig
    {
    }
}
