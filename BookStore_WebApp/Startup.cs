using BussinessLayer.Interfaces;
using BussinessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_WebApp
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


            services.AddStackExchangeRedisCache(options =>
            {

                options.Configuration = "127.0.0.1:6379";
                // options.Configuration = "localhost:6379";
            });

            var cacheKey = Configuration.GetSection("redis").GetSection("cacheKey").Value;
            services.AddMemoryCache();

            services.Configure<DBSetting>(
                this.Configuration.GetSection(nameof(DBSetting)));

            services.AddSingleton<IDBSetting>(sp =>
               sp.GetRequiredService<IOptions<DBSetting>>().Value);
            var secret = this.Configuration.GetSection("JwtConfig").GetSection("SecretKey").Value;
            var key = Encoding.ASCII.GetBytes(secret);

            var secretAdmin = this.Configuration.GetSection("JwtConfig").GetSection("SecretKeyAdmin").Value;
            var keyAdmin= Encoding.ASCII.GetBytes(secretAdmin);

            var Key = new SymmetricSecurityKey(key);
            var KeyAdmin = new SymmetricSecurityKey(keyAdmin);  


            services.AddTransient<IbookstoreContext, bookstoreContext>();

            services.AddTransient<IWishListBl, WishListBl>();
            services.AddTransient<IWishListRl, WishListRl>();

            services.AddTransient<IOrderBl, OrderBl>();
            services.AddTransient<IOrderRl, OrderRl>();

            services.AddTransient<IFeedbackBl, FeedbackBl>();
            services.AddTransient<IFeedbackRl, FeedbackRl>();



            services.AddTransient<IAddressBl, AddressBl>();
            services.AddTransient<IAddressRl, AddressRl>();

            services.AddTransient<ICartBl, CartBl>();
            services.AddTransient<ICartRl, CartRl>();



            //
            services.AddTransient<IBookBl, BookBl>();
            services.AddTransient<IBookRl, BookRl>();
            //
            services.AddTransient<IAdminBL, AdminBL>();
            services.AddTransient<IAdminRL, AdminRL>();


            services.AddTransient<IUserBL, UserBL>();
            services.AddTransient<IUserRL, UserRL>();

            services.AddControllers();

            //Role based
            services.AddAuthorization(options =>
            {

                options.AddPolicy("Admin",
                    authBuilder =>
                    {
                        authBuilder.RequireRole("Admin");
                    });

            });

            services.AddAuthentication(x =>
            {
                
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuerSigningKey = true,
                   // IssuerSigningKey = new SymmetricSecurityKey(key),
                    IssuerSigningKeys = new List<SecurityKey> { Key,KeyAdmin },
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidIssuer = "localhost",
                    ValidAudience = "localhost"
                };
            });

            services.AddSwaggerGen(setup =>
            {
                //Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWI",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **ONLY** your JWT Bearer taken on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<String>()}
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
           
            loggerFactory.AddFile("log.txt");
            


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore");
            });

            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}");
            });
        }
    }
}
