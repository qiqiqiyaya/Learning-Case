using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Producer
{
    public class GeneralConfirm
    {
        public GeneralConfirmResult SendMessage<T>(T message)
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
            var result = new GeneralConfirmResult();
            try
            {
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                // send 
                for (int i = 0; i < 10; i++)
                {
                    channel.BasicPublish(exchange: "", routingKey: "orders", body: body);
                    // 每发送一条消息，就等待MQ服务器的ack响应
                    if (channel.WaitForConfirms())
                    {
                        result.SuccessCount++;
                    }
                    else
                    {
                        result.FailCount++;
                    }
                }
                return result;
            }
            catch (Exception e)
            {

            }
            finally
            {
                channel.Dispose();
            }

            return result;
        }
    }
}
