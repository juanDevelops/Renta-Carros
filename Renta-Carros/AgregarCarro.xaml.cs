
namespace Renta_Carros;

public partial class AgregarCarro : ContentPage
{
	public AgregarCarro()
	{
		InitializeComponent();
	}

    db db = new db();
	String filePath = "";

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        db.InsertarCarro(filePath, tbMarca.Text, tbModelo.Text, tbAño.Text, tbColor.Text, tbPlacas.Text, tbPrecio.Text);
        await DisplayAlert("Aviso", "fijate", "Ok");

        #region LIMPIAR CAMPOS
        tbMarca.Text = "";
		tbModelo.Text = "";
		tbAño.Text = "";
		tbColor.Text = "";
		tbPlacas.Text = "";
		tbPrecio.Text = "";
		imgCarro.Source = "";
        #endregion
    }

    private async void btnCargarFoto_Clicked(object sender, EventArgs e)
    {
		try
		{
			var resultado = await MediaPicker.PickPhotoAsync();
			
            if (resultado != null)
			{
                filePath = resultado.FullPath;
                imgCarro.Source = ImageSource.FromStream(() => resultado.OpenReadAsync().Result);
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"Error al cargar la imagen {ex.Message}", "Ok");
		}
    }
}