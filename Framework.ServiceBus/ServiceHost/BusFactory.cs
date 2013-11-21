using Framework.ServiceBus.Util;
using Ninject;
using NServiceBus;
using NServiceBus.Installation.Environments;

namespace Framework.ServiceBus.ServiceHost
{
    public static class BusFactory
    {
        public static IKernel BusKernel { get; set; }

        public static IBus StartBus<T>(bool configureAsSendOnly = false, Configure busConfiguration = null) where T : IConfigureThisEndpoint
        {
            return StartBus(configureAsSendOnly, busConfiguration, EndpointNameResolver.Instance.Resolve<T>());
        }
        public static IBus StartBus(bool configureAsSendOnly = false, Configure busConfiguration = null, string endpointName = null)
        {

            if (busConfiguration == null)
                busConfiguration = ConfigureBus(endpointName);

            if (!string.IsNullOrWhiteSpace(endpointName))
                busConfiguration.DefineEndpointName(endpointName);

            if (configureAsSendOnly)
                return busConfiguration.SendOnly();

            var bus = busConfiguration.CreateBus()
                .Start(() => Configure.Instance.ForInstallationOn<Windows>().Install()); // Force the installers to run when the bus starts
            return bus;

        }

        private static Configure _busConfiguration;


        public static Configure ConfigureBus(string endpointName = null)
        {

            if (BusKernel == null)
                BusKernel = new StandardKernel();

            if (_busConfiguration == null)
            {

                   var config = Configure.With()
                    .Log4Net()
                    .NinjectBuilder(BusKernel) // leverage Ninject for DI
                    .UseTransport<Msmq>()
                    .ConfigureUnobstrusiveMode() // Extension method in our configuration assembly                                       
                    .UnicastBus();


                if (!string.IsNullOrWhiteSpace(endpointName))
                    config.DefineEndpointName(endpointName);


                Configure.Serialization.Xml();
                _busConfiguration = config;
            }
            return _busConfiguration;
        }

        public static IBus ProduceBusForPublisher(string endpointName)
        {
            return StartBus(false, ConfigureBusForPublisher(endpointName));
        }

        public static Configure ConfigureBusForPublisher(string endpointName = null)
        {
            var busConfiguration = ConfigureBus(endpointName)
                .RavenPersistence()
                .RavenSubscriptionStorage();// configured via the RavenBootstrapper 
            return busConfiguration;

        }
    }
   
}
