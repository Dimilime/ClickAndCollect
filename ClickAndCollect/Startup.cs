using ClickAndCollect.DAL;
using ClickAndCollect.DAL.IDAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            
            string connectionString = Configuration.GetConnectionString("default");
            services.AddControllersWithViews();
            services.AddTransient<ICashierDAL>(cad => new CashierDAL(connectionString));
            services.AddTransient<ICustomerDAL>(cud => new CustomerDAL(connectionString));
            services.AddTransient<IOrderDAL>(od => new OrderDAL(connectionString));
            services.AddTransient<IOrderPickerDAL>(opd => new OrderPickerDAL(connectionString));
            services.AddTransient<IPersonDAL>(pd => new PersonDAL(connectionString));
            services.AddTransient<IProductDAL>(psd => new ProductDAL(connectionString));
            services.AddTransient<IShopDAL>(sd => new ShopDAL(connectionString));
            services.AddTransient<ITimeSlotDAL>(tsd => new TimeSlotDAL(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Person}/{action=Authenticate}/{id?}");
            });
        }
    }
}
