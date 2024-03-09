namespace Renta_Carros;

public partial class Prueba : ContentPage
{
    public Prueba()
    {
        InitializeComponent();
        
    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        CarrosViewModel carros = new CarrosViewModel();
    }


    private async void btnRentar_Clicked(object sender, EventArgs e)
    {
        var tabbedPage = Application.Current.MainPage as Menu;
        //((RentarCarro)((Prueba)this.Parent).Parent.CurrentPage).RellenarDatos("1521f", "abd");
        var rentarCarroPage = tabbedPage.Children.FirstOrDefault(p => p is RentarCarro) as RentarCarro;
        //var myPage = (Prueba)this.Parent;
        RentarCarro rentarPage = tabbedPage.Children[2] as RentarCarro;
        //var tabbedPage = (TabbedPage)this.Parent;
        //var paginaRentarCarro = (RentarCarro)tabbedPage.CurrentPage;
        //paginaRentarCarro.RellenarDatos("1521f", "abd");
        rentarPage.RellenarDatos("1521f", "abd");
        //var carroSeleccionado = (Carros)ListaCarros.SelectedItem;

        //if (carroSeleccionado != null)
        //{
        //    paginaRentarCarro.RellenarDatos(carroSeleccionado.Placas.ToString(), carroSeleccionado.Precio.ToString());
        //    //var mensaje = $"Marca: {carroSeleccionado.Marca}\nModelo: {carroSeleccionado.Modelo}\nAño: {carroSeleccionado.Año}";
        //    //await DisplayAlert("Información del vehículo", mensaje, "Ok");
        //}
        //else
        //{
        //    await DisplayAlert("Error", "No se ha seleccionado ningún vehículo.", "Ok");
        //}
    }
}
