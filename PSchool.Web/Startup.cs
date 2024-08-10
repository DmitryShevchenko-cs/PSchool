using Microsoft.EntityFrameworkCore;
using PSchool.DAL;

namespace PSchool.Web;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    //service to adding dependency injection 
    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("SQLSERVER_CONNECTION_STRING") ?? Configuration.GetConnectionString("ConnectionString");

        services.AddDbContext<PSchoolDbContext>(options =>
            options.UseSqlServer(connectionString));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        
        app.UseRouting();
        app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseStaticFiles();
    }
}