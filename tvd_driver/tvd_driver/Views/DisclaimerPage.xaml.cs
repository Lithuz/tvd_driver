using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tvd_driver.Services;
using tvd_driver.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tvd_driver.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisclaimerPage : ContentPage
    {
        VentasItemViewModel Venta;
        public DisclaimerPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Venta = MainViewModel.Getinstance().Venta;
            DiclaimerText.Text = "CONTRATO QUE CELEBRAN POR UNA PARTE BIENESTAR EN TERAPIAS HIDRATANTES " +
                $"S.DE R.L.DE C.V.Y POR LA OTRA EL / LA C.{Venta.NombreCliente.ToUpper()}, " +
                "COMO CLIENTE.\n\n" +
                $"Yo, {Venta.NombreCliente.ToUpper()}, he sido INFORMADO DETALLADAMENTE sobre las características de los productos " +
                "y procedimientos que he contratado bajo mi propia voluntad, y autorizó al personal de la empresa " +
                "Bienestar en Terapias Hidratantes S.de R.L.de C.V.,quién previamente ha sido contratado por el " +
                "cliente, realice los procedimientos médicos previamente acordados necesarios para llevar a cabo " +
                "la atención, aceptando desde ahora cualesquiera y todos los riesgos implícitos a la terapia o " +
                "derivadas de la misma.\n\n" +
                "Se le ha informado sobre los causas y efectos secundarios inherentes a la mencionada y " +
                "explicada terapia de infusión, y que son los siguientes: flebitis, dolor, molestias, y / o alergias a " +
                "algunos de los componentes. Todo ello tal y como preceptúa la actual Ley de Autonomía del " +
                "Paciente, por lo cual, entiende y acepta los anteriores puntos por lo que firma el presente " +
                $"CONSENTIMIENTO INFORMADO En la fecha: {Venta.Fecha}";
        }

        private async void Acept_Clicked(object sender, EventArgs e)
        {
            var DisclaimerConcat =
                   DiclaimerText.Text + "|" +
                   ntyEnfermedad.Text + "|" +
                   ntyMedicamento.Text + "|" +
                   ntyMedicamento2.Text + "|" +
                   FR.Text + "|" +
                   FC.Text + "|" +
                   TA.Text + "|" +
                   O2.Text + "|" +
                   T.Text;

            ApiServices apiService = new ApiServices();
            var response = await apiService.SaveDisclaimer(Venta.idVenta, DisclaimerConcat);
            if (response)
            {
                await Navigation.PopModalAsync();
            }
        }
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}