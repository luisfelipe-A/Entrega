using Microsoft.EntityFrameworkCore;
using WizarStore_API.Models;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WizardStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WizardStoreContext") ?? throw new InvalidOperationException("Connection string 'WizardStoreContext' not found.")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<WizarStoreContext>(opt =>
    opt.UseInMemoryDatabase("ItemList"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
