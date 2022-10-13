using System.Text;

namespace LPWService.StaticFile
{
    internal static class ValidationStaticMethod
    {
        internal static bool CheckSgReflection(this object obj)
        {
            string sg = string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (var item in obj.GetType().GetProperties())
            {
                if (item.Name == "Sg")
                {
                    sg = item.GetValue(obj).ToString();
                    continue;
                }
                sb.Append(item.GetValue(obj));
            }
            return sg == sb.ToString().Md5Encrypto();
        }
        internal static bool CheckSgSeri(this object obj)
        {
            var sgmd5 = obj.ToJson().JsonTo<Dictionary<string, string>>();
            StringBuilder sb = new StringBuilder();
            string sg = sgmd5["Sg"];
            sgmd5.Remove("Sg");
            foreach (var item in sgmd5)
            {
                sb.Append(item.Value);
            }
            return sg == sgmd5.ToString().Md5Encrypto();
        }
    }
}
