using Microsoft.EntityFrameworkCore;
using Storage.Services;
using Storage.Data;


namespace Storage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //This method looks at the appsettings.json file.
            //In this case it contains logging information and very importantly our
            //SQL Server connection string.
            //It makes sure that the internal web server Kestrel is included
            //and it will set up IIS integration.
            //It sees to that the wwwroot  container will host static files (images,JS and CSS).
            var builder = WebApplication.CreateBuilder(args);


            //The Builder also gives access to the Services collection.
            builder.Services.AddDbContext<StorageContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("StorageContext")
                ?? throw new InvalidOperationException("Connection string 'StorageContext' not found.")));

            //This method makes this an MVC application. (Add services to the container). 
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<ICategorySelectListService, CategorySelectListService>();


            //Builds the app.
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseDeveloperExceptionPage();

            /// Adds a middleware to the pipeline that will catch exceptions, log them, reset the request path, and re-execute the request.
            /// The request will not be re-executed if the response has already started.
            app.UseExceptionHandler("/Home/Error");

            // The default HSTS value is 30 days. 
            //You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

            //HTTP Strict Transport Security (HSTS) is a policy mechanism that helps
            //to protect websites against man-in-the-middle attacks such as protocol downgrade attacks
            //and cookie hijacking.
            app.UseHsts();

            // Adds middleware for redirecting HTTP Requests to HTTPS.
            app.UseHttpsRedirection();

            // Enables static file serving for the current request path
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Products}/{action=Index}/{id?}");

            app.Run();
        }
    }
}