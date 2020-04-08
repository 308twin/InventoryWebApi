using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using InventoryApi.Data;
using InventoryApi.Services;

namespace InventoryApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                //Microsotf.AspNetCore.MVC.NewtonsoftJson
                .AddNewtonsoftJson(options =>  //������ JSON ���л��ͷ����л����ߣ����滻��ԭ��Ĭ�ϵ� JSON ���л����ߣ�����ƵP32��
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddXmlDataContractSerializerFormatters() //XML ���л��ͷ����л����ߣ���ƵP8��
                .ConfigureApiBehaviorOptions(options =>   //�Զ�����󱨸棨��ƵP29��
                {
                    //IsValid = false ʱ��ִ��
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Type = "http://www.baidu.com",
                            Title = "���ִ���",
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Detail = "�뿴��ϸ��Ϣ",
                            Instance = context.HttpContext.Request.Path
                        };
                        problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };
                });

            //ʹ�� AutoMapper��ɨ�赱ǰӦ��������� Assemblies Ѱ�� AutoMapper �������ļ�����ƵP12��
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IStorageListRepository, StorageListRepository>();
            services.AddScoped<IOutboundListRepository, OutboundListRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<InventoryDbContext>(option =>
            {
                option.UseSqlite("Data Source = routine.db");

            });
        }

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
