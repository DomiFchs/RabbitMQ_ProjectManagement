// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Entities.Projects.AProjects;
using Shared.Enums;

const string hostName = "localhost";
const int port = 5672;
const string receiveProjectsName = "validatedProjects";

List<AProjectCreated> acceptedProjects = [];
List<AProjectCreated> declinedProjects = [];

var factory = new ConnectionFactory { HostName = hostName, Port = port };

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(receiveProjectsName, ExchangeType.Topic, true);

channel.QueueDeclare(queue: "declinedQueue", durable: true, exclusive: false, autoDelete: false);
channel.QueueDeclare(queue: "acceptedQueue", durable: true, exclusive: false, autoDelete: false);

channel.QueueBind(queue: "declinedQueue", exchange: receiveProjectsName, routingKey: "projects.declined");
channel.QueueBind(queue: "acceptedQueue", exchange: receiveProjectsName, routingKey: "projects.accepted");

Console.WriteLine("Queues initialized! Waiting for messages...");

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (_, ea) => {
    
    var body = ea.Body.ToArray();
    var createdAProjectDto = JsonSerializer.Deserialize<AProjectCreated>(Encoding.UTF8.GetString(body));
    
    if (createdAProjectDto == null) {
        Console.WriteLine("Received invalid project");
        return;
    }

    switch (createdAProjectDto.State) {
        case EProjectState.Approved:
            Console.WriteLine($"Received accepted project: {createdAProjectDto.Title}");
            acceptedProjects.Add(createdAProjectDto);
            break;
        case EProjectState.Cancelled:
            Console.WriteLine($"Received declined project: {createdAProjectDto.Title}");
            declinedProjects.Add(createdAProjectDto);
            break;
    }
};

channel.BasicConsume(queue: "declinedQueue", autoAck: true, consumer: consumer);
channel.BasicConsume(queue: "acceptedQueue", autoAck: true, consumer: consumer);

Thread.Sleep(Timeout.Infinite);


