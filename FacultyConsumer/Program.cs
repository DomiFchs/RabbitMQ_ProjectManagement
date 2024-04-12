// See https://aka.ms/new-console-template for more information

//rabbitmq factory

using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Entities.Projects.AProjects;
using Shared.Enums;

const string hostName = "localhost";
const int port = 5672;
const string receiveProjectsExName = "project-exchange";
const string sendProjectsExName = "validatedProjects";

var factory = new ConnectionFactory { HostName = hostName, Port = port };

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(receiveProjectsExName, ExchangeType.Topic, true);
channel.ExchangeDeclare(sendProjectsExName, ExchangeType.Topic, true);

channel.QueueDeclare(queue: "architectureQueue", durable: true, exclusive: false, autoDelete: false);
channel.QueueDeclare(queue: "chemistryQueue", durable: true, exclusive: false, autoDelete: false);
channel.QueueDeclare(queue: "electricalEngineeringQueue", durable: true, exclusive: false, autoDelete: false);

channel.QueueBind(queue: "architectureQueue", exchange: receiveProjectsExName, routingKey: "projects.architecture");
channel.QueueBind(queue: "chemistryQueue", exchange: receiveProjectsExName, routingKey: "projects.chemistry");
channel.QueueBind(queue: "electricalEngineeringQueue", exchange: receiveProjectsExName, routingKey: "projects.electricalengineering");

Console.WriteLine("Queues initialized! Waiting for messages...");

var consumer = new EventingBasicConsumer(channel);

bool IsValidProject() {
    return RandomNumberGenerator.GetInt32(0, 101) >= 20;
}

consumer.Received += (model, ea) => {
    try {
    
        var body = ea.Body.ToArray();
        var createdAProjectDto = JsonSerializer.Deserialize<CreateAProjectDto>(Encoding.UTF8.GetString(body));
    
        if (createdAProjectDto == null) {
            Console.WriteLine("Received invalid project");
            return;
        }
    
        Console.WriteLine($"Received project: {createdAProjectDto.Title}");
        var isValid = IsValidProject();
    
    
        if (!isValid) {
            Console.WriteLine($"Project declined: {createdAProjectDto.Title}");
            createdAProjectDto.State = EProjectState.Cancelled;
        
            var newBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(createdAProjectDto));
        
            channel.BasicPublish(sendProjectsExName, $"projects.declined", null, newBody);
        }
        else {
            Console.WriteLine($"Project approved: {createdAProjectDto.Title}");
            createdAProjectDto.State = EProjectState.Approved;
        
            var newBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(createdAProjectDto));
        
            channel.BasicPublish(sendProjectsExName, $"projects.accepted", null, newBody);
        }
    }
    catch(Exception e) {
        Console.WriteLine(e);
    }
    
};

channel.BasicConsume(queue: "architectureQueue", autoAck: true, consumer: consumer);
channel.BasicConsume(queue: "chemistryQueue", autoAck: true, consumer: consumer);
channel.BasicConsume(queue: "electricalEngineeringQueue", autoAck: true, consumer: consumer);

Thread.Sleep(Timeout.Infinite);



