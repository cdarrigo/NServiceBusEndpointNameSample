using System.Reflection;
using Framework.ServiceBus.Bootstrapper;
using Framework.ServiceBus.ServiceHost;
using Framework.ServiceBus.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using NServiceBus;

namespace EndpointNameSample.Test
{
    [TestClass]
    public class UnitTest1
    {
        public static IKernel Kernel;
        [ClassInitialize]
        public static void RunOnceForAllTests(TestContext testContext)
        {
            // load up the IoC container 

            Kernel = new StandardKernel();
            BusFactory.BusKernel = Kernel;
            Kernel.Load(new PublisherServiceBusIoCRegistrations(typeof (EndpointConfig))); // pass the endpoint config type here and it will set the endpoint name when configuring the bus
            

        }

        [TestMethod]
        public void test_bus_inputqueuename()
        {
            var bus = Kernel.Get<IBus>();
            Assert.IsNotNull(bus);

            // Once the bus has started, it creates the following queues (if not already present... vmBusiness.Retries, vmBusiness.Timeouts, vmBusiness.TimeoutsDispatcher and Framework.ServiceBus.ServiceHost
            // This is incorrect behavior as the endpoint name is getting set via DefineQueueName() when the bus is configured 

            var expectedInputQueueName = EndpointNameResolver.Instance.Resolve<EndpointConfig>();
            var actualInputQueueName = GetInputQueueName(bus);
            Assert.AreEqual(expectedInputQueueName,actualInputQueueName,"ServiceBus' Input Queue Name is incorrect");

            
        }

        private string GetInputQueueName(IBus bus)
        {
            const string memberName = "inputAddress";
            var inputAddressField = bus.GetType().GetField(memberName, BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(inputAddressField);
            object inputAddress = inputAddressField.GetValue(bus);
            var queueNameProp = inputAddress.GetType().GetProperty("Queue", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return queueNameProp.GetValue(inputAddress).ToString();
        }
    }
}
