using FlightsConsumer.Application.Commands;
using FlightsConsumer.Application.Hubs;
using FlightsConsumer.Application.Secrets;
using FlightsConsumer.Domain.Interfaces;
using FlightsConsumer.Infrastructure.Db;
using FlightsConsumer.Infrastructure.Repositories;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<SecretKeys>(builder.Configuration.GetSection("SecretKeys"));

builder.Services.AddSingleton<IFlightsRepository, FlightsRepository>();
builder.Services.AddSignalR();

builder.Services.AddMediatR((m) => m.RegisterServicesFromAssemblyContaining(typeof(CreateOrUpdateFlightCommand)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddMassTransit(cfg =>
{


    cfg.SetDefaultEndpointNameFormatter();

    //add two consumers



    cfg.UsingRabbitMq((context, configuration) =>
    {

        configuration.Host("rabbitmq", "/", h =>
        {

            h.Username("guest");
            h.Password("guest");
        });

        configuration.ConfigureEndpoints(context);
    });


});

builder.Services.AddCors(c =>
{
    c.AddPolicy("reactapp", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("reactapp");

app.MapHub<FlightsHub>("/flightshub");
app.MapControllers();

app.Run();
