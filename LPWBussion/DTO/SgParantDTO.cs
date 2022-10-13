using LPWService.ModelValidation;

namespace LPWBussion.DTO
{

    [SgValidation(ErrorMessage = "签名错误")]
    public class SgParantDTO
    {
        public string Sg { get; set; }
    }
}
