using LPWBussion.AutoMapperFile;
using LPWBussion.DTO;
using LPWService;
using LPWService.StaticFile;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Model;
using NETCore.MailKit.Extensions;
using SqlSugar;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;

namespace LPWCodePro.Filter
{
    internal static class ProConfig
    {
        private static WebApplicationBuilder webApplicationBuilder;
        internal static void Load(WebApplicationBuilder builder)
        {

            webApplicationBuilder = builder;
            builder.Services.AddDbContext<DbContextModule>(options =>
            options.UseSqlServer(ConstCode.SqlServer), ServiceLifetime.Singleton);
            builder.Services.AddAutoMapper(typeof(MapperProfile));
            var csredis = new CSRedis.CSRedisClient(builder.Configuration.GetConnectionString("Redis"));
            RedisHelper.Initialization(csredis);
            ISqlSugarClient sugarClient = new SqlSugarClient(new ConnectionConfig()
            {
                DbType = DbType.SqlServer,
                ConnectionString = ConstCode.SqlServer,
                IsAutoCloseConnection = true
            }, db =>
            {
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine(sql);//输出sql
                };
            });
            builder.Services.AddSingleton<ISqlSugarClient>(sugarClient);
            builder.Services.AddMailKit(optionBuilder =>
            {
                optionBuilder.UseMailKit(new NETCore.MailKit.Infrastructure.Internal.MailKitOptions()
                {
                    //get options from sercets.json
                    Server = "smtp.163.com",
                    Port = 465,
                    SenderName = "系统邮件",
                    SenderEmail = ConstCode.SendEmail,

                    // can be optional with no authentication 
                    Account = ConstCode.SendEmail,
                    Password = "WYWUCVUTDYYKADWM",
                    // enable ssl or tls
                    Security = true
                }, ServiceLifetime.Singleton);
            });

            Assembly.Load("LPWBussion").ExportedTypes.TreeClass();
            Assembly.Load("LPWService").ExportedTypes.TreeClass();


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(ConstCode.ClockSkew),
                ValidateIssuerSigningKey = true,
                ValidAudience = ConstCode.ValidAudience,
                ValidIssuer = ConstCode.ValidIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConstCode.IssuerSigningKey))
            };
            options.Events = new JwtBearerEvents
            {
                //权限验证失败后执行
                OnChallenge = context =>
                {
                    //终止默认的返回结果(必须有)
                    context.HandleResponse();
                    var result = new ResultCode() { Code = ResponseCode.Authentication, Status = false, Message = "验证失败" };
                    context.Response.ContentType = "application/json";
                    //验证失败返回401
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    context.Response.WriteAsync(result.ToJson());
                    return Task.FromResult(0);
                }
            };
        });


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                //在header中添加token，传递到后台
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传递)直接在下面框中输入Bearer {token}(注意两者之间是一个空格) \"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
            });
            builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            builder.Services.AddControllers(c =>
            {
                c.Filters.Add<ExceptionFilter>();
                c.Filters.Add<ActionFilter>();
            });

        }
        private static void TreeClass(this IEnumerable<Type> type)
        {
            foreach (var item in type)
            {
                if (!item.IsClass) continue;
                item.GetInterfaces().TreeInterfaces(item);
            }
        }
        private static void TreeInterfaces(this Type[] type, Type ImpType)
        {
            foreach (var item in type)
            {

                if (!item.IsInterface) continue;
                webApplicationBuilder.Services.AddSingleton(item, ImpType);
            }
        }


    }
}
