namespace Renta_Carros;

public partial class RentarCarro : ContentPage
{
	public RentarCarro()
	{
		InitializeComponent();
        
	}


    public void RellenarDatos(string placas, string precio)
    {
        tbPlacas.Text = placas.ToString();
        tbPrecio.Text = precio.ToString();
    }

    private void btnGuardar_Clicked(object sender, EventArgs e)
    {

    }

    private void btnCerrar_Clicked(object sender, EventArgs e)
    {

    }
}