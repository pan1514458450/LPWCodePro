using AutoMapper;
using LPWBussion.AutoMapperFile;
using LPWBussion.DTO.SysDTO;
using LPWBussion.DTO.SysShoopDTO;
using LPWService.BaseRepostiory;
using LPWService.Desgin;
using LPWService.StaticFile;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Primitives;
using Model.ShoopModel;
using Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPWBussion.ShoopBussion
{
    public sealed class ShoopImp : IShoopInter
    {
        private readonly ISqlSugarRepository _context;
        private readonly IMapper mapper;
        private readonly IAdoSql _mssql;
        private readonly IRedisOrDb _redisOrDb;
        private readonly ICsredisHelp csredis;
        private readonly IUserClaimsMethods _userClaims;
        public ShoopImp(ISqlSugarRepository context,IMapper mapper,IAdoSql mssql,IRedisOrDb redisOrDb,
            ICsredisHelp csredis,IUserClaimsMethods userClaims)
        {
            _userClaims = userClaims;
            _redisOrDb = redisOrDb;
            this.mapper = mapper;
            _mssql = mssql;
            _context = context;
            this.csredis = csredis;
        }
        public async Task<bool> UpdateShoop(ShoopInfoDTO shoopInfo, string[] arr)
        {

            var shoopmodel = mapper.Map<Shoop>(shoopInfo);
            for (int i = 0; i < arr.Length; i++)
            {
                shoopmodel.GetType().GetProperty($"Url{i + 1}").SetValue( shoopmodel, arr[i]);
            }
            shoopmodel.UserId = (int)_userClaims.GetClaims().Id;
           return await _context.UpdateAsync<Shoop>(shoopmodel)>0?true:false;
        }
        public async Task<bool> DeleteShoop(int Id)
        {
          
            string sql = $"update Shoops set Isdelete=0,updatetime=@updatetime where id=@id and UserId=@userId";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                 new SqlParameter("@id",Id),
                 new SqlParameter("@updatetime",DateTime.Now),
                 new SqlParameter("@userId",  _userClaims.GetClaims().Id)
            };
            return await _mssql.WirteOrUpdate(sql,parameters);
        }
        public async Task<bool> CreateShoop(ShoopInfoDTO shoopInfo, string[] arr)
        {
            StringBuilder sb= new StringBuilder();
            sb.Append($" insert into shoops value( @shoopname,@introduce,@price,@shooptypeid,@numberwarn,@iswarn,0,@userid");
            List<SqlParameter> parameter = new List<SqlParameter> {
                new SqlParameter("@shoopname",shoopInfo.ShoopName),
                new SqlParameter("@introduce",shoopInfo.Introduce),
                new SqlParameter("@price",shoopInfo.Price),
                new SqlParameter("@shooptypeid",shoopInfo.ShoopTypeId),
                new SqlParameter("@numberwarn",shoopInfo.NumBerWarn),
                new SqlParameter("@iswarn",shoopInfo.IsWarn),
                new SqlParameter("@userid",_userClaims.GetClaims().Id),
                new SqlParameter("@datetime",DateTime.Now),
            };
            for (int i = 0; i < 3; i++)
            {
                sb.Append($" @url{i}, ");
                if (i <= arr.Length)
                {
                    parameter.Add(new SqlParameter($" @url{i}", arr[i]));
                }
                else
                {
                    parameter.Add(new SqlParameter($" @url{i}", ""));
                }
            }
            sb.Append(" 0,@datetime,@datetime,@price)");
            sb.Append(" insert into [shoopUsers] values(@userid, (Select top 1 SCOPE_IDENTITY() AS 'Identity' from shoops where [UserId]=@userid),0,@datetime,@datetime,0");
            return await _mssql.TranWirteOrUpdate(sb.ToString(), parameter);           
        }
        public async Task<List<ShoopUserListDTO>> GetShoopList(string ShoopName, int ShoopTypeid,int index,int page)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select a.id, b.ShoopName,a.PriceProportion*b.Price as Price, b.Introduce,b.NumBerWarn,b.IsWarn,b.Url1,b.Url2,b.Url3,a.CreateDateTime,");
            sb.Append("  a.UpdateTime,count(case c.isdelete when 0 then 0 end) inventoryCard,count(case c.isdelete when 1 then 1 end) disableCard, count(case c.isdelete when 2 then 2 end) sellCard ");
            sb.Append("  from shoopUsers a left join shoops b  on a.ShoopId=b.Id left join shoopCards c on b.Id=c.ShoopId ");
            sb.Append(" where a.IsDelete=0 and b.IsDelete=0  and a.UserId=@UserId  ");
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@UserId", _userClaims.GetClaims().Id));
            if (!ShoopName.IsNull())
            {
                sb.Append(" And b.ShoopName like '%@ShoopName%'");
                parameter.Add(new SqlParameter("@ShoopName", ShoopName));
            }
            if(ShoopTypeid > 0)
            {
                sb.Append(" And b.ShoopTypeId in (@ShoopTypeId)");
                parameter.Add(new SqlParameter("@ShoopTypeId", ShoopTypeid));
            }
            sb.Append(" GROUP BY b.ShoopName,a.PriceProportion,b.Price,b.Introduce,b.ShoopTypeId,b.NumBerWarn,b.IsWarn,b.Url1,b.Url2,b.Url3,a.CreateDateTime,a.UpdateTime");
            sb.Append(" order by a.Id desc offset @page rows fetch next @index rows only;");
            parameter.Add(new SqlParameter("@index",index));
            parameter.Add(new SqlParameter("@page",index*page));
           return await _mssql.GetAllAsync<ShoopUserListDTO>(sb.ToString(), parameter);
        }


        public async Task<bool> SetOrUpdateUserShoop(List<SetOrUpdateUserShoopDTO> setOrUpdateUsers)
        {
            var userid = _userClaims.GetClaims();
            StringBuilder sb = new StringBuilder();
            setOrUpdateUsers.ForEach(d =>
            {
                sb.Append($" IF NOT EXISTS (SELECT 1 FROM ShoopUsers WHERE ShoopId={d.Id} AND )");
                sb.Append($" INSERT INTO ShoopUsers VALUES({userid.Id},{d.Id},{d.AddRate},0,@datetime,@datetime) ");
                sb.Append(handler: $" ELSE Update ShoopUsers set IsDelete=0,UpdateDateTime=@datetime,PriceProportion={d.AddRate} WHERE ShoopId={d.Id} and UserId={userid}");
            });
            return await _mssql.TranWirteOrUpdate(sb.ToString());
        }
        public async Task<bool> DeleteUserShoop(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@userid", _userClaims.GetClaims().Id));
            parameters.Add(new SqlParameter("@id", id));
            parameters.Add(new SqlParameter("@datetime", DateTime.Now));
            string sql = "UPDATE ShoopUsers set IsDelete=1,UpdateDateTime=@datetime Where Id=@id and UserId=@userid";
            return await _mssql.WirteOrUpdate(sql, parameters);
        }



        public async Task<bool> CreatShoopType(string TypeName)
        {
            await csredis.DeleteRedis(nameof(ShoopType) + "Table");

            var stat= await  _context.AddAsync(new ShoopType() { TypeName = TypeName })>0?true:false;
            await csredis.DeleteRedis(nameof(ShoopType) + "Table");
            return stat;

        }

        public async Task<bool> DeleteShoopType(int Id)
        {
           await csredis.DeleteRedis(nameof(ShoopType) + "Table");
            bool stat= await _context.DeleteAsync<ShoopType>(Id) > 0 ? true : false;
            await csredis.DeleteRedis(nameof(ShoopType) + "Table");
            return stat;
            //return await  _redisOrDb.WriteOrUpdate( _context.DeleteAsync<ShoopType>, nameof(ShoopType));
        }

        public async Task<List<ShoopTypeDTO>> ReadShoopType(string TypeName, int Size, int Index)
        {
           var typelist=await _redisOrDb.GetAllAsync<ShoopType>(TypeName.IsNull() ? null : x => x.TypeName.Contains(TypeName));
            //var result=  _context.GetAllPageAsync<ShoopType>(TypeName.IsNull()?null:x => x.TypeName.Contains(TypeName), Size, Index);
            return  mapper.Map<List<ShoopTypeDTO>>(typelist.Skip(Index).Take(Size));
        }

        public async Task<bool> UpdateShoopType(int Id, string TypeName)
        {
            await csredis.DeleteRedis(nameof(ShoopType) + "Table");

            string sql = " update ShoopsType set TypeName=@typeName,CreateDateTime=@datetime where id=@id";
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter("@typename",TypeName),
                new SqlParameter("@id",Id),
                new SqlParameter("@datetime",DateTime.Now)
            };
            var stat= await _mssql.WirteOrUpdate(sql, parameters);
            await csredis.DeleteRedis(nameof(ShoopType) + "Table");
            return stat;
        }

    }
}
