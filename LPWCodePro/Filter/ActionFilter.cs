using LPWService.StaticFile;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace LPWCodePro.Filter
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            sw.Stop();
            var action = context.Controller;
            var actionname = context.RouteData.Values.FirstOrDefault().Value;
            Log4Methods.Info("控制器:" + action + "." + actionname + "本次运行时间:" + sw.Elapsed.TotalSeconds.ToString() + "秒");
        }
        Stopwatch sw = new Stopwatch();
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                Dictionary<string, string> errmessage = new Dictionary<string, string>();
                foreach (var item in context.ModelState)
                {
                    errmessage.Add(item.Key, item.Value.Errors.FirstOrDefault().ErrorMessage);
                };
                context.Result = new ObjectResult(new LPWBussion.DTO.ResultCode { Code = LPWBussion.DTO.ResponseCode.IsValid, Message = (errmessage.ToJson()), Status = false });
                return;
            }
            var actionRespone = (context.ActionArguments.FirstOrDefault().Value.ToJson());

            var actionname = context.RouteData.Values.FirstOrDefault().Value;
            Log4Methods.Debug("进入" + context.Controller + "." + actionname + "参数：" + actionRespone);
            sw.Start();
        }
    }
}
