using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopApi.Repository;
using EshopApi.Models;
using EshopApi.Repositories;
using EshopApi.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;

namespace EshopApi
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
            services.AddControllers();
            services.AddDbContext<EShopApi_Context>(
                options =>
                {
                    // options.UseSqlServer("Server=localhost;Database= EShopAPI;User=sa;Password=Docker@123;");
                    options.UseSqlServer("Data Source =.; Initial Catalog = EShopApi; Integrated Security = true");
                });
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ISalesPersonRepository, SalesPersonRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "TestSwagger",
                    Version = "v2",
                    Description = "Sample service",
                    
                });
            });

            services.AddResponseCaching();
            services.AddMemoryCache();


            //JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    //enable tokan valiation
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        //validatin in server side 
                        ValidateIssuer = true,
                        //validation in client side 
                        ValidateAudience = false,
                        //expire time of tokan 
                        ValidateLifetime = true,
                        //validate the tokan , اعتبار سنجی همون امضا
                        ValidateIssuerSigningKey = true,
                        //set servers that aprove to validate 
                        ValidIssuer = "https://localhost:44350",
                        //signing key
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AriyanProjectKeyValue"))

                    };
                });

            //access to use other application 
            services.AddCors(option =>
            {
                option.AddPolicy("AddCors", builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .Build();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v2/swagger.json", "TestSwagger");
            });
            app.UseResponseCaching();

            app.UseCors("AddCors");
            app.UseAuthentication();


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}