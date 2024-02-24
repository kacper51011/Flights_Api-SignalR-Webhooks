using Flights.Application.BackgroundJobs;
using Flights.Application.Commands.CreateFlight;
using Flights.Application.Consumers;
using Flights.Application.WebhookService;
using Flights.Domain.Interfaces;
using Flights.Infrastructure.Db;
using Flights.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IFlightsRepository, FlightsRepository>();
builder.Services.AddTransient<IWebhookSubscriptionsRepository, WebhookSubscriptionsRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<IWebhookService, WebhookService>();


builder.Services.AddMediatR((m) => m.RegisterServicesFromAssemblyContaining(typeof(CreateFlightCommand)));


//builder.Services.AddQuartz(c =>
//{
//    //c.ScheduleJob<SendWebhooksJob>(trigger =>
//    //{
//    //    trigger.WithSimpleSchedule(x => x.WithIntervalInSeconds(30));
//    //});

//    c.ScheduleJob<TestJob>(trigger =>
//    {
//        trigger.WithSimpleSchedule(x => x.WithIntervalInSeconds(30));
//    });
//    //var SendWebhookJobKey = JobKey.Create(nameof(SendWebhooksJob));

//    //c
//    //.AddJob<SendWebhooksJob>(SendWebhookJobKey)
//    //.AddTrigger(trigger =>
//    //    trigger.WithIdentity("webhook trigger").ForJob(SendWebhookJobKey)
//    //    .WithSimpleSchedule(schedule => schedule.WithInterval(TimeSpan.FromSeconds(30)).RepeatForever())
//    //    );
//});

////builder.Services.AddQuartzHostedService(options =>
////{
////    // when shutting down we want jobs to complete gracefully
////    options.WaitForJobsToComplete = true;
////});
///

builder.Services.AddQuartz(config =>
{
    var testJobKey = new JobKey(nameof(WebhookJob));

    config
    .AddJob<WebhookJob>(testJobKey)
    .AddTrigger(trigger =>
        trigger.WithIdentity("webhook trigger").ForJob(testJobKey)
        .WithSimpleSchedule(schedule => schedule.WithInterval(TimeSpan.FromSeconds(30)).RepeatForever())
        );
});



builder.Services.AddQuartzHostedService(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});

builder.Services.AddMassTransit(cfg =>
{


    cfg.SetDefaultEndpointNameFormatter();

    cfg.AddConsumer<FlightAddedOrChangedConsumer>();



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


var server = "flightsdb";
var user = "SA";
var database = "flights";
var password = "RandomPassword123";

builder.Services.AddDbContext<ApplicationDbContext>(c =>
{
    c.UseSqlServer($"Data Source={server};Initial Catalog={database};User ID={user};Password={password}; TrustServerCertificate=True;Encrypt=True;MultiSubnetFailover=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("Flights.Api"));
});



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
        Description =
        "<h5>Created as a basic simulation of api which provides real-time changing data of flight plans." +
        "<h5>Created with purpose of learning how to use webhooks as a provider and consumer of them with other api" +
        "<h5>The API revolves mainly around:</h5>" +
        "<ul>" +
        "<li>Generating Random flights and manipulating the data through background jobs (quartz)</li>" +
        "<li>Allowing users to create their own flights </li>" +
        " <ul/>" +
        ""
    });
});

builder.Services.AddHttpClient();

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
