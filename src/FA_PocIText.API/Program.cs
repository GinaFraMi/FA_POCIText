using FastEndpoints;
using FastEndpoints.Swagger;
using Common;

DotEnv.Load();

var bld = WebApplication.CreateBuilder();
bld.Services
   .AddFastEndpoints()
   .SwaggerDocument();

var app = bld.Build();
app.UseFastEndpoints()
   .UseSwaggerGen();
app.Run();
