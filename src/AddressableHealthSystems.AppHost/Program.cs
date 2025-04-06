var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Gateway>("gateway");

builder.AddProject<Projects.Client>("client");

builder.AddProject<Projects.DirectoryService>("directoryservice");

builder.AddProject<Projects.DiscoveryService>("discoveryservice");

builder.AddProject<Projects.MessagingService>("messagingservice");

builder.Build().Run();
