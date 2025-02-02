using Projects;
using Template.AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

#region Postbres Db

var serverPotgsres = builder.AddPostgres("next-database-server",
    password: builder.AddParameter("PostgresPassword", secret: true)).WithDataVolume().WithPgAdmin(c => c.WithHostPort(5050));

var postgresDbNext = serverPotgsres
    .AddDatabase("nutri-solid-database");

#endregion

#region Login Solid
builder.AddProject<Migration>("nutri-solid-migration")
    .WithReference(postgresDbNext).WaitFor(postgresDbNext);

var api = builder.AddProject<Api>("nutri-solid")
        .WithReference(postgresDbNext).WaitFor(postgresDbNext); ;

#endregion



builder.Build().Run();