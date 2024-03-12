namespace Renta_Carros;

public partial class RentarCarro : ContentPage
{
	public RentarCarro()
	{
		InitializeComponent();
        
	}

    dbMethods db = new dbMethods();

    public void RellenarDatos(string placas, string precio)
    {
        tbPlacas.Text = placas.ToString();
        tbPrecio.Text = precio.ToString();
    }


    private void btnCerrar_Clicked(object sender, EventArgs e)
    {

    }

    private int DiasDeCompra()
    {
        var diasDeCompra = 0;
        var salida = dpFechaSalida.Date;
        var entrega = dpFechaEntrega.Date;

        if (salida < entrega)
        {
            diasDeCompra = (int)(entrega - salida).TotalDays;
        }
        else
        {

        }

        return diasDeCompra;
    }

    private async void dpFechaEntrega_DateSelected(object sender, DateChangedEventArgs e)
    {
        var salida = dpFechaSalida.Date;
        var entrega = dpFechaEntrega.Date;

        if (salida >= entrega)
        {
            await DisplayAlert("Error", "La fecha de salida es posterior o igual a la fecha de entrega.", "Ok");
            return;
        }

        var diasDeCompra = (int)(entrega - salida).TotalDays; 
                                                            
        if (diasDeCompra < 0)
        {
            await DisplayAlert("Error", "La fecha de entrega es anterior a la fecha de salida.", diasDeCompra.ToString());
            return;
        }

        if (!int.TryParse(tbPrecio.Text, out int precioPorDia))
        {
            await DisplayAlert("Error", "Precio no es un número válido.", "Ok");
            return;
        }

        var precioCompra = diasDeCompra * precioPorDia; 

        lblPrecio.Text = $"Total a pagar: {precioCompra}";
    }


    private async void btnGuardar_Clicked_1(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(tbCliente.Text) ||
            string.IsNullOrEmpty(tbTelefono.Text) ||
            string.IsNullOrEmpty(tbPlacas.Text) ||
            string.IsNullOrEmpty(tbPrecio.Text) ||
            DiasDeCompra() <= 0)
        {
            await DisplayAlert("Error", "Rellene todos los campos.", "Ok");
            return;
        }

        
        if (!db.ExisteCarroPorPlaca(tbPlacas.Text))
        {
            await DisplayAlert("Error", "No existe ese auto.", "Ok");
            return;
        }

        if (db.RentarCarro(tbPlacas.Text))
        {
            await DisplayAlert("Exito", "Auto ha sido rentado", "Ok");
        }
       
    }

    private void btnCerrar_Clicked_1(object sender, EventArgs e)
    {

    }
}