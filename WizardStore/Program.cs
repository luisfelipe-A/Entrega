using Microsoft.EntityFrameworkCore;
using WizardStoreAPI.Data;
using Microsoft.AspNetCore.Authentication;
using WizardStoreAPI.Handlers;
using WizardStoreAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<WizardStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WizardStoreContext") 
    ?? throw new InvalidOperationException("Connection string 'WizardStoreContext' not found.")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAuthentication("BasicAuthentication")
.AddScheme<AuthenticationSchemeOptions,BasicAuthenticationHandler>("BasicAuthentication", null);

WebApplication app;

app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

DatabaseManagementService.MigrationInitialization(app);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
