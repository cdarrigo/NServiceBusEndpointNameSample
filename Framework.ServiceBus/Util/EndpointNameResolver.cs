using System;
using System.Reflection;
using NServiceBus;

namespace Framework.ServiceBus.Util
{
    public class EndpointNameResolver
    {
        private static readonly EndpointNameResolver _Instance = new EndpointNameResolver();
        public static EndpointNameResolver Instance
        {
            get { return _Instance; }
        }
        public string Resolve(string endpointName)
        {
            return endpointName;
        }

        public string Resolve(Type endpointConfigurationType)
        {
            string endpointName = endpointConfigurationType.Namespace;
            var nameAttrib = endpointConfigurationType.GetCustomAttribute<EndpointNameAttribute>();
            if (nameAttrib != null)
                endpointName = nameAttrib.Name;
            return endpointName;
        }

        public string Resolve<T>()
        {
            return Resolve(typeof(T));
        }

        public string Resolve(IConfigureThisEndpoint endpointConfiguration)
        {
            return Resolve(endpointConfiguration.GetType());
        }
    }
}