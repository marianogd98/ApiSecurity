using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.Repository;
using WebApiPosRio.Models.Common;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApiPosRio
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
            string connection = Configuration.GetConnectionString("ProductionConnection");
            services.AddDbContext<RIOPOSContext>(opt => opt.UseSqlServer(connection));

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            //contexto de la base de datos
           services.AddDbContext<BIOContext>(opt =>
                                               opt.UseSqlServer("Server=172.50.3.74;Database=RIOPOS;ID=apiuser;PWD=123456;").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
           
            //services.AddDbContext<StoreContext>(c =>
            //c.UseInMemoryDatabase(Guid.NewGuid().ToString()).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddControllers();

            //parametros de variables de appsetting
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);

            //jwt
            var appSettings = appSettingSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(d => {
                d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer( ajb => 
            {
                ajb.RequireHttpsMetadata = false;
                ajb.SaveToken = true;
                ajb.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //Registrar la clase con inyeccion de dependencia
            //esto es estatico
            //services.Add(new ServiceDescriptor(typeof(IRolRepository), new RolRepository()));
            //services.Add(new ServiceDescriptor(typeof(), new ModuloRepository()));
            //services.Add(new ServiceDescriptor(typeof(IAccionRepository), new AccionRepository()));
            //services.AddSingleton<IRolRepository, RolRepository>();
            services.AddScoped<IBancoRepository, BancoRepository>();
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IModuloRepository, ModuloRepository>();
            services.AddScoped<IAccionRepository, AccionRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ICajaRepository, CajaRepository>();
            services.AddScoped<IInsideRepository, InsideRepository>();
            services.AddScoped<ITiendaRepository, TiendaRepository>();
            services.AddScoped<IInsideRepository, InsideRepository>();
            services.AddScoped<IAreaRepository, AreaRepository>();
            services.AddScoped<ICajaAdministrativaRepository, CajaAdministrativaRepository>();
            services.AddScoped<IDevolucionRepository, DevolucionRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IQqmRepository,QqmRepository>();
            services.AddScoped<IFormaPagoRepository, FormaPagoRepository>();
            services.AddScoped<ISpRepository, SpRepository>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IComprasInternacionalRepository, ComprasInternacionalRepository>();
            services.AddScoped<IPromocionesRepository, PromocionesRepository>();
            services.AddScoped<IApiConectorRepository, ApiConectorRepository>();
            services.AddScoped<IDonacionRepository, DonacionRepository>();
            services.AddScoped<IMetaRepository, MetaRepository>();
            services.AddScoped<ICandadoRepository, CandadoRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiPosRio", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            env.WebRootPath =  $"http://172.50.0.22:8302/";//$"https://localhost:44345/";//

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiPosRio v1"));
            }

            app.UseCors("MyPolicy");
            //app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseRouting();

           app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "images")),
                RequestPath = new PathString("/images")
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
