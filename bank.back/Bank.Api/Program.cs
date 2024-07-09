using Autofac;
using Autofac.Extensions.DependencyInjection;
using Bank.Application.Common.Jwt;
using Bank.Application.Modules;
using Bank.Back.Modules;

var applicationBuilder = WebApplication.CreateBuilder(args);
applicationBuilder.Services.Configure<JwtOptions>(applicationBuilder.Configuration.GetSection("Jwt"));

applicationBuilder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(containerBuilder =>
    {
        var configuration = applicationBuilder.Configuration;
        containerBuilder.RegisterModule(new ApiModule(configuration));
        containerBuilder.RegisterModule(new ApplicationModule());
    }
));

applicationBuilder.Services.AddControllers();
applicationBuilder.Services.AddEndpointsApiExplorer();
applicationBuilder.Services.AddSwaggerGen();

applicationBuilder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(b =>
    {
        b.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = applicationBuilder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseRouting();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(_ => true)
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.Run();