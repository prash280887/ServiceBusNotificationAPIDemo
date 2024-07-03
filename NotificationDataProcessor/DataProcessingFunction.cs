using System;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace NotificationDataProcessor
{

    /// <summary>
    /// A) In Service BUs Topic both the Functions :Fucntion 1 and Update report will recieve the same set of message 
    /// In azure crate servicebus namespace --> cratea topic : weather-forecast-added" ,but create 2 susbcriptions "send-email" ,"update-report"
    ///  WeatherDataConnection is onnection string : goto Azure > select topic > Sahred AccessPolicy tab    
    /// B)  In Service Bus Queue : Subsciber will be only one for each queue
    /// </summary>
    public static class DataProcessingFunction
    {
        //Subscriber 1 (multiple consumers) : THis method can be in separet  SBTopicMicroservice1 
        [FunctionName("NotificationProcessingFunction")]
        public static void Run(
            [ServiceBusTrigger("demo-notification-topic", "demo-send-email-subscription", Connection = "ServiceBusTopicConnectionString")]string mySbMsg, ILogger logger)
        {
            if (mySbMsg.Contains("2020")) throw new Exception("Cannot process for year 2020");

            logger.LogInformation($"SEND EMAIL: {mySbMsg}");
        }

        //Subscriber 2 (multiple consumers) : THis method can be in a seprate SBTopicMicroservice2 
        [FunctionName("UpdateReport")]
        public static void Run1(
       [ServiceBusTrigger("demo-notification-topic", "demo-update-report-subscription", Connection = "ServiceBusTopicConnectionString")] string mySbMsg, ILogger logger)
        {
            logger.LogInformation($"UPDATING REPORT: {mySbMsg}");
        }

        //Case : Single Subscriber 3  (single service bus queue consumer): THis method can be in a seprate SBusMicroservice 
        [FunctionName("ServiceBusTriggerFunction")]
        public static void Run2(
            [ServiceBusTrigger("demonotificationqueue", Connection = "ServiceBusConnectionString")] string myQueueItem,
            ILogger log)
        {        
           
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}

