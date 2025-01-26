using Projects;

var builder = DistributedApplication.CreateBuilder(args);

#region Postbres Db

var serverPotgsres = builder.AddPostgres("nutri-solid-server").WithDataVolume().WithPgAdmin(c => c.WithHostPort(5050));

var postgresDbNext = builder.ExecutionContext.IsRunMode ? serverPotgsres
    .AddDatabase("nutri-solid-database") : builder.AddConnectionString("nutri-solid-database");

#endregion

#region Login Solid
builder.AddProject<Migration>("nutri-solid-migration")
    .WithReference(postgresDbNext);

var api = builder.AddProject<Api>("nutri-solid")
        .WithReference(postgresDbNext);

#endregion



builder.Build().Run();