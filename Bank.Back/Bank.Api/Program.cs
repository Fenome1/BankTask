using Autofac;
using Autofac.Extensions.DependencyInjection;
using Bank.Application.Common.Jwt;
using Bank.Back.Modules;
using Bank.Persistence.Context;

var applicationBuilder = WebApplication.CreateBuilder(args);
applicationBuilder.Services.Configure<JwtOptions>(applicationBuilder.Configuration.GetSection("Jwt"));

applicationBuilder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(containerBuilder =>
    {
        var configuration = applicationBuilder.Configuration;
        containerBuilder.RegisterModule(new ApiModule(configuration));
    }
));

applicationBuilder.Services.AddControllers();
applicationBuilder.Services.AddEndpointsApiExplorer();
applicationBuilder.Services.AddSwaggerGen();

applicationBuilder.Services.AddCors(options => options.AddPolicy("CORS", policy =>
{
    policy
        .WithMethods(
            HttpMethods.Get,
            HttpMethods.Post,
            HttpMethods.Put,
            HttpMethods.Delete)
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed(_ => true);
}));

var app = applicationBuilder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseCors("CORS");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await using var scope = app.Services.CreateAsyncScope();
await using var context = scope.ServiceProvider.GetRequiredService<BankDbContext>();
await context.Database.EnsureCreatedAsync();

app.Run();