using DotNetOpenAuth.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using NotificationWebAPI.Entities;
using System.Text;

namespace NotificationWebAPI.Controllers
{
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly IMessageSendingFactory _messageFactory;

        public NotificationController(ILogger<NotificationController> logger, IMessageSendingFactory messageFactory)
        {
            _logger = logger;
            _messageFactory = messageFactory;
        }


        /// <summary>
        /// Send Notifiaction Message to Service Bus Queue
        /// service bus Message is then process by the Notification Logic App ( as setup in demo doc) to sent it as Email to the recipen specified in the message json
        /// Eg. or Sample Input Notification Mesage Json format - 
        /// { "MessageBody": "{\"EmailTo\":\"prakhour@microsoft.com\" , \"EmailCc\":\"prakhour@microsoft.com\" , \"MessageSubject\":\"Hello , Test Notification mail\" ,\"MessageBody\":\"Hello ,This is a test Notification mail\" }", " 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("SendNotificationToServiceBusQueue")]
        public IActionResult SendNotificationToServiceBusQueue(string message)
        {
            // Write code to add message in the service bus queue
            this._messageFactory.SendMessageToServiceBusQueueAsync(message).GetAwaiter().GetResult();
            this._logger.LogInformation($"Sending message: " + message);

            return Ok();
        }


        /// <summary>
        /// Send Notifiaction Message to Service Bus Topic 
        /// service bus Message is then process by the Notification Logic App ( as setup in demo doc) to sent it as Email to the recipen specified in the message json
        /// Eg. or Sample Input Notification Mesage Json format - 
        /// { "MessageBody": "{\"EmailTo\":\"prakhour@microsoft.com\" , \"EmailCc\":\"prakhour@microsoft.com\" , \"MessageSubject\":\"Hello , Test Notification mail\" ,\"MessageBody\":\"Hello ,This is a test Notification mail\" }", " 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("SendNotificationToServiceBusTopic")]
        public IActionResult SendNotificationToServiceBusTopic(string message)
        {
            // Write code to add message in the service bus queue
            this._messageFactory.SendMessageToServiceBusTopicAsync(message).GetAwaiter().GetResult();
            this._logger.LogInformation($"Sending message: " + message);

            return Ok();
        }


    }
}
