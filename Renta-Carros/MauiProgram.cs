

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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "medium");
                    fonts.AddFont("OpenSans-Semibold.ttf", "bold");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "fas");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
