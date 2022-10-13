namespace LPWBussion.DTO.SysDTO
{
    public class CaptchaResult
    {
        /// <summary>
        /// CaptchaCode
        /// </summary>
        public string CaptchaCode { get; set; }

        /// <summary>
        /// CaptchaMemoryStream
        /// </summary>
        public MemoryStream CaptchaMemoryStream { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
