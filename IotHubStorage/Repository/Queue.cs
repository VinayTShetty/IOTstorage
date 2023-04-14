using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;


namespace IotHubStorage.Repository
{
    public class Queue
    {
        public static string connectionString = "DefaultEndpointsProtocol=https;AccountName=vtshubstor;AccountKey=kAHuGYDMmyceoMOiokHBTKdzcYKR4HvnusqhRRov7kgXe7s6HKyFYb7IqRjGTxk7pZ0Rgcf8wrUd+ASt+0LTYQ==;EndpointSuffix=core.windows.net";

        public static async Task<bool> CreateQueue(string queueName)
        {
            if (string.IsNullOrEmpty(queueName))
            {
                throw new ArgumentNullException("Enter Queue Name: ");
            }
            try
            {
                QueueClient queue = new QueueClient(connectionString,queueName);
                await queue.CreateIfNotExistsAsync();
                if(queue.Exists())
                {
                    Console.WriteLine("Queue Created: "+queue.Name);
                    return true;
                }
                else
                {
                    Console.WriteLine("Check Azure connection and try again: ");
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static async Task InsertMessage(string queueName, string msg)
        {

            if (string.IsNullOrEmpty(queueName))
            {
                throw new ArgumentNullException("Enter Queue Name: ");
            }
            QueueClient queue = new QueueClient(connectionString, queueName);
            await queue.CreateIfNotExistsAsync();
            if (queue.Exists())
            {
                var data = queue.SendMessage(msg);
                Console.WriteLine("Message sent Successfully: ");
            }
            else
            {
                Console.WriteLine("Queue message not sent:");

            }

        }
        
        public static async Task<PeekedMessage[]> PeekMessage(string queueName)
        {
            QueueClient queue = new QueueClient(connectionString, queueName);
            PeekedMessage[] msg = null;
            if(queue.Exists())
            {
                msg = queue.PeekMessages(2);
            }
            return msg;
        }

        public static async Task UpdateMessage(string queueName,string data)
        {
            QueueClient queue = new QueueClient(connectionString, queueName);
            if(queue.Exists())
            {
                QueueMessage[] msg = queue.ReceiveMessages();
                queue.UpdateMessage(msg[0].MessageId, msg[0].PopReceipt, data, TimeSpan.FromSeconds(10));
            }
        }

        public static async Task DequeueMessage(string queueName)
        {
            QueueClient queue = new QueueClient(connectionString, queueName);
            if (queue.Exists())
            {
                QueueMessage[] msg = queue.ReceiveMessages();
                Console.WriteLine("Dequeue message" + msg[0].Body);
                queue.UpdateMessage(msg[0].MessageId, msg[0].PopReceipt);
            }
        }

        public static async Task DeleteQueue(string queueName)
        {
            QueueClient queue = new QueueClient(connectionString, queueName);
            if (queue.Exists())
            {
                await queue.DeleteAsync();
            }
        }
    }
}
