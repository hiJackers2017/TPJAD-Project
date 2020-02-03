using AuthorizationApi.Domain;
using AuthorizationApi.Domain.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
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
            services.AddDbContext<AuthorizationApiContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AuthorizationApiContext")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core API", Version = "v1" });
                string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "AuthorizationApi.xml");
                //c.IncludeXmlComments(xmlPath);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Please enter into this field the word 'Bearer' followed by a space and then JWT",
                    Name = "Authorization",

                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, new[] { "readAccess", "writeAccess" } } });


                // configure jwt authentication
                //        JwtToken jwtToken = new JwtToken();
                //        services.AddAuthentication(x =>
                //        {
                //            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //        }).AddJwtBearer(x =>
                //        {
                //            x.RequireHttpsMetadata = false;
                //            x.SaveToken = true;
                //            x.TokenValidationParameters = jwtToken.GetTokenValidationParameters();
                //            x.Events = new JwtBearerEvents
                //            {
                //                OnMessageReceived = context =>
                //                {
                //                    string value = context.Request.Headers["Authorization"];
                //                    if (value != null && value.Contains("Bearer"))
                //                    {
                //                        string[] aux = value.Split(" ");
                //                        value = aux[1];
                //                    }
                //                    context.Token = value;

                //                    return Task.CompletedTask;
                //                }
                //            };
                //        });
                //    });

                //    services.AddAuthorization(options =>
                //    {
                //        options.AddPolicy("OnlyAdmins", policy =>
                //            policy.Requirements.Add(new RoleRequirement("Administrator")));
                //        options.AddPolicy("OnlyModerators", policy =>
                //            policy.Requirements.Add(new RoleRequirement("Moderator")));
                //        options.AddPolicy("ModeratorsAndMentors", policy =>
                //            policy.Requirements.Add(new RoleRequirement("Administrator,Moderator")));
                //        options.AddPolicy("OnlyStudents", policy =>
                //            policy.Requirements.Add(new RoleRequirement("BasicUser")));
                //    });
                //} 
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

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
