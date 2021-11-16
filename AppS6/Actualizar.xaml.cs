using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace AppS6
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Actualizar : ContentPage
    {
        private const string url = "http://192.168.0.102/App/post.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<AppS6.Datos> _post;

        public Actualizar(string id,string name,string lasName,string age)
        {
            
            InitializeComponent();
            txtCodigo.Text = id;
            txtNombre.Text = name;
            txtApellido.Text = lasName;
            txtEdad.Text = age;
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                string codigo = txtCodigo.Text;
                string  name=txtNombre.Text;
                string lastName = txtApellido.Text;
                string age = txtEdad.Text;
                var uri = "http://192.168.0.102/App/post.php?codigo=" + codigo + "&nombre="+name+"&apellido="+lastName+"&edad="+age+"";
                HttpClient cliente = new HttpClient();
                var parametros = new System.Collections.Specialized.NameValueCollection();
                parametros.Add("codigo", txtCodigo.Text);
                parametros.Add("nombre", txtNombre.Text);
                parametros.Add("apellido", txtApellido.Text);
                parametros.Add("edad", txtEdad.Text);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("codigo",codigo),
                    new KeyValuePair<string, string>("nombre", name),
                    new KeyValuePair<string, string>("apellido",lastName),
                     new KeyValuePair<string, string>("edad",age),
                });

                await cliente.PutAsync(uri,content);

                /*   await cliente.PutAsync("http://192.168.0.102/App/post.php",
                   new FormUrlEncodedContent(new[]
                   {
                        new KeyValuePair<string, string>("codigo",codigo),
                        new KeyValuePair<string, string>("nombre", name),
                        new KeyValuePair<string, string>("apellido",lastName),
                         new KeyValuePair<string, string>("edad",age),
                   }));*/
                var mensaje = "Se actualizo";
                DependencyService.Get<Mensaje>().longAlert(mensaje);
                limpiar();
                //await DisplayAlert("Ok", "Se actualizo", "ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("error", ex.Message, "ok");
            }
        }
        public void limpiar() {
            txtCodigo.Text="";
           txtNombre.Text="";
            txtApellido.Text="";
          txtEdad.Text="";
        }
        private async void btnRegresar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}