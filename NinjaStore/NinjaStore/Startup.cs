using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequiredLength = 12;
				options.Password.RequiredUniqueChars = 1;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireDigit = true;
			});

			/* ZAP */
			services.AddMvc(options =>
			{
				options.CacheProfiles.Add("Default30",
					new CacheProfile()
					{
						Duration = 30
					});
				options.CacheProfiles.Add("NoCache",
					new CacheProfile()
					{
						Location = ResponseCacheLocation.None,
						Duration = 0
					});

			}).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
			/* ZAP END */

			services.AddRazorPages();
			services.AddTransient<IStoreLogic, StoreLogic>();
			services.AddTransient<IParserService, ParserService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(
			IApplicationBuilder app,
			IWebHostEnvironment env,
			UserManager<User> userManager,
			RoleManager<IdentityRole> roleManager,
			ILoggerFactory loggerFactory)
		{
		
			//LOGGER
			loggerFactory.AddFile("Logs/Ninjas-{Date}.log");

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

			/* ZAP */
			app.Use((context, next) =>
			{
				context.Response.GetTypedHeaders().CacheControl =
					new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
					{
						MustRevalidate = true,
						NoCache = true,
						NoStore = true,

					};

				context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
				context.Response.Headers.Add("Content-Security-Policy", "default-src 'none'; script-src 'self'; connect-src 'self'; img-src 'self'; style-src 'self';base-uri 'self';form-action 'self';frame-ancestors 'none';");

				return next.Invoke();
			});
			/* ZAP END */

			SeedIdentity(userManager, roleManager);

			/* ZAP HTTPS AND STATIC FILES */
		    app.UseHttpsRedirection();
			app.UseStaticFiles();
			/* ZAP HTTPS AND STATIC FILES END */

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});

			/* ZAP COOKIE WITHOUT HEADER */
			app.UseCookiePolicy(new CookiePolicyOptions
			{
				HttpOnly = HttpOnlyPolicy.Always
			});
			/* ZAP COOKIE WITHOUT HEADER END */
		}

		private static void SeedIdentity(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			CreateRoleIfNotExists(roleManager, Roles.ADMIN);
			CreateRoleIfNotExists(roleManager, Roles.USER);

			CreateAdminIfNotExists(userManager, "admin", "NinjAdmin_01");
		}

		private static void CreateRoleIfNotExists(RoleManager<IdentityRole> roleManager, string role)
		{
			bool roleExists = roleManager.RoleExistsAsync(role).Result;
			if (!roleExists)
			{
				roleManager.CreateAsync(new IdentityRole
				{
					Name = role,
				}).Wait();
			}
		}

		private static void CreateAdminIfNotExists(UserManager<User> userManager, string username, string password)
		{
			var user = userManager.FindByNameAsync(username).Result;
			if (user == null)
			{
				user = new User
				{
					UserName = username,
					Email = $"{username}@ninjas.com",
				};
				userManager.CreateAsync(user, password).Wait();
			}

			bool isAdmin = userManager.IsInRoleAsync(user, Roles.ADMIN).Result;
			if (!isAdmin)
			{
				userManager.AddToRoleAsync(user, Roles.ADMIN).Wait();
			}
		}
	}
}
