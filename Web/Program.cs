using Global.Settings;
using Serilog;
using Serilog.Events;
using Services;
using Services.Automapper;
using Services.Interfaces;
using Web.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add third party libs
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Add filters
builder.Services.AddMvc(options =>
        {
          Log.Debug("Adding MVC options");
          options.Filters.Add(new ExceptionFilter(builder.Environment));
        });

//TODO: Needs to configure:
// - the file name Patterns
// - the Error patterns
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("serilog/logs", LogEventLevel.Information)
    .CreateLogger();
Log.Information("Starting up serilog.");


// Add WebReader Buiness logic services
builder.Services.AddScoped<IGMailApiService, GmailApiService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Add settings
ADAuthenticationSettings adSettings = new ADAuthenticationSettings();
builder.Configuration.GetSection("ADAuthKeys").Bind(adSettings);
builder.Services.AddSingleton(adSettings);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
// app.UseAuthorization();

app.MapControllers();
app.Run();
