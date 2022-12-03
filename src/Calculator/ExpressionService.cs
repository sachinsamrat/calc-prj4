using Project4.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4.Services
{
    public class ExpressionService : IExpressionService
    {
        private SQLiteAsyncConnection _dbConnection;

        private async Task SetUpDb()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Expression.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<Expression>();
            }
        }

        public async Task<int> AddExpression(Expression expression)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(expression);
        }
        public async Task<List<Expression>> GetExpressionList()
        {
            await SetUpDb();
            var expressionList = await _dbConnection.Table<Expression>().ToListAsync();
            return expressionList;
        }
        public async Task<bool> DeleteAllExpression()
        {
            await SetUpDb();
            await _dbConnection.DeleteAllAsync<Expression>();
            return await Task.FromResult(true);
        }

    }
}
