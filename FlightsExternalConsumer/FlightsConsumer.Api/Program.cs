using FlightsConsumer.Application.Commands;
using FlightsConsumer.Application.Consumers.FlightCreatedConsumers;
using FlightsConsumer.Application.Consumers.FlightDeletedConsumers;
using FlightsConsumer.Application.Consumers.FlightUpdatedConsumers;
using FlightsConsumer.Application.Hubs;
using FlightsConsumer.Application.Jobs;
using FlightsConsumer.Application.Messages;
using FlightsConsumer.Application.Secrets;
using FlightsConsumer.Domain.Interfaces;
using FlightsConsumer.Infrastructure.Db;
using FlightsConsumer.Infrastructure.Repositories;
using MassTransit;
using Quartz;

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

builder.Services.AddQuartz(config =>
{
    var DeleteCompletedFlightJobKey = new JobKey(nameof(DeleteCompletedFlightsJob));

    config
    .AddJob<DeleteCompletedFlightsJob>(DeleteCompletedFlightJobKey)
    .AddTrigger(trigger =>
        trigger.WithIdentity("DeleteCompletedFlightsJob").ForJob(DeleteCompletedFlightJobKey)
        .WithSimpleSchedule(schedule => schedule.WithInterval(TimeSpan.FromMinutes(5)).RepeatForever())
        );
});

builder.Services.AddQuartzHostedService(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
    options.AwaitApplicationStarted = true;
});
builder.Services.AddMassTransit(cfg =>
{


    cfg.SetDefaultEndpointNameFormatter();

    //add two consumers
    cfg.AddConsumer<FlightCreatedDbConsumer>();
    cfg.AddConsumer<FlightCreatedSignalRConsumer>();

    cfg.AddConsumer<FlightDeletedDbConsumer>();
    cfg.AddConsumer<FlightDeletedSignalRConsumer>();

    cfg.AddConsumer<FlightUpdatedDbConsumer>();
    cfg.AddConsumer<FlightUpdatedSignalRConsumer>();



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
