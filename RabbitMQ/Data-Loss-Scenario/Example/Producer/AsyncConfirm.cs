using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Producer
{
    public class AsyncConfirm
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "127.0.0.1";
            factory.UserName = "yy";
            factory.Password = "hello!";
            factory.VirtualHost = "/";
            factory.Port = 5672;

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare("orders", exclusive: false);
            // start up Confirm model
            channel.ConfirmSelect();
            
            try
            {
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                List<ulong> no=new List<ulong>();
                // send 
                for (int i = 0; i < 1000; i++)
                {
                    no.Add(channel.NextPublishSeqNo);
                    channel.BasicPublish(exchange: "", routingKey: "orders", body: body);
                }

                // 消息确认后，删除已确认消息
                channel.BasicAcks += (sender, tag) =>
                {
                    if (tag.Multiple)
                    {
                        no.RemoveAll(x => x == tag.DeliveryTag);
                    }
                    else
                    {
                        no.Remove(tag.DeliveryTag);
                    }
                };

                // 消息发送失败
                channel.BasicNacks += (sender, tag) =>
                {
                    if (tag.Multiple)
                    {
                        no.RemoveAll(x => x == tag.DeliveryTag);
                    }
                    else
                    {
                        no.Remove(tag.DeliveryTag);
                    }
                };
                
            }
            catch (Exception e)
            {

            }
            finally
            {
                channel.Dispose();
            }
        }
    }
}
