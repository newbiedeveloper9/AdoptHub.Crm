var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AdoptHub_Crm_Api>("adopthub-crm-api");

builder.Build().Run();
