namespace Renta_Carros;

public partial class RentarCarro : ContentPage
{
	public RentarCarro()
	{
		InitializeComponent();
	}

    public RentarCarro(string placas, string precio)
    {
        InitializeComponent();

        tbPlacas.Text = placas;
        tbPrecio.Text = precio.ToString();
    }

    private void btnGuardar_Clicked(object sender, EventArgs e)
    {

    }

    private void btnCerrar_Clicked(object sender, EventArgs e)
    {

    }
}