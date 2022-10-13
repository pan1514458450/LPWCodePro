using log4net;
using log4net.Config;

namespace LPWService.StaticFile
{
    public sealed class Log4Methods
    {
        private static ILog logger;
        static Log4Methods()
        {
            var repository = LogManager.CreateRepository("LPWCodePro");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            logger = LogManager.GetLogger(repository.Name, "RollingLogFileAppender");
        }
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Debug(string message, Exception exception = null)
        {

            if (exception == null)
                logger.Debug(message);
            else
                logger.Debug(FormartLog(message, exception));

        }
        /// <summary>
        /// 一般信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Info(string message, Exception exception = null)
        {

            if (exception == null)
                logger.Info(message);
            else
                logger.Info(FormartLog(message, exception));
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Warn(string message, Exception exception = null)
        {

            if (exception == null)
                logger.Warn(message);
            else
                logger.Warn(FormartLog(message, exception));

        }

        /// <summary>
        /// 一般错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Error(string message, Exception exception = null)
        {

            if (exception == null)
                logger.Error(message);
            else
                logger.Error(FormartLog(message, exception));
        }


        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Fatal(string message, Exception exception = null)
        {

            if (exception == null)
                logger.Fatal(message);
            else
                logger.Fatal(FormartLog(message, exception));

        }


        /// <summary>
        /// 自定义返回格式
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static string FormartLog(string throwMsg, Exception ex)
        {
            return string.Format("【错误地点】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}", new object[] { throwMsg, ex.GetType().Name, ex.Message, ex.StackTrace });
        }
    }
}
