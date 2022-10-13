using LPWService.BaseRepostiory;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace LPWService.StaticFile
{
    public static class ExtensionMethods
    {
        private static JsonSerializerOptions options;
        private static HttpClient client;
        internal readonly static ICsredisHelp csredis;

        static ExtensionMethods()
        {
            client = new HttpClient();
            options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeConverterUsingDateTimeParse());
            options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            csredis = new CsredisHelp();
        }
        public static IQueryable<F> QueryAndJoin<Q, T, C, F>(this IQueryable<Q> quer, IQueryable<T> values, Expression<Func<Q, C>> firstexp, Expression<Func<T, C>> twoexp, Expression<Func<Q, T, F>> exp)
        {
            return quer.Join(values, firstexp, twoexp, exp);
        }
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> values, bool IsAnd, Expression<Func<T, bool>> exp)
        {
            if (IsAnd)
                values = values.Where(exp);
            return values;
        }
        #region json
        /// <summary>
        /// 转json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj) => JsonSerializer.Serialize(obj, options);
        /// <summary>
        /// json转泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonTo<T>(this string json) => JsonSerializer.Deserialize<T>(json);

        #endregion

        #region http
        public static async Task<string> HttpGet(this string str)
        {
            return await client.GetStringAsync(str);
        }
        public static async Task<T> HttpGet<T>(this string str) where T : class, new()
        {
            return (await client.GetStringAsync(str)).JsonTo<T>();
        }
        public static async Task<string> HttpPost(this string str, string json)
        {
            return await HttpPostSend(str, json);
        }
        public static async Task<string> HttpPost(this string str, object json)
        {
            return await HttpPostSend(str, json, 1);
        }
        public static async Task<T> HttpPost<T>(this string str, string json) where T : class, new()
        {
            return (await HttpPostSend(str, json)).JsonTo<T>();
        }
        public static async Task<T> HttpPost<T>(this string str, object json) where T : class, new()
        {
            return (await HttpPostSend(str, json, 1)).JsonTo<T>();
        }
        private static async Task<string> HttpPostSend(string str, object obj, int enums = 0)
        {
            var responese = enums == 0 ? await client.PostAsync(str, new StringContent(obj.ToString())) :
                                         await client.PostAsync(str, new StringContent(obj.ToJson()));
            if (responese.StatusCode == System.Net.HttpStatusCode.OK)
                return await responese.Content.ReadAsStringAsync();
            throw new Exception($"http状态码请求为{responese.StatusCode}");
        }
        #endregion
        #region 加密

        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string Md5Encrypto(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentNullException("字符串为空");
            //32位大写
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }
        private static SymmetricAlgorithm mobjCryptoService = new RijndaelManaged();
        /// <summary>   
        /// 获得密钥   
        /// </summary>   
        /// <returns>密钥</returns>   
        private static byte[] GetLegalKey()
        {
            string sTemp = "xfsdfgsfgsdgsdfgsdfg";
            mobjCryptoService.GenerateKey();
            byte[] bytTemp = mobjCryptoService.Key;
            int KeyLength = bytTemp.Length;
            if (sTemp.Length > KeyLength)
                sTemp = sTemp.Substring(0, KeyLength);
            else if (sTemp.Length < KeyLength)
                sTemp = sTemp.PadRight(KeyLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
        /// <summary>   
        /// 获得初始向量IV   
        /// </summary>   
        /// <returns>初试向量IV</returns>   
        private static byte[] GetLegalIV()
        {
            string sTemp = "swetwerehetyeryertyerty";
            mobjCryptoService.GenerateIV();
            byte[] bytTemp = mobjCryptoService.IV;
            int IVLength = bytTemp.Length;
            if (sTemp.Length > IVLength)
                sTemp = sTemp.Substring(0, IVLength);
            else if (sTemp.Length < IVLength)
                sTemp = sTemp.PadRight(IVLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
        /// <summary>   
        /// 加密方法   
        /// </summary>   
        /// <param name="Source">待加密的串</param>   
        /// <returns>经过加密的串</returns>   
        public static string Sha256Encrypto(this string Source)
        {
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source);
            MemoryStream ms = new MemoryStream();
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            ms.Close();
            byte[] bytOut = ms.ToArray();
            var reuslt = Convert.ToBase64String(bytOut);
            return reuslt;
        }
        /// <summary>   
        /// 解密方法   
        /// </summary>   
        /// <param name="Source">待解密的串</param>   
        /// <returns>经过解密的串</returns>   
        public static string Sha256Decrypto(this string Source)
        {
            byte[] bytIn = Convert.FromBase64String(Source);
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
        #endregion

    }
    internal class DateTimeConverterUsingDateTimeParse : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader,
            Type typeToConvert, JsonSerializerOptions options) => DateTime.Parse(reader.GetString());

        public override void Write(Utf8JsonWriter writer, DateTime value,
            JsonSerializerOptions options) => writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
    }
}
