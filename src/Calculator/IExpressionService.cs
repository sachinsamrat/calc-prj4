using Project4.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4.Services
{
    public interface IExpressionService
    {
        Task<List<Expression>> GetExpressionList();
        Task<int> AddExpression(Expression expression);
        Task<bool> DeleteAllExpression();
    }
}
