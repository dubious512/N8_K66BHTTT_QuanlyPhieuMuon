using Microsoft.EntityFrameworkCore;
using CoreWeb_MVC.Models;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
namespace CoreWeb_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //DI for DbContext
            builder.Services.AddDbContext<TicketDbContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();
            builder.Services.AddControllers().AddNewtonsoftJson();
            var app = builder.Build();


			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
