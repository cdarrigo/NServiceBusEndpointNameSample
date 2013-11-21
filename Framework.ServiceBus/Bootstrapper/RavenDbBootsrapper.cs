using NServiceBus;
using NServiceBus.Persistence.Raven;
using Raven.Client;
using Raven.Client.Document;

namespace Framework.ServiceBus.Bootstrapper
{
    public class RavenBootstrapper : INeedInitialization
    {
        public void Init()
        {            

            Configure.Instance.Configurer.ConfigureComponent<IDocumentStore>(() =>
            {
                var store = new DocumentStore
                {
                    Url = "http://localhost:8080"
                };
                store.Initialize();
                store.JsonRequestFactory.DisableRequestCompression = true;
                return store;
            }, DependencyLifecycle.SingleInstance);

            Configure.Instance.Configurer.ConfigureComponent(() => Configure.Instance.Builder.Build<IDocumentStore>().OpenSession(), DependencyLifecycle.InstancePerUnitOfWork);
            Configure.Instance.Configurer.ConfigureComponent<RavenUnitOfWork>(DependencyLifecycle.InstancePerUnitOfWork);
        }
    }
}
