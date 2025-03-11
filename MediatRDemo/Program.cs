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



			// MediatR ve di�er servislerin kay�t edilmesi
			builder.Services.AddMediatR(cfg => {
				cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

				// Pipeline davran��lar�n� ekleme
				cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
				cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
			});

			// FluentValidation validat�rlerinin kay�t edilmesi
			builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			// Entity Framework Core yap�land�rmas�
			builder.Services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();

			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Title = "API Ba�l�k",
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
