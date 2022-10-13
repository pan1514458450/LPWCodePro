namespace LPWService.BaseRepostiory
{
    public interface ICsredisHelp
    {
        Task<T> GetRedis<T>(string key);
        Task<T> GetDeleteRedis<T>(string key);
        Task<bool> SetRedis(string key, object value, int time);
        Task<bool> DeleteRedis(string key);
        Task<bool> CheckKey(string key);
    }
}
