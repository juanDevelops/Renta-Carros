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

    dbMethods db = new dbMethods();

    private async void btnPrueba_Clicked(object sender, EventArgs e)
    {
        //List<Carros> documents = db.ObtenerCarros();

        //if (documents != null)
        //{
        //    foreach (var document in documents)
        //    {
        //        Console.WriteLine(document.ToJson());
        //    }
        //}

        //foreach (var carro in documents)
        //{
            
        //}
    }
}