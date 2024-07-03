﻿// <copyright file="MessageSendingFactory.cs" company="Microsoft">
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
//  THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR
//  OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//  ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//  OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

    using Microsoft.Azure.ServiceBus;
    /// <summary>
    /// MessageSendingFactory
    /// </summary>
    
    public interface IMessageSendingFactory
    {
        public QueueClient GetSBQueueClient();

        public Task SendMessageToServiceBusQueueAsync(string messageBody);

        public Task SendMessageToServiceBusTopicAsync(string messageBody);

}
