using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TradeBook.Data;
using TradeBook.Data.Core;
using TradeBook.Services;

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

      services.AddSingleton<IDbContextOptions>(
        sp => sp.GetRequiredService<IOptions<DbContextOptions>>().Value
      );

      services.AddSingleton<TradeBookContext>();

      services.AddSingleton<TradeService>();
      services.AddSingleton<TradeCategoriesService>();

      services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
        app.UseDeveloperExceptionPage();

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
