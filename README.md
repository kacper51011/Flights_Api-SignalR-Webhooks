## Description
This project is designed to demonstrate the implementation of 
- SignalR, 
- webhooks,
- CQRS (Command Query Responsibility Segregation),
- Clean Architecture,

in .NET web api.
The main reason of creating the project was to learn how does webhook communication works from both sides - sender and consumer in simplified scenario. I thought it was a great opportunity to use SignalR and observe how does the communication behaves. In my opinion communication between services is really interesting, especially when it is not typical request/response.

### Flights Project
Simulation of a service, which generates information about new possible flights. Except of seed data, the upcoming flights are automatically generated, modified, deleted and send to subscribers by background jobs. Of course, API enables creating own flights and manipulating them.Provides the endpoint for registering own webhook endpoint.

### Flights External Consumer
Simulation of a service coming from external system, which is a subscriber to Flights Api through webhook. Manages the data with MassTransit connected to RabbitMQ. On the one hand, it saves/change/delete the data in database, on the other hand, if the flight is today, it do the same for the signalR.

### WebUI
Simple UI written in React with typescript, which consumes the SignalR messages.
