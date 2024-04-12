using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using ProjectManagement_MOM.Entities;
using RabbitMQ.Client;
using Shared.Entities.Projects.AProjects;

namespace ProjectManagement_MOM.Services;

public class RabbitMqService : IDisposable{
    
    private readonly ILogger<RabbitMqService> _logger;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly RabbitCfg _config;
    
    public RabbitMqService(ILogger<RabbitMqService> logger, IOptions<RabbitCfg> options) {
        _logger = logger;
        _config = options.Value;
        var factory = new ConnectionFactory { HostName = _config.Host , Port = _config.Port};
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        
        _channel.ExchangeDeclare(_config.ProjectExchangeName, ExchangeType.Topic, true);
    }
    
    public void PublishProject(AProjectCreated project) {
        
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(project));
        //var facility = project.Facility.Name.ToLower();
        var facility = "architecture";
        _channel.BasicPublish(_config.ProjectExchangeName, $"projects.{facility}", null, body);
        _logger.LogInformation($"Project {project.Title} published to projects.{facility}");
    }

    public void Dispose() {
        _connection.Dispose();
        _channel.Dispose();
    }
}