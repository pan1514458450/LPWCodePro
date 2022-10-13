namespace LPWBussion.DTO
{
    public class ResultCode
    {
        public ResponseCode Code { get; set; } = ResponseCode.Success;
        public string Message { get; set; }
        public bool Status { get; set; } = true;
        public object data { get; set; }
    }
    public enum ResponseCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 200,
        /// <summary>
        /// 没有权限
        /// </summary>
        Authentication = 401,
        /// <summary>
        /// 参数校验失败
        /// </summary>
        IsValid = 403,
        /// <summary>
        /// 验签失败
        /// </summary>
        IsSgValid = 405,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 500
    }
}
