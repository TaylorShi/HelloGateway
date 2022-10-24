using GrpcServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TeslaOrder.Mobile.ApiAggregator
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
            services.AddGrpcClient<OrderGrpc.OrderGrpcClient>(grpcClientFactoryOptions =>
            {
                grpcClientFactoryOptions.Address = new Uri("https://localhost:5001");
            }).ConfigurePrimaryHttpMessageHandler(serviceProvider =>
            {
                var handler = new SocketsHttpHandler();
                // 允许无效或自签名证书
                handler.SslOptions.RemoteCertificateValidationCallback = (a, b, c, d) => true;
                return handler;
            });

            services.AddGrpcClient<PayGrpc.PayGrpcClient>(grpcClientFactoryOptions =>
            {
                grpcClientFactoryOptions.Address = new Uri("https://localhost:5006");
            }).ConfigurePrimaryHttpMessageHandler(serviceProvider =>
            {
                var handler = new SocketsHttpHandler();
                // 允许无效或自签名证书
                handler.SslOptions.RemoteCertificateValidationCallback = (a, b, c, d) => true;
                return handler;
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
