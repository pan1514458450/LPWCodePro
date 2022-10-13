namespace LPWService.BaseRepostiory
{
    public sealed class CsredisHelp : ICsredisHelp
    {
        public async Task<bool> CheckKey(string key)
        {
            return await RedisHelper.ExistsAsync(key);
        }

        public async Task<bool> DeleteRedis(string key)
        {
            return await RedisHelper.DelAsync(key) > 0 ? true : false;
        }

        public async Task<T> GetRedis<T>(string key)
        {
            
            return await RedisHelper.GetAsync<T>(key);
        }
        public async Task<T> GetDeleteRedis<T>(string key)
        {
            var result =await RedisHelper.GetAsync<T>(key);
            await RedisHelper.DelAsync(key);
            return result;
        }

        public async Task<bool> SetRedis(string key, object value, int time)
        {
            return await RedisHelper.SetAsync(key, value, time);
        }
    }
}
