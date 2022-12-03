using Project4.ViewModel;

namespace Project4.Views;

public partial class History : ContentPage
{
    private HistoryPageViewModel _viewMode;
    public History(HistoryPageViewModel viewModel)
	{
		InitializeComponent();
        _viewMode = viewModel;
        this.BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewMode.GetExpressionListCommand.Execute(null);
    }
}