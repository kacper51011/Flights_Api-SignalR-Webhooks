using Flights.Application.Commands.CreateFlight;
using Flights.Domain.Interfaces;
using Flights.Infrastructure.Repositories;
using MassTransit;
using Quartz;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMediatR((m) => m.RegisterServicesFromAssemblyContaining(typeof(CreateFlightCommand)));

builder.Services.AddMassTransit(cfg =>
{
    cfg.SetDefaultEndpointNameFormatter();

    cfg.UsingRabbitMq((context, configuration) =>
    {

        configuration.Host(builder.Configuration["RabbitMQ:HostName"], "/", h =>
        {

            h.Username(builder.Configuration["RabbitMQ:UserName"]);
            h.Password(builder.Configuration["RabbitMQ:Password"]);
        });

        configuration.ConfigureEndpoints(context);
    });
});

builder.Services.AddQuartz(c =>
{

});

builder.Services.AddTransient<IFlightsRepository, FlightsRepository>();
builder.Services.AddTransient<IWebhookSubscriptionsRepository, WebhookSubscriptionsRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cfg =>
{
    var commentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var commentsFullPath = Path.Combine(AppContext.BaseDirectory, commentFile);

    cfg.IncludeXmlComments(commentsFullPath);

    cfg.SwaggerDoc("FlightsApiSpecification", new()
    {
        Title = "Flights Api",
        Version = "1",
        Contact = new()
        {
            Name = "Kacper Tylec",
            Email = "kacper.tylec1999@gmail.com",
            Url = new Uri("https://github.com/kacper51011")
        },
        Description = "<h3>Flights Api:<h3/>" +
        "<h5>Created as a basic simulation of api which provides real-time changing data of flight plans.<br/>" +
        "<h5>Created with purpose of learning how to use webhooks as a provider and consumer of them with other api" +
        "<ul>" +
        "<li>Generate Random flights and manipulate the data through background jobs (quartz)</li>" +
        "<li>Allows users to create their own flights </li>" +
        "<li> </li>" +
        " <ul/>" +
        ""
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setup =>
    {
        setup.SwaggerEndpoint("/swagger/FlightsApiSpecification/swagger.json", "Flights Api");
        setup.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
