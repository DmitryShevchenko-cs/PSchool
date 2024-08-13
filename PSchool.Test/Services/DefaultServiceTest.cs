using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PSchool.DAL;
using PSchool.Web;

namespace PSchool.Test.Services;

public class DefaultServiceTest<TService> where TService : class
{
    protected IServiceProvider ServiceProvider;
    protected IServiceCollection ServiceCollection;

    public virtual TService Service => ServiceProvider.GetRequiredService<TService>();

    public IConfiguration Configuration;

    protected virtual void SetUpAdditionalDependencies(IServiceCollection services)
    {
        services.AddScoped<TService>();
        services.AddAutoMapper(typeof(Startup));
    }

    [SetUp]
    public virtual void SetUp()
    {
        ServiceCollection = new ServiceCollection();
        ServiceCollection.AddDbContext<PSchoolDbContext>(options =>
            options.UseInMemoryDatabase("TestPSchoolDb"));
        ServiceCollection.AddLogging();
            
        SetUpAdditionalDependencies(ServiceCollection);

        var rootServiceProvider = ServiceCollection.BuildServiceProvider(new ServiceProviderOptions()
            { ValidateOnBuild = true, ValidateScopes = true });

        var spScope = rootServiceProvider.CreateScope();
        ServiceProvider = spScope.ServiceProvider;
    }
}

public abstract class DefaultServiceTest<TServiceInterface, TService> : DefaultServiceTest<TService>
    where TService : class, TServiceInterface where TServiceInterface : class
{
    public override TService Service => ServiceProvider.GetRequiredService<TService>();

    protected override void SetUpAdditionalDependencies(IServiceCollection services)
    {
        services.AddScoped<TServiceInterface, TService>();
        base.SetUpAdditionalDependencies(services);
    }
}