using Microsoft.Extensions.Logging;
using testRender.Data;
using testRender.View;
using testRender.ViewModel;

namespace testRender;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<DatabaseContext>();
		builder.Services.AddSingleton<ProductViewModel>();
		//builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<dbTest>();

		return builder.Build();
	}
}

