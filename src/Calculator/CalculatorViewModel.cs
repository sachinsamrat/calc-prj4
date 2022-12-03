using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Project4.Views;
using Project4.Model;
using System.Linq.Expressions;
using Expression = Project4.Model.Expression;
using Project4.Services;

namespace Project4.ViewModel
{
    [QueryProperty(nameof(Expression), "Expression")]
    public partial class CalculatorViewModel: ObservableObject
    {
        [ObservableProperty]
        private Expression _expression = new Expression();

        private readonly IExpressionService _expressionService;
        public CalculatorViewModel(IExpressionService expressionService)
        {
            _expressionService = expressionService;
        }

        [ICommand]
        private async void AddExpression()
        {
            int response = -1;
            response =  await _expressionService.AddExpression(new Model.Expression
            {
                Exp = Expression.Exp,
                Ans = Expression.Ans,
            });
        }
        

        
    }
}
