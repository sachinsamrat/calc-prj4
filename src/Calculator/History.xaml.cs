namespace Calculator;

public partial class History1 : ContentPage
{
    public List<HistoryItem> Items { get; set; }
    public History1()
    {
        InitializeComponent();
        ListViewItems.ItemsSource = Calculator.historyItems;
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        ListViewItems.ItemsSource = Calculator.historyItems;
    }
}