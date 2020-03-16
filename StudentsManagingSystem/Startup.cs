using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SMS.Domain.Abstract;
using SMS.Domain.Concrete;
using SMS.Domain.Entities;
using Services;

namespace StudentsManagingSystem
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {            
            string connection = Configuration.GetConnectionString("DefaultConnection");                    
            services.AddDbContext<StudentsContext>(options => options.UseSqlServer(connection));
            services.AddScoped<IRepository<Student>, StudentsRepository>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddSingleton<SortingService>();
            services.AddSingleton<UniqIdService>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}