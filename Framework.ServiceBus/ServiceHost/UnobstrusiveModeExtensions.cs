using NServiceBus;

namespace Framework.ServiceBus.ServiceHost
{
    public static class UnobstrusiveModeExtensions
    {
        public static Configure ConfigureUnobstrusiveMode(this Configure configure)
        {
            // by convention, if the assembly name that contains ".Contract" and  ends in ".Messages.Events", treat the types in the assembly as Events for nServiceBus pub/sub
            var config = configure.DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains(".Contract.") && t.Namespace.EndsWith(".Messages.Events"))
                // by convention, if the assembly name that contains ".Contract" and  ends in ".Messages.Commands", treat the types in the assembly as Commands for nServiceBus routing
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.Contains(".Contract.") && t.Namespace.EndsWith(".Messages.Commands"));

            return config;
        }
    }
}
