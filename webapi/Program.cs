using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using webapi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using webapi.Models;
using webapi.Data_Access_Layer;
using webapi.Exceptions;
using AutoMapper;
using webapi.Mapping;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.
        builder.Services.AddDbContext<MyPlannerDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAutoMapper(typeof(MappingProfiles));

        var app = builder.Build();

        // Updating the database structure with the current coded database context with migrating
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<MyPlannerDbContext>();
            dbContext.Database.Migrate();
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
}
