using Project4.Services;
using Project4.ViewModel;
using Project4.Views;

namespace Project4;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        // Services
        builder.Services.AddSingleton<IExpressionService, ExpressionService>();

        //Views Registration
        builder.Services.AddSingleton<CalculatorDisplay>();
        builder.Services.AddTransient<History>();

        //View Modles 
        builder.Services.AddSingleton<CalculatorViewModel>();
        builder.Services.AddTransient<HistoryPageViewModel>();
        return builder.Build();
	}
}
