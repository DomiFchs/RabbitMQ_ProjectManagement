using ProjectManagement_MOM.Entities;
using ProjectManagement_MOM.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


builder.Services.Configure<RabbitCfg>(cfg => {
    cfg.Host = builder.Configuration.GetSection("RabbitMq:Host").Value!;
    cfg.Port = int.Parse(builder.Configuration.GetSection("RabbitMq:Port").Value!);
    cfg.ProjectExchangeName = builder.Configuration.GetSection("RabbitMq:ProjectExchangeName").Value!;
});

builder.Services.AddScoped<RabbitMqService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();






app.Run();