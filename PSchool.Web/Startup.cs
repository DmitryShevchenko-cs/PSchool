using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using PSchool.BLL.Services;
using PSchool.BLL.Services.Interfaces;
using PSchool.DAL;
using PSchool.DAL.Repositories;
using PSchool.DAL.Repositories.Interfaces;
using PSchool.Web.Validators;

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
        services.AddControllers().AddNewtonsoftJson(opt => 
            opt.SerializerSettings.Converters.Add(new StringEnumConverter()));
        
        var connectionString = Environment.GetEnvironmentVariable("SQLSERVER_CONNECTION_STRING") ?? Configuration.GetConnectionString("ConnectionString");

        services.AddDbContext<PSchoolDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        //services di
        services.AddScoped<IParentRepository, ParentRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IParentService, ParentService>();
        
        services.AddAutoMapper(typeof(Startup));
        
        //swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "PSchool API",
                Version = "v1",
                Description = "PSchool API"
            });
        });
        
        //inject dependency of FluentValidation
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<BaseModelValidator>();
        services.AddValidatorsFromAssemblyContaining<StudentCreateValidator>();
        services.AddValidatorsFromAssemblyContaining<StudentUpdateValidator>();
        services.AddValidatorsFromAssemblyContaining<ParentCreateValidator>();
        services.AddValidatorsFromAssemblyContaining<ParentUpdateValidator>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transaction API V1");
            });
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
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
    }
}