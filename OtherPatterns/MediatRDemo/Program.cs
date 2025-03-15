using FluentValidation;
using MediatR;
using MediatRDemo.Behavior;
using MediatRDemo.Context;
using MediatRDemo.Models;
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
			builder.Services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

				// Pipeline davranýþlarýný ekleme
				cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
				cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
			});

			// FluentValidation validatörlerinin kayýt edilmesi
			builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			//// Entity Framework Core yapýlandýrmasý
			//builder.Services.AddDbContext<AppDbContext>(options =>
			//	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			//Entity Framework'ün InMemory veritabaný saðlayýcýsý kullanarak, gerçek bir SQL Server'a baðlanmadan testi yapabilirsiniz:
			// Program.cs dosyasýnda, SQL Server yerine InMemory kullanýn:
			builder.Services.AddDbContext<AppDbContext>(options =>
				  options.UseInMemoryDatabase("MediatRDemoDB"));




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




			// ... diðer app yapýlandýrma kodlarý


			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(options =>
				{
					// Swagger UI, /swagger/index.html üzerinden eriþilebilir olur.
					options.RoutePrefix = "swagger";
					options.SwaggerEndpoint("/swagger/v1/swagger.json", "API Baþlýk v1");
				});
			}


			//// Veritabanýný oluþtur ve migrate et
			// Seed Database - Örnek veri ekle
			using (IServiceScope scope = app.Services.CreateScope())
			{
				IServiceProvider services = scope.ServiceProvider;
				AppDbContext context = services.GetRequiredService<AppDbContext>();

				// Örnek verileri ekle
				if (!context.Products.Any())
				{
					context.Products.AddRange(
						new Product { Name = "Laptop", Price = 1299.99m, Stock = 15 },
						new Product { Name = "Telefon", Price = 799.99m, Stock = 25 },
						new Product { Name = "Kulaklýk", Price = 99.99m, Stock = 50 },
						new Product { Name = "Mouse", Price = 29.99m, Stock = 40 },
						new Product { Name = "Klavye", Price = 49.99m, Stock = 30 }
					);
					context.SaveChanges();
				}
			}


			// Uygulama ana sayfaya ("/") istek geldiðinde Swagger UI'a yönlendirme yapýyoruz.
			app.Use(async (context, next) =>
			{
				if (context.Request.Path == "/weatherforecast")
				{
					context.Response.Redirect("/swagger/index.html");
					return;
				}
				await next();
			});
			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
