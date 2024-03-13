using Microsoft.Maui.Controls;
using MongoDB.Bson;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace Renta_Carros;

public partial class Inicio : ContentPage
{
   

    public Inicio()
    {
        InitializeComponent();
    }

    private async void btnEntrar_Clicked_1(object sender, EventArgs e)
    {
        var tabbedPage = Application.Current.MainPage as Menu;
        Menu menu = tabbedPage as Menu;
        menu.ipv4 = tbIp.Text;

        await DisplayAlert("Exito", "IP ha sido guardada.", "Ok");
    }

}
