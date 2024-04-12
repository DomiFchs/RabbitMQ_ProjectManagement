namespace ProjectManagement_MOM.Entities;

public class RabbitCfg {
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string ProjectExchangeName { get; set; } = null!;
}