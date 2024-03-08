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
        var carroSeleccionado = (Carros)ListaCarros.SelectedItem;

        if (carroSeleccionado != null)
        {
            var mensaje = $"Marca: {carroSeleccionado.Marca}\nModelo: {carroSeleccionado.Modelo}\nAño: {carroSeleccionado.Año}";
            await DisplayAlert("Información del vehículo", mensaje, "Ok");
        }
        else
        {
            await DisplayAlert("Error", "No se ha seleccionado ningún vehículo.", "Ok");
        }
    }
}
