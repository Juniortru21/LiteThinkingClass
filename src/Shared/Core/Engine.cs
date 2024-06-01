using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

#nullable disable
namespace Shared.Core;
public class Engine : IEngine
{
    public virtual IServiceProvider ServiceProvider { get; protected set; }

    public T Resolve<T>(IServiceScope scope = null)
        where T : class
    {
        return (T)this.Resolve(typeof(T), scope);
    }

    public object Resolve(Type type, IServiceScope scope = null)
    {
        return this.GetServiceProvider(scope)?.GetService(type);
    }

    public void Configure(IServiceProvider applicationServices)
    {
        this.ServiceProvider = applicationServices;
    }

    protected IServiceProvider GetServiceProvider(IServiceScope scope = null)
    {
        if (scope == null)
        {
            var accessor = this.ServiceProvider?.GetService<IHttpContextAccessor>();
            var context = accessor?.HttpContext;
            return context?.RequestServices ?? this.ServiceProvider;
        }

        return scope.ServiceProvider;
    }
}
