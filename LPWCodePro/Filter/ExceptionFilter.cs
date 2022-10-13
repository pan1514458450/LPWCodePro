using LPWService.StaticFile;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LPWCodePro.Filter
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var url = context.ActionDescriptor.DisplayName;
            var result = new LPWBussion.DTO.ResultCode()
            {
                Code = LPWBussion.DTO.ResponseCode.Error,
                Message = context.Exception.Message,
                Status = false
            };
            Log4Methods.Error("在" + url + ":形成错误,错误原因" + context.Exception.Message, context.Exception);
            context.Result = new Microsoft.AspNetCore.Mvc.ContentResult
            {
                // 返回状态码设置为200，表示成功
                StatusCode = StatusCodes.Status200OK,
                // 设置返回格式
                ContentType = "application/json;charset=utf-8",
                Content = result.ToJson()
            };

            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }

    }
}
