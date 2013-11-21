using System;
using Framework.ServiceBus.ServiceHost;
using Framework.ServiceBus.Util;
using Ninject.Modules;

namespace Framework.ServiceBus.Bootstrapper
{
    public class PublisherServiceBusIoCRegistrations:NinjectModule
    {
        private readonly string _EndpointName;

        public PublisherServiceBusIoCRegistrations(Type endpointConfigurationType)
        {
            _EndpointName = EndpointNameResolver.Instance.Resolve(endpointConfigurationType);
        }

        public PublisherServiceBusIoCRegistrations(string endpointName)
        {
            _EndpointName = endpointName;
        }

        public override void Load()
        {
            var bus = BusFactory.ProduceBusForPublisher(_EndpointName);
            //Bind<IBus>().ToConstant(bus); -- dont need to bind, the ninject extension for nservicebus takes care of this for us
        }
    }
}
