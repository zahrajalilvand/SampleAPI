
using APISample.DbContexts;
using APISample.Repositories;
using APISample.Repositories.Interfaces;
using APISample.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace APISample
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers(options =>
			{

				options.ReturnHttpNotAcceptable = true;
			})
				.AddNewtonsoftJson()
				.AddXmlDataContractSerializerFormatters();
			// Add services to the container.
			builder.Services.AddAuthorization();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#if DEBUG
			builder.Services.AddTransient<IMailService, LocalMailService>();
#else
            builder.Services.AddTransient<IMailService, CloudMailService>();
#endif

			builder.Services.AddSingleton<CitiesDataStore>();

			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.WriteTo.Console()
				.WriteTo.File("logs/cityInfo.txt", rollingInterval: RollingInterval.Day)
				.CreateLogger();

			builder.Host.UseSerilog();

			builder.Services.AddDbContext<ApplicationDBContext>(option =>
			{
				option.UseSqlServer(builder.Configuration["SQLConnectionString:Default"]);
			});

			builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();

			builder.Services.AddAuthentication("Bearer")
				.AddJwtBearer(option =>
				{
					option.TokenValidationParameters = new()
					{
						ValidateIssuer = true,
						ValidateIssuerSigningKey = true,
						ValidateAudience = true,
						ValidIssuer = builder.Configuration["Authentication:Issuer"],
						ValidAudience = builder.Configuration["Authentication:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(
							Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretKey"]))
					};
				});

			var app = builder.Build();


			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			//controller/action/id
			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


			app.Run();
		}
	}
}