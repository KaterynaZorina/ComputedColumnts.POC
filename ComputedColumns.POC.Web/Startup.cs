using System;
using ComputedColumns.POC.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComputedColumns.POC.Web
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
            services.AddMvcCore(options => options.EnableEndpointRouting = false);
            
            var connectionString = Configuration.GetConnectionString("Default");
            services.AddDbContext<ComputedColumnsDbContext>(options => options.UseSqlServer(connectionString));
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env, IServiceProvider services)
        {
            app.UseMvc();
            
            var columnsDbContext = services.GetService<ComputedColumnsDbContext>();
            //columnsDbContext.Database.Migrate();
            
            ComputedColumnsDbContext.Seed(columnsDbContext);
        }
    }
}
