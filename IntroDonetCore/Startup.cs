using IntroDonetCore.DAL;
using IntroDonetCore.Services.IRepository;
using IntroDonetCore.Services.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IntroDonetCore
{
    public class Startup
    {

        public Startup(IConfiguration config) {

            Configuration = config;
        }

        public IConfiguration Configuration { get; set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<IntroContext>(options =>
            {

                options.UseSqlServer(Configuration.GetConnectionString("Default"));

            });


            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();

            services.AddMvc();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
            //   endpoints.MapGet("/", async context =>
            //   {
            //       await context.Response.WriteAsync("Hello World!");
            //   }

            endpoints.MapControllerRoute("default",
               pattern: "{controller=Home}/{action=Index}/{int?}"

                );
            });


            DbInitializer.Seed(app);

        }
    }




}
