using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace FixerExchangeCurrency.Repositories
{
    public abstract class BaseRepository
    {
        protected DbConnection Connection;

        protected BaseRepository()
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
        }

        protected async Task<IEnumerable<TResult>> GetListAsync<TResult>(string condition, object param = null)
        {
            using (var conn = GetDbConnection())
            {
                return await conn.GetListAsync<TResult>(condition, param);
            }
        }

        protected async Task<int?> InsertAsync<T>(T entityObject)
        {
            using (var conn = GetDbConnection())
            {
                return await conn.InsertAsync(entityObject);
            }
        }

        private DbConnection GetDbConnection()
        {
            return new MySqlConnection("Database=localdb;Data Source=localhost;User Id=root;Password=root");
        }
    }
}