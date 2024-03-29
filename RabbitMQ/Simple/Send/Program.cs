﻿// See https://aka.ms/new-console-template for more information

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
        //4. 申明队列
        channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
        //5. 构建byte消息数据包
        string message = args.Length > 0 ? args[0] : "Hello RabbitMQ!";
        var body = Encoding.UTF8.GetBytes(message);
        //6. 发送数据包
        channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
        Console.WriteLine(" [x] Sent {0} ", message);
    }
}