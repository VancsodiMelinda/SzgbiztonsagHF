using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NinjaStore.BLL;
using NinjaStore.DAL;
using NinjaStore.DAL.Models;
using NinjaStore.Parser.Services;

namespace NinjaStore
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
			services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("NinjaStoreDB"));
			});

			services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<StoreContext>()
				.AddDefaultTokenProviders();

			services.AddRazorPages();
			services.AddTransient<IStoreLogic, StoreLogic>();
			services.AddTransient<IParserService, ParserService>();

			//LOGGER CODE
			services.AddSingleton<ILoggerRepo, LoggerRepo>();
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
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			//LOGGER CODE
			app.UseMiddleware<Logger>();

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
	}
}
