using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Services.News;
using Stripe;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Services.ArtWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;
using ProjectStation.EmailService;
using Services.Shopping;
using ProjectStation.Stripe;
using Microsoft.AspNetCore.Http;

namespace ProjectStation
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
            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["TigDbConnection"]);
            });
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedAccount = true;
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultUI()
            .AddDefaultTokenProviders();


            services.AddDistributedMemoryCache();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                // requires using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddControllers();

            services.AddScoped<IClientRepository, SQLClientRepository>();
            services.AddScoped<INewsSnippetRepository, SQLNewsSnippetRepository>();
            services.AddScoped<IProductRepository, SQLProductRepository>();
            services.AddScoped<IArtPieceRepository, SQLArtPieceRepository>();
            services.AddScoped<IShoppingCartRepository, SQLShoppingCartRepository>();
            services.AddScoped<IOrderRepository, SQLOrderRepository>();
            services.Configure<CaptchaData>(x =>
            {
                x.PublicKey = Configuration["captcha-public"];

                x.PrivateKey = Configuration["captcha-private"];

            });
            services.AddHttpClient();

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = false;
                options.AppendTrailingSlash = true;
            });

            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
      .AddRazorPagesOptions(options =>
      {
          options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
         options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
          
      });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });


            services.Configure<DataProtectionTokenProviderOptions>(o =>
       o.TokenLifespan = TimeSpan.FromHours(3));
            services.AddSingleton<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(x =>
            {
                x.SendGridKey = Configuration["SendGridKey"];

                x.SendGridUser = Configuration["SendGridUser"];

            });

           

            services.Configure<StripeSettings>(x =>
            {
                x.SecretKey = Configuration["StripeSecretKey"];

                x.PublishableKey = "pk_test_51HDwJsAIBd311jybUxWKurSnOIr2IsNw6bL2py7507Y02v73utS0Ilwn9spNHopwg3FkNjRjrRQUe81uwuO0QIcn009kRWulWp";

            });
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute { Permanent = true });
            });
        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
               // app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            StripeConfiguration.ApiKey = Configuration["StripeSecretKey"];
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //COOKIES
             app.UseSession();

            

            app.UseStatusCodePagesWithRedirects("/Error"); //enable to allow custom error page
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
            });
        }
    }
}
