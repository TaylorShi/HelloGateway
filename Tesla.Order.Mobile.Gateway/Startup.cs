using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Text;

namespace TeslaOrder.Mobile.Gateway
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

            services.AddOcelot(Configuration);

            // 从配置中读取安全密钥并加入到容器中
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]));
            services.AddSingleton(securityKey);

            // 添加身份验证，指定默认方案为Cookies
            services.AddAuthentication(defaultScheme: CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {

                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // 验证签发者
                        ValidateIssuer = true,
                        // 验证客户端
                        ValidateAudience = true,
                        // 验证失效时间
                        ValidateLifetime = true,
                        // 失效时间的偏离时间，这里设置30秒，意味着在失效的30秒内它还是可以使用的
                        ClockSkew = TimeSpan.FromSeconds(30),
                        // 是否验证SigningKey
                        ValidateIssuerSigningKey = true,
                        // 有效的客户端
                        ValidAudience = "localhost",
                        // 有效的签发者
                        ValidIssuer = "localhost",
                        // 签发者密钥
                        IssuerSigningKey = securityKey
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

            // 启用身份验证中间件
            app.UseAuthentication();

            // 必须在UseEndpoints之前
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOcelot().Wait();
        }
    }
}
