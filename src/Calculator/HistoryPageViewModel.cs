using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Project4.Model;
using Project4.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4.ViewModel
{
    public partial class HistoryPageViewModel : ObservableObject
    {
        //public static List<Expression> ExpressionListForSearch { get; private set; } = new List<Expression>();
        public ObservableCollection<Expression> Expressions { get; set; } = new ObservableCollection<Expression>();

        private readonly IExpressionService _expressionService;


        public HistoryPageViewModel(IExpressionService expressionService)
        {
            _expressionService = expressionService;
        }
        [ICommand]
        public async void DeleteExpression()
        {
            var delResponse = await _expressionService.DeleteAllExpression();
            if (delResponse == true)
            {
                GetExpressionList();
            }
        }
        [ICommand]
        public async void GetExpressionList()
        {
            Expressions.Clear();
            var expressionList = await _expressionService.GetExpressionList();
            if (expressionList?.Count > 0)
            {
                expressionList = expressionList.ToList();
                foreach (var expression in expressionList)
                {
                    Expressions.Add(expression);
                }
            }
        }

        [ICommand]
        public async void DisplayAction(Expression expression)
        {
            GetExpressionList();
        }

    }
}
