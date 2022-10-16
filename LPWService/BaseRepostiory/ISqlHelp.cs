using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPWService.BaseRepostiory
{
    public interface ISqlHelp
    {
        Task<T> Get<T>(string sql,  object args);
        Task<List<T>> GetALL<T>(string sql,  object args);
    }
}
