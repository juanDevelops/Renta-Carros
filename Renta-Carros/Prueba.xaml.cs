

namespace Renta_Carros;

public partial class Prueba : ContentPage
{
    CarrosViewModel carrosViewModel = new CarrosViewModel();

    public Prueba()
    {
        InitializeComponent();

        //// Obtener la imagen del primer carro en la lista de CarrosViewModel
        //if (carrosViewModel.CarrosCollection.Count > 0)
        //{
        //    var primerCarro = carrosViewModel.CarrosCollection[0];
        //    //Bytes[] = Magnetometer
        //    ImageSource imagen = ImageSource.FromStream(() => new MemoryStream(primerCarro.Imagen.AsByteArray)); // Asígnale la imagen a la propiedad
        //    var stream = new MemoryStream(primerCarro.Imagen.AsByteArray);
        //    Image aImage = new Image
        //    {
        //        Source = ImageSource.FromStream(() => stream)
        //    };

        //    //aqui modificar el databinding

        //}

    }
}
    