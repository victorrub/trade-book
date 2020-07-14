using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TradeBook.Data;
using TradeBook.Data.Core;
using TradeBook.Services;
using TradeBook.Services.Core;

namespace TradeBook
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<DbContextOptions>(Configuration.GetSection("TradeBookDatabase"));
      services.Configure<CacheContextOptions>(Configuration.GetSection("TradeBookCache"));

      services.AddSingleton<IDbContextOptions>(
        sp => sp.GetRequiredService<IOptions<DbContextOptions>>().Value
      );

      services.AddSingleton<ICacheContextOptions>(
        sp => sp.GetRequiredService<IOptions<CacheContextOptions>>().Value
      );

      services.AddDistributedRedisCache(options =>
      {
        options.Configuration = Configuration
          .GetSection("TradeBookCache")
          .GetValue<string>("ConnectionString");
        options.InstanceName = "TradeBook:";
      });

      services.AddSingleton<TradeBookContext>();

      services.AddSingleton<TradeService>();
      services.AddSingleton<TradeCategoriesService>();
      services.AddSingleton<CachedTradeCategories>();
      services.AddSingleton<TradeRiskService>();

      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy", builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
      });

      services.Configure<GzipCompressionProviderOptions>(options =>
      {
        options.Level = CompressionLevel.Optimal;
      })
      .AddResponseCompression(options =>
      {
        options.Providers.Add<GzipCompressionProvider>();
        options.EnableForHttps = true;
      });

      services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
        app.UseDeveloperExceptionPage();

      app.UseCors("CorsPolicy");

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
