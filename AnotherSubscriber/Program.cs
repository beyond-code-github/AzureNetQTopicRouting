namespace AnotherSubscriber
{
    using System;
    using AzureNetQ;
    using Messages;

    class Program
    {
        static void Main(string[] args)
        {
            const string connectionstring =
                "Endpoint=sb://servicebus/ServiceBusDefaultNamespace;StsEndpoint=https://servicebus:10355/ServiceBusDefaultNamespace;RuntimePort=10354;ManagementPort=10355";

            using (var bus = AzureBusFactory.CreateBus(connectionstring))
            {
                bus.Subscribe<TextMessage>(HandleAnotherMessage);
                    
                Console.WriteLine("Listening for more messages. Hit <return> to quit.");
                Console.ReadLine();
            }
        }

        static void HandleAnotherMessage(TextMessage textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got another message: {0}", textMessage.Text);
            Console.ResetColor();
        }
    }
}
