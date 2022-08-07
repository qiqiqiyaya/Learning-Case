// See https://aka.ms/new-console-template for more information

using System.Text;
using RabbitMQ.Client;

//1.1.实例化连接工厂
var factory = new ConnectionFactory();
factory.HostName = "127.0.0.1";
factory.UserName = "yy";
factory.Password = "hello!";
factory.VirtualHost = "/";
factory.Port = 5672;

//2. 建立连接
using (var connection = factory.CreateConnection())
{
    //3. 创建信道
    using (var channel = connection.CreateModel())
    {
        // 生成随机队列名称
        //var queueName = channel.QueueDeclare().QueueName;
        //Console.WriteLine(" Queue Name :", queueName);

        // 使用fanout exchange type , 指定exchange名称
        channel.ExchangeDeclare(exchange: "topicEC", type: "topic");

        //5. 构建byte消息数据包
        string message = args.Length > 0 ? args[0] : "Hello RabbitMQ!";
        var body = Encoding.UTF8.GetBytes(message);
        // 发布到指定exchange，fanout类型无需指定routingKey
        channel.BasicPublish(exchange: "topicEC", routingKey: "a.green.fast", basicProperties: null, body: body);
        Console.WriteLine(" [x] Sent {0} ", message);
    }
}