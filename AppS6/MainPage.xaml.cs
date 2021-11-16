using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net;
using System.Net.Http;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Diagnostics;
using Xamarin.Forms.Xaml;

namespace AppS6
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage 
    {
       
        string  id;
        string name;
        string lastName;
        string age;
       
        private const string url = "http://192.168.0.102/App/post.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<AppS6.Datos> _post;
        
        public object Constants { get; private set; }

        public MainPage()
        {
            InitializeComponent();
            // MainPage = new NavigationPage(new MainPage());
            get();

        }

       
        private async void get()
        {
            var content = await client.GetStringAsync(url);
            List<AppS6.Datos> post = JsonConvert.DeserializeObject<List<AppS6.Datos>>(content);
            _post = new ObservableCollection<AppS6.Datos>(post);
            MyListView.ItemsSource = _post;
        }
   /*     private async void btnConsultar_Clicked(object sender, EventArgs e)
        {
            var content = await client.GetStringAsync(url);
            List<AppS6.Datos> post = JsonConvert.DeserializeObject<List<AppS6.Datos>>(content);
            _post = new ObservableCollection<AppS6.Datos>(post);
            MyListView.ItemsSource = _post;
        }
        */
        private async Task btbIngresar_Clicked(object sender, EventArgs e)
        {
            HttpClient cliente = new HttpClient();
           // await cliente.PutAsync("http://192.168.0.102/App/post.php?{"+id+"}");
            await client.DeleteAsync("api/Songs/Delete/"+this.id+"");
            Uri uri = new Uri(string.Format("http://192.168.0.102/App/post.php", this.id));
          await  DisplayAlert("ok", this.id, "ok");
            HttpResponseMessage response = await client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"\tTodoItem successfully deleted.");
            }


        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            

             await Navigation.PushAsync(new Actualizar(id, name, lastName, age));
        }

        private void item(object sender, SelectedItemChangedEventArgs e)
        {
           
            var obj = (Datos)e.SelectedItem;
          
            this.id = obj.codigo.ToString();
            this.name = obj.nombre;
            this.lastName = obj.apellido;
            this.age = obj.edad.ToString();




        }
        private void datos(object obj) {

            
        }
        private async void btbEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                string uri = "http://192.168.0.102/App/post.php?codigo=" + this.id + "";
                var client = new HttpClient();
                var content = new StringContent(
                    JsonConvert.SerializeObject(new { codigo = this.id }));
                await client.DeleteAsync(uri);
                var mensaje = "Se Elimino";
                DependencyService.Get<Mensaje>().longAlert(mensaje);
              //  await DisplayAlert("Ok", "Se elimino correctamente", "ok");
                get();
            }
            catch (Exception ex)
            {
                await DisplayAlert("ok", ex.Message, "ok");
            }
        }

        private async void btbInsertar_Clicked(object sender, EventArgs e)
        {
            //utilizar el toast
            //var mensaje = "Alert toast xamarin";
            //DependencyService.Get<Mensaje>().longAlert(mensaje);
            await Navigation.PushAsync(new Insertar());
        }

        public  void datos_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");

        }
    }
}
