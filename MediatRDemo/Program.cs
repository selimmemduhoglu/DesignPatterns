using FluentValidation;
using MediatR;
using MediatRDemo.Behavior;
using MediatRDemo.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MediatRDemo
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);



			// MediatR ve diðer servislerin kayýt edilmesi
			builder.Services.AddMediatR(cfg => {
				cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

				// Pipeline davranýþlarýný ekleme
				cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
				cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
			});

			// FluentValidation validatörlerinin kayýt edilmesi
			builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			// Entity Framework Core yapýlandýrmasý
			builder.Services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();

			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Title = "API Baþlýk",
					Version = "v1"
				});
			});


			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			// Configure the HTTP request pipeline.

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
