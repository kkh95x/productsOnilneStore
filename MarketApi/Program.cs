
using Data;
using MarketApi.Services;
using Microsoft.EntityFrameworkCore;

namespace MarketApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<ICarsRepostory, CarsRepostory>();
            builder.Services.AddScoped<ICustomerRepostory, CustomerRepostory>();
            builder.Services.AddScoped<ISalesRepository, SalesRepository>();
            builder.Services.AddScoped<IPartsRepository, PartsRepository>();
            builder.Services.AddScoped<ISuppliersRepository, SuppliersRepository>();


            builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);


            builder.Services.AddDbContext<MyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration["ConnectionStrings:CarsStroeDb"]);
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();
     
          

            app.MapControllers();

            app.Run();
        }

    }

    
}