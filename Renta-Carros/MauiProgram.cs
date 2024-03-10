#if ANDROID
using Renta_Carros.Platforms.Android;
#endif
#if IOS
using Renta_Carros.Platforms.iOS;
#endif

using Microsoft.Extensions.Logging;

namespace Renta_Carros
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureMauiHandlers(handlers => {
#if ANDROID
                    handlers.AddHandler<CustomViewCell, CustomViewCellHandler>();
#endif
#if IOS
				handlers.AddHandler<CustomViewCell, CustomViewCellHandler>();
#endif
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("DMSans-Regular.ttf", "medium");
                    fonts.AddFont("DMSans-Bold.ttf", "bold");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "fas");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
