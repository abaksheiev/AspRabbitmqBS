# Simple example of message communication bitween two systems(appliation/containers)
Communication proccssed via rabbitMQ
```
|   .dockerignore
|   .gitignore
|   AspRabbitmqBS.csproj
|   AspRabbitmqBS.sln
|   docker-compose.yml
|   Program.cs
|   README.md
|   
+---.github
|   \---workflows
|           dotnet.yml
|
+---AspRabbitmqBS.Consumer
|   |   appsettings.json
|   |   AspRabbitmqBS.Consumer.csproj
|   |   AspRabbitmqBS.Consumer.csproj.user
|   |   ConsumerService.cs
|   |   Dockerfile
|   |   Program.cs
|   |   RabbitMQConsumer.cs
|   |
|   +---Models
|   |       RabbitMQSettings.cs
|   |
|   \---Properties
|           launchSettings.json
|
\---AspRabbitmqBS.Publisher
    |   appsettings.Development.json
    |   appsettings.json
    |   AspRabbitmqBS.Publisher.csproj
    |   AspRabbitmqBS.Publisher.csproj.user
    |   Dockerfile
    |   Program.cs
    |   PublisherEndpoint.cs
    |   RabbitMQService.cs
    |
    +---Models
    |       RabbitMQSettings.cs
    |
    \---Properties
            launchSettings.json
```
