namespace NotificationWebAPI.Entities
{
    public class AzureServiceBusConfiguration
    {
        /// <summary>
        /// Gets or sets the ServiceBusConnectionString.
        /// </summary>
        /// <value>The mServiceBusConnectionString.</value>
        public string ServiceBusConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the queuename.
        /// </summary>
        /// <value>The queuename.</value>
        public string QueueName  { get; set; }

        /// <summary>
        /// Gets or sets the maximum back off.
        /// </summary>
        /// <value>The maximum back off.</value>
        public int MaxBackOff { get; set; }

        /// <summary>
        /// Gets or sets the maximum retry count.
        /// </summary>
        /// <value>The maximum retry count.</value>
        public int MaxRetryCount { get; set; }

        /// <summary>
        /// Gets or sets the minimum back off.
        /// </summary>
        /// <value>The minimum back off.</value>
        public float MinBackOff { get; set; }

        /// <summary>
        /// Gets or sets the receive mode.
        /// </summary>
        /// <value>The receive mode.</value>
        public string ReceiveMode { get; set; }

        /// <summary>
        /// Gets or sets the time to live.
        /// </summary>
        /// <value>The time to live.</value>
        public string TimeToLive { get; set; }
    }
}
