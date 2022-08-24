using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Producer
{
    public class Transaction
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
            // 开启实物
            channel.TxSelect();
            try
            {
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                for (int i = 0; i < 10; i++)
                {
                    channel.BasicPublish(exchange: "", routingKey: "orders", body: body);
                }
                //throw new Exception("test");
                channel.TxCommit();
            }
            catch (Exception e)
            {
                channel.TxRollback();
            }
            finally
            {
                channel.Dispose();
            }

        }
    }
}
