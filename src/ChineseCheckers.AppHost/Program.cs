var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.ChineseCheckers_ApiService>("apiservice");

builder.AddProject<Projects.ChineseCheckers_Web>("webfrontend")
    .WithReference(apiService);

builder.Build().Run();