namespace Renta_Carros
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Menu();

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
                    handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
            });
        }
    }
}
