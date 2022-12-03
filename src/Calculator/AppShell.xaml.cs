using Project4.ViewModel;
using Project4.Views;

namespace Project4;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(CalculatorDisplay), typeof(CalculatorDisplay));
		//Routing.RegisterRoute(nameof(History), typeof(History));
	}
}
