using AuthorizationApi.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Threading.Tasks;

namespace AuthorizationApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core API", Version = "v1" });
            });
            //services.AddSwaggerGen(x =>
            //{
            //    x.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "Core API",
            //        Description = "Swagger Core API"
            //    });
            //    string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "ELearning.Api.xml");
            //    x.IncludeXmlComments(xmlPath);
            //    x.AddSecurityDefinition("Bearer",
            //    new ApiKeyScheme
            //    {
            //        In = "header",
            //        Description = "Please enter into this field the word 'Bearer' followed by a space and then JWT",
            //        Name = "Authorization",
            //        Type = "apiKey"
            //    });
            //    x.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
            //    { "Bearer", Enumerable.Empty<string>() } });
            //});
            // configure jwt authentication
            JwtToken jwtToken = new JwtToken();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = jwtToken.GetTokenValidationParameters();
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        string value = context.Request.Headers["Authorization"];
                        if (value != null && value.Contains("Bearer"))
                        {
                            string[] aux = value.Split(" ");
                            value = aux[1];
                        }
                        context.Token = value;

                        return Task.CompletedTask;
                    }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API"));

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
