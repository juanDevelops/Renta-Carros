using Microsoft.Maui.Controls;
using MongoDB.Bson;

namespace Renta_Carros;

public partial class Prueba : ContentPage
{
    public Prueba()
    {
        InitializeComponent();
        BindingContext = null; // Establece el BindingContext en null
        BindingContext = new CarrosViewModel(); // Crea una nueva instancia de tu ViewModel y establece el BindingContext nuevamente
        ListaCarros.ItemsSource = (BindingContext as CarrosViewModel).CarrosCollection;
    }



    private async void btnRentar_Clicked(object sender, EventArgs e)
    {
        var tabbedPage = Application.Current.MainPage as Menu;
        RentarCarro rentarCarro = tabbedPage.Children[2] as RentarCarro;

        var carroSeleccionado = (Carros)ListaCarros.SelectedItem;

        if (carroSeleccionado != null)
        {
            rentarCarro.RellenarDatos(carroSeleccionado.Placas.ToString(), carroSeleccionado.Precio.ToString());
            tabbedPage.CurrentPage = rentarCarro;
        }
        else
        {
            await DisplayAlert("Error", "No se ha seleccionado ningún vehículo.", "Ok");
        }
    }

    private async void btnModificar_Clicked(object sender, EventArgs e)
    {
        var tabbedPage = Application.Current.MainPage as Menu;
        AgregarCarro modificarCarro = tabbedPage.Children[0] as AgregarCarro;

        var carroSeleccionado = (Carros)ListaCarros.SelectedItem;


        if (carroSeleccionado != null)
        {
            BsonBinaryData imageData = carroSeleccionado.Imagen;
            byte[] imageBytes = imageData.Bytes;
            ImageSource sourceImagen = ImageSource.FromStream(() => new MemoryStream(imageBytes));
            modificarCarro.RellenarDatos(carroSeleccionado.Marca.ToString(), carroSeleccionado.Modelo.ToString(), carroSeleccionado.Año.ToString(), carroSeleccionado.Color.ToString(), carroSeleccionado.Placas.ToString(), carroSeleccionado.Precio.ToString(), sourceImagen, imageBytes);
            
            tabbedPage.CurrentPage = modificarCarro;
        }
        else
        {
            await DisplayAlert("Error", "No se ha seleccionado ningún vehículo.", "Ok");
        }
    }

    public void btnActualizar_Clicked(object sender, EventArgs e)
    {
        BindingContext = null; 
        BindingContext = new CarrosViewModel();
        ListaCarros.ItemsSource = (BindingContext as CarrosViewModel).CarrosCollection;
    }

    public void ActualizarLista()
    {
        BindingContext = null;
        BindingContext = new CarrosViewModel();
        ListaCarros.ItemsSource = (BindingContext as CarrosViewModel).CarrosCollection;
    }
}
