// <copyright file="MessageSendingFactory.cs" company="Microsoft">
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
//  THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR
//  OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//  ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//  OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

namespace NotificationWebAPI.Entities
{
    using System;
    using System.Text;
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Azure.ServiceBus.Core;

    /// <summary>
    /// MessageSendingFactory
    /// </summary>
    public class MessageSendingFactory : IMessageSendingFactory
    {
        /// <summary>
        /// The clients
        /// </summary>
 
        private readonly ILogger<MessageSendingFactory> _logger;
        private readonly AzureServiceBusConfiguration _serviceBusConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageSendingFactory" /> class.
        /// </summary>
        /// <param name="serviceBusConfiguration">The service bus configuration.</param>
        public MessageSendingFactory(ILogger<MessageSendingFactory> logger, IConfiguration configuration)
        {
            this._logger = logger;    
            this._serviceBusConfiguration = new AzureServiceBusConfiguration();
            configuration.GetSection("AzureServiceBusConfiguration").Bind(this._serviceBusConfiguration);

        }

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <returns>
        ///   <see cref="QueueClient" />
        /// </returns>
        public QueueClient GetClient()
        {
            var newclient = new QueueClient(
              _serviceBusConfiguration.ServiceBusConnectionString,
                this._serviceBusConfiguration.QueueName,
                (ReceiveMode)Enum.Parse(typeof(ReceiveMode), this._serviceBusConfiguration.ReceiveMode, true),
                new RetryExponential(TimeSpan.FromSeconds(this._serviceBusConfiguration.MinBackOff), TimeSpan.FromSeconds(this._serviceBusConfiguration.MaxBackOff), this._serviceBusConfiguration.MaxRetryCount));
             return newclient;

        }

        public async Task SendMessageAsync(string messageBody)
        {
            try
            {
                ISenderClient senderClient = this.GetClient();
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                this._logger.LogInformation($"Sending message: {messageBody}");
                await senderClient.SendAsync(message).ConfigureAwait(false);

                this._logger.LogInformation($"Message: {messageBody} sent successfully");
            }
            catch (Exception exception)
            {
                this._logger.LogError($"Error while sending message {DateTime.Now} :: Exception: {exception.Message}");
                throw;
            }
        }
    }
}
