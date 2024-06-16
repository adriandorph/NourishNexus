using server.Config;

var builder = WebApplication.CreateBuilder(args);
Startup startup = new(builder.Configuration);
startup.Start();
