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



			// MediatR ve di�er servislerin kay�t edilmesi
			builder.Services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

				// Pipeline davran��lar�n� ekleme
				cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
				cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
			});

			// FluentValidation validat�rlerinin kay�t edilmesi
			builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			//// Entity Framework Core yap�land�rmas�
			//builder.Services.AddDbContext<AppDbContext>(options =>
			//	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			//Entity Framework'�n InMemory veritaban� sa�lay�c�s� kullanarak, ger�ek bir SQL Server'a ba�lanmadan testi yapabilirsiniz:
			// Program.cs dosyas�nda, SQL Server yerine InMemory kullan�n:
			builder.Services.AddDbContext<AppDbContext>(options =>
				  options.UseInMemoryDatabase("MediatRDemoDB"));




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




			// ... di�er app yap�land�rma kodlar�


			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(options =>
				{
					// Swagger UI, /swagger/index.html �zerinden eri�ilebilir olur.
					options.RoutePrefix = "swagger";
					options.SwaggerEndpoint("/swagger/v1/swagger.json", "API Ba�l�k v1");
				});
			}


			//// Veritaban�n� olu�tur ve migrate et
			// Seed Database - �rnek veri ekle
			using (IServiceScope scope = app.Services.CreateScope())
			{
				IServiceProvider services = scope.ServiceProvider;
				AppDbContext context = services.GetRequiredService<AppDbContext>();

				// �rnek verileri ekle
				if (!context.Products.Any())
				{
					context.Products.AddRange(
						new Product { Name = "Laptop", Price = 1299.99m, Stock = 15 },
						new Product { Name = "Telefon", Price = 799.99m, Stock = 25 },
						new Product { Name = "Kulakl�k", Price = 99.99m, Stock = 50 },
						new Product { Name = "Mouse", Price = 29.99m, Stock = 40 },
						new Product { Name = "Klavye", Price = 49.99m, Stock = 30 }
					);
					context.SaveChanges();
				}
			}


			// Uygulama ana sayfaya ("/") istek geldi�inde Swagger UI'a y�nlendirme yap�yoruz.
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
