using ExpenseTracker.Infrastructure;
using ExpenseTracker.Infrastructure.Data;
using ExpenseTracker.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await MainAsync();

static async Task MainAsync()
{
    var builder = WebApplication.CreateBuilder();

    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers(); 
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowDevClient", policy =>
        {
            policy.WithOrigins("http://localhost:5173") 
                .AllowAnyHeader()
                .AllowAnyMethod(); 
        });
    });
    
    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ExpenseTrackerDbContext>();
        await DbSeeder.SeedAsync(context);
    }
    
    app.UseCors("AllowDevClient");

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers(); 
    app.MapGet("/", () => "Expense Tracker is running");

    app.Run();
}