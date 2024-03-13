using Microsoft.Maui.Controls;
using MongoDB.Bson;
using Org.Apache.Http.Authentication;
using System.Collections.ObjectModel;

namespace Renta_Carros;

public partial class Prueba : ContentPage
{

    public Prueba()
    {
        InitializeComponent();
        if (true)
        {

        }
        //BindingContext = null; // Establece el BindingContext en null
        //BindingContext = new CarrosViewModel(); // Crea una nueva instancia de tu ViewModel y establece el BindingContext nuevamente
        //ListaCarros.ItemsSource = (BindingContext as CarrosViewModel).CarrosCollection;
    }

    public ObservableCollection<Carros> AutosList { get; set; }

    public async void CargarAutos()
    {
        var tabbedPage = Application.Current.MainPage as Menu;
        Menu menu = tabbedPage as Menu;
        dbMethods db = new dbMethods(tabbedPage.ipv4);

        try
        {
            var autos = await db.ObtenerAutos();
            AutosList = new ObservableCollection<Carros>();
            ListaCarros.ItemsSource = AutosList;
            foreach (var auto in autos)
            {
                AutosList.Add(auto);
            }
        }
        catch (Exception)
        {

        }
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
            //var mensaje = $"Marca: {carroSeleccionado.Marca}\nModelo: {carroSeleccionado.Modelo}\nAño: {carroSeleccionado.Año}";
            //await DisplayAlert("Información del vehículo", mensaje, "Ok");
        }
        else
        {
            await DisplayAlert("Error", "No se ha seleccionado ningún vehículo.", "Ok");
        }
    }

    private async void btnModificar_Clicked(object sender, EventArgs e)
    {
        var tabbedPage = Application.Current.MainPage as Menu;
        AgregarCarro modificarCarro = tabbedPage.Children[3] as AgregarCarro;

        var carroSeleccionado = (Carros)ListaCarros.SelectedItem;


        if (carroSeleccionado != null)
        {
            BsonBinaryData imageData = carroSeleccionado.Imagen;
            byte[] imageBytes = imageData.Bytes;
            ImageSource sourceImagen = ImageSource.FromStream(() => new MemoryStream(imageBytes));
            modificarCarro.RellenarDatos(carroSeleccionado.Marca.ToString(), carroSeleccionado.Modelo.ToString(), carroSeleccionado.Año.ToString(), carroSeleccionado.Color.ToString(), carroSeleccionado.Placas.ToString(), carroSeleccionado.Precio.ToString(), sourceImagen, imageBytes);
            //var mensaje = $"Marca: {carroSeleccionado.Marca}\nModelo: {carroSeleccionado.Modelo}\nAño: {carroSeleccionado.Año}";
            //await DisplayAlert("Información del vehículo", mensaje, "Ok");
            tabbedPage.CurrentPage = modificarCarro;
        }
        else
        { 
            var tabbedPageMenu = Application.Current.MainPage as Menu;
            Menu menu = tabbedPageMenu as Menu;


            //await DisplayAlert("Error", menu.ipv4, "Ok");
            await DisplayAlert("Error", "No se ha seleccionado ningún vehículo.", "Ok");
        }
    }

    public void btnActualizar_Clicked(object sender, EventArgs e)
    {
        CargarAutos();
        //await Navigation.PopAsync();
        //await Navigation.PushAsync(new Prueba());
        //BindingContext = null; // Establece el BindingContext en null
        //BindingContext = new CarrosViewModel(); // Crea una nueva instancia de tu ViewModel y establece el BindingContext nuevamente
        //ListaCarros.ItemsSource = (BindingContext as CarrosViewModel).CarrosCollection;
    }

    public void ActualizarLista()
    {
        BindingContext = null; // Establece el BindingContext en null
        BindingContext = new CarrosViewModel(); // Crea una nueva instancia de tu ViewModel y establece el BindingContext nuevamente
        ListaCarros.ItemsSource = (BindingContext as CarrosViewModel).CarrosCollection;
    }
}
