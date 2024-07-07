using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Bank.Application.Common.Interfaces;
using Bank.Application.Common.Mappings;
using Bank.Application.Services;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;

namespace Bank.Application.Modules;

public sealed class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAutoMapper(config => { config.AddProfile(new AssemblyMappingProfile(ThisAssembly)); });

        builder.RegisterType<PasswordHasher>()
            .As<IPasswordHasher>()
            .AsSelf();

        builder.RegisterType<TokenService>()
            .As<ITokenService>()
            .AsSelf();

        builder.RegisterMediatR(MediatRConfigurationBuilder
            .Create(ThisAssembly)
            .WithAllOpenGenericHandlerTypesRegistered()
            .WithRegistrationScope(RegistrationScope.Scoped)
            .Build());
    }
}