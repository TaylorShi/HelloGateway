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

            // �������ж�ȡ��ȫ��Կ�����뵽������
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]));
            services.AddSingleton(securityKey);

            // ��������֤��ָ��Ĭ�Ϸ���ΪCookies
            services.AddAuthentication(defaultScheme: CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {

                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // ��֤ǩ����
                        ValidateIssuer = true,
                        // ��֤�ͻ���
                        ValidateAudience = true,
                        // ��֤ʧЧʱ��
                        ValidateLifetime = true,
                        // ʧЧʱ���ƫ��ʱ�䣬��������30�룬��ζ����ʧЧ��30���������ǿ���ʹ�õ�
                        ClockSkew = TimeSpan.FromSeconds(30),
                        // �Ƿ���֤SigningKey
                        ValidateIssuerSigningKey = true,
                        // ��Ч�Ŀͻ���
                        ValidAudience = "localhost",
                        // ��Ч��ǩ����
                        ValidIssuer = "localhost",
                        // ǩ������Կ
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

            // ���������֤�м��
            app.UseAuthentication();

            // ������UseEndpoints֮ǰ
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOcelot().Wait();
        }
    }
}
