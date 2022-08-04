﻿// See https://aka.ms/new-console-template for more information

using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

// 1.实例化连接工厂
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
        Console.WriteLine(" direct type red ");
        // 申明direct类型exchange
        channel.ExchangeDeclare(exchange: "directEC", type: "direct");

        channel.QueueDeclare("red", true, false, false, null);
        // 绑定队列到direct类型exchange，需指定路由键routingKey
        channel.QueueBind(queue: "red", exchange: "directEC", routingKey: "red");

        //设置prefetchCount : 1来告知RabbitMQ，在未收到消费端的消息确认时，不再分发消息，也就确保了当消费端处于忙碌状态时
        channel.BasicQos(prefetchCount: 1, prefetchSize: 0, global: false);

        //5. 构造消费者实例
        var consumer = new EventingBasicConsumer(channel);
        //6. 绑定消息接收后的事件委托
        consumer.Received += (model, ea) =>
        {
            var message = Encoding.UTF8.GetString(ea.Body.ToArray());
            Console.WriteLine(" [x] Received {0}", message);
            Thread.Sleep(9000); //模拟耗时
            Console.WriteLine(" [x] Done");

            // 7.  发送消息确认信号（手动消息确认)
            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };

        //8. 启动消费者
        //autoAck:true；自动进行消息确认，当消费端接收到消息后，就自动发送ack信号，不管消息是否正确处理完毕
        //autoAck:false；关闭自动消息确认，通过调用BasicAck方法手动进行消息确认
        channel.BasicConsume(queue: "", autoAck: false, consumer: consumer);

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}

