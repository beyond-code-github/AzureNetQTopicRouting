namespace Subscriber
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
                 bus.Subscribe<TextMessage>(HandleTextMessage, o => o.WithTopic("topic.one").WithTopic("topic.two"));
                 bus.Subscribe<TextMessage>(HandleAnotherMessage, o => o.WithTopic("topic.*"));
                
                 bus.Subscribe<TextMessage>(HandleUnfilteredMessage);

                Console.WriteLine("Listening for messages. Hit <return> to quit.");
                Console.ReadLine();
            }
        }

        static void HandleUnfilteredMessage(TextMessage textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got unfiltered message: {0}", textMessage.Text);
            Console.ResetColor();
        }

        static void HandleTextMessage(TextMessage textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got message: {0}", textMessage.Text);
            Console.ResetColor();
        }

        static void HandleAnotherMessage(TextMessage textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got another message: {0}", textMessage.Text);
            Console.ResetColor();
        }
    }
}
