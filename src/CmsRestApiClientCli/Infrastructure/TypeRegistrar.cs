using System;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace CmsRestApiClientCli.Infrastructure;

public sealed class TypeRegistrar : ITypeRegistrar
{
    private readonly IServiceCollection builder;

    public TypeRegistrar(IServiceCollection builder)
    {
        this.builder = builder;
    }

    public ITypeResolver Build()
    {
        return new TypeResolver(this.builder.BuildServiceProvider());
    }

    public void Register(Type service, Type implementation)
    {
        this.builder.AddSingleton(service, implementation);
    }

    public void RegisterInstance(Type service, object implementation)
    {
        this.builder.AddSingleton(service, implementation);
    }

    public void RegisterLazy(Type service, Func<object> func)
    {
        if (func is null)
        {
            throw new ArgumentNullException(nameof(func));
        }

        this.builder.AddSingleton(service, (provider) => func());
    }
}
