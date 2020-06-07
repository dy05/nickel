using Newtonsoft.Json;
using Nickel.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Nickel.UWP
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page, IDisposable
    {
        private HttpClient client;
        public MainPage()
        {
            this.InitializeComponent();
            client = new HttpClient();
        }

        public void Dispose()
        {
            client?.Dispose();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // wv.Navigate(new Uri(App.BaseUrl));

            //var response = await client.GetAsync(App.BaseUrl + "/Product");

            //if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    var data = await response.Content.ReadAsStringAsync();
            //    // wv.NavigateToString(data);
            //    IEnumerable<ProductDTO> products = JsonConvert.DeserializeObject<List<ProductDTO>>(data);
            //    wv.ItemsSource = products;
            //} else
            //{
            //    var alert = new MessageDialog($"{response.StatusCode} : {response.ReasonPhrase}");
            //    await alert.ShowAsync();
            //}
        }

        private void wv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ProductDTO product = ((ListView)sender).SelectedItem as ProductDTO;
            //this.Frame.Navigate(typeof(ProductDetailsPage), product);
        }
    }
}
