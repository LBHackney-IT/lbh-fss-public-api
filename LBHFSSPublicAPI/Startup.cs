using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LBHFSSPublicAPI.V1.Gateways;
using LBHFSSPublicAPI.V1.Gateways.Interfaces;
using LBHFSSPublicAPI.V1.Infrastructure;
using LBHFSSPublicAPI.V1.UseCase;
using LBHFSSPublicAPI.V1.UseCase.Interfaces;
using LBHFSSPublicAPI.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace LBHFSSPublicAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private static List<ApiVersionDescription> _apiVersions { get; set; }
        private const string ApiName = "LBHFSSPublicAPI";

        // This method gets called by the runtime. Use this method to add services to the container.
        public static void ConfigureServices(IServiceCollection services)
        {
            services
            .AddMvc(setupAction =>
            {
                setupAction.EnableEndpointRouting = false;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;// assume that the caller wants the default version if they don't specify
                o.ApiVersionReader = new UrlSegmentApiVersionReader();// read the version number from the url segment header)
            });
            services.AddCors();
            services.AddSingleton<IApiVersionDescriptionProvider, DefaultApiVersionDescriptionProvider>();

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Token",
    new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Your Hackney API Key",
        Name = "X-Api-Key",
        Type = SecuritySchemeType.ApiKey
    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
{
new OpenApiSecurityScheme
{
Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Token" }
},
new List<string>()
}
            });

                //Looks at the APIVersionAttribute [ApiVersion("x")] on controllers and decides whether or not
                //to include it in that version of the swagger document
                //Controllers must have this [ApiVersion("x")] to be included in swagger documentation!!
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    apiDesc.TryGetMethodInfo(out var methodInfo);

                    var versions = methodInfo?
.DeclaringType?.GetCustomAttributes()
.OfType<ApiVersionAttribute>()
.SelectMany(attr => attr.Versions).ToList();

                    return versions?.Any(v => $"{v.GetFormattedApiVersion()}" == docName) ?? false;
                });

                //Get every ApiVersion attribute specified and create swagger docs for them
                foreach (var apiVersion in _apiVersions)
                {
                    var version = $"v{apiVersion.ApiVersion.ToString()}";
                    c.SwaggerDoc(version, new OpenApiInfo
                    {
                        Title = $"{ApiName}-api {version}",
                        Version = version,
                        Description = $"{ApiName} version {version}. Please check older versions for deprecated endpoints."
                    });
                }

                c.CustomSchemaIds(x => x.FullName);
                //Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (System.IO.File.Exists(xmlPath))
                    c.IncludeXmlComments(xmlPath);
            });
            ConfigureDbContext(services);
            ConfigureAddressesAPIContext(services);
            RegisterGateways(services);
            RegisterUseCases(services);
        }

        private static void ConfigureDbContext(IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "Host=127.0.0.1;Database=testdb;port=6543;username=postgres;password=mypassword;";
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddDbContext<DatabaseContext>(
            opt => opt.UseNpgsql(connectionString));
        }

        private static void ConfigureAddressesAPIContext(IServiceCollection services)
        {
            var apiBaseUrl = Environment.GetEnvironmentVariable("ADDRESSES_API_BASE_URL")
                             //?? throw new ArgumentNullException("Addresses API base url");
                             ?? "Test";

            var apiKey = Environment.GetEnvironmentVariable("ADDRESSES_API_KEY")
                         //?? throw new ArgumentNullException("Addresses API key");
                         ?? "Test";
            var apiToken = Environment.GetEnvironmentVariable("ADDRESSES_API_TOKEN")
                           //?? throw new ArgumentNullException("Addresses API token");
                           ?? "Test";
            var connOptions = new AddressesAPIConnectionOptions(apiBaseUrl, apiKey, apiToken);

            services.AddScoped<IAddressesAPIContext>(s =>
            {
                return new AddressesAPIContext(connOptions);
            });
        }

        private static void RegisterGateways(IServiceCollection services)
        {
            services.AddScoped<ITaxonomiesGateway, TaxonomiesGateway>();
            services.AddScoped<IServicesGateway, ServicesGateway>();
            services.AddScoped<IAddressesGateway, AddressesGateway>();
        }

        private static void RegisterUseCases(IServiceCollection services)
        {
            services.AddScoped<ITaxonomiesUseCase, TaxonomiesUseCase>();
            services.AddScoped<IServicesUseCase, ServicesUseCase>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            env.EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    scope.ServiceProvider.GetService<DatabaseContext>().Database.Migrate();
                }
            }
            else
            {
                app.UseHsts();
            }

            //Get All ApiVersions,
            var api = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
            _apiVersions = api.ApiVersionDescriptions.ToList();

            //Swagger ui to view the swagger.json file
            app.UseSwaggerUI(c =>
            {
                foreach (var apiVersionDescription in _apiVersions)
                {
                    //Create a swagger endpoint for each swagger version
                    c.SwaggerEndpoint($"{apiVersionDescription.GetFormattedApiVersion()}/swagger.json",
                    $"{ApiName}-api {apiVersionDescription.GetFormattedApiVersion()}");
                }
            });
            app.UseSwagger();
            app.UseRouting();
            app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials());
            app.UseEndpoints(endpoints =>
            {
                //SwaggerGen won't find controllers that are routed via this technique.
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
