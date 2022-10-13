namespace LPWService
{
    public struct ConstCode
    {
        public const int VerificationCode = 4;
        public const string SqlServer = "server=.;database=Sysdb;uid=sa;pwd=123456;";
        public const string SendEmail = "pan1514458450@163.com";
        public const int ClockSkew = 10;
        /// <summary>
        /// 接受人
        /// </summary>
        public const string ValidAudience = "https://127.0.0.1";
        /// <summary>
        /// 发签人
        /// </summary>
        public const string ValidIssuer = "LPW";
        /// <summary>
        /// 
        /// </summary>
        public const string IssuerSigningKey = "6Zi/5pifUGx1c+mYv+aYn1BsdXPpmL/mmJ9QbHVz6Zi/5pifUGx1c+mYv+aYn1BsdXPpmL/mmJ9QbHVz6Zi/5pifUGx1c+mYv+aYn1BsdXPpmL/mmJ9QbHVz6Zi/5pifUGx1cw==";
        /// <summary>
        /// 时长
        /// </summary>
        public const int Expires = 120;
        public const string Success = "Success";
        public const string Error = "Error";
    }
}
