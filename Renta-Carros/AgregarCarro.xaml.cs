
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Controls.Platform;
using MongoDB.Bson;
using System.IO;
using Microsoft.Maui.Graphics;

namespace Renta_Carros
{
    public partial class AgregarCarro : ContentPage
    {
    	public AgregarCarro()
    	{
    		InitializeComponent();
    	}


        
    	String filePath = "";
        byte[] imagenBytes = null;

        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            var tabbedPage = Application.Current.MainPage as Menu;
            Menu menu = tabbedPage as Menu;
            dbMethods db = new dbMethods(tabbedPage.ipv4);
            Prueba prueba = new Prueba();

            // Validar que ninguno de los parámetros sea nulo
            if (!string.IsNullOrEmpty(tbMarca.Text) &&
                !string.IsNullOrEmpty(tbModelo.Text) &&
                !string.IsNullOrEmpty(tbAño.Text) &&
                !string.IsNullOrEmpty(tbColor.Text) &&
                !string.IsNullOrEmpty(tbPlacas.Text) &&
                !string.IsNullOrEmpty(tbPrecio.Text) &&
                filePath != "")
            {
                // Llamar al método InsertarCarro si todos los parámetros son válidos
                db.InsertarCarro(filePath, tbMarca.Text, tbModelo.Text, tbAño.Text, tbColor.Text, tbPlacas.Text, tbPrecio.Text);
                prueba.ActualizarLista();
                await DisplayAlert("Aviso", "Nuevo auto registrado exitosamente.", "Ok");
            }
            else
            {
                await DisplayAlert("Error", "Todos los campos son requeridos", "Ok");
                return;
            }

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
                    imagenBytes = null;
    			}
    		}
    		catch (Exception ex)
    		{
    			await DisplayAlert("Error", $"Error al cargar la imagen {ex.Message}", "Ok");
    		}
        }

        public void RellenarDatos(string marca, string modelo, string año, string color, string placas, string precio, ImageSource source, byte[] imageBytes)
        {
            imagenBytes = imageBytes;
            imgCarro.Source = source;
            tbMarca.Text = marca;
            tbModelo.Text = modelo;
            tbAño.Text = año;
            tbColor.Text = color;
            tbPlacas.Text = placas;
            tbPrecio.Text = precio;
        }

        private async void btnModificar_Clicked(object sender, EventArgs e)
        {
            var tabbedPage = Application.Current.MainPage as Menu;
            Menu menu = tabbedPage as Menu;
            dbMethods db = new dbMethods(tabbedPage.ipv4);

            if (imagenBytes==null)
            {
                imagenBytes = File.ReadAllBytes(filePath);
            }

            if (db.ModificarCarroPorPlaca(imagenBytes, tbMarca.Text, tbModelo.Text, tbAño.Text, tbColor.Text, tbPlacas.Text, tbPrecio.Text))
            {
                await DisplayAlert("Error", $"Auto ha sido modificado.", "Ok");
                //await DisplayActionSheet("Elige", "Nose", "Nose 2");
                await DisplayPromptAsync("Titulo", "asdf");
                Prueba prueba = tabbedPage.Children[0] as Prueba;
                prueba.ActualizarLista();
                tabbedPage.CurrentPage = prueba;
            }
        
        }
    }
}