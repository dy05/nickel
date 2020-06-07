using Nickel.Interface;
using Nickel.Mobile.Models;
using Nickel.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Nickel.Mobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<ProductModel> products;

        public MainPage()
        {
            InitializeComponent();
            this.Appearing += MainPage_Appearing;

            products = new ObservableCollection<ProductModel>();
            //{
            //    new ProductModel(1, "Product 1", 1200, "Nouvel Description"),
            //    new ProductModel(2, "Product 2", 2200, "Nouvel Description"),
            //    new ProductModel(3, "Product 3", 3200, "Nouvel Description"),
            //}

            BindingContext = new MainPageViewModel
            (
                products
            );
        }

        private async void MainPage_Appearing(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add
                (
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );

                var response = await client.GetAsync(App.ApiAddress + "/Product/Index");

                if (response.IsSuccessStatusCode)
                {
                    List<ProductDTO> productDTOs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductDTO>>
                    (
                        await response.Content.ReadAsStringAsync()
                    );

                    foreach (var p in productDTOs)
                    {
                        Console.WriteLine(p.Photo);
                        products.Add
                        (
                            new ProductModel
                            (
                                p.Id,
                                p.Name,
                                p.Price,
                                p.Description,
                                p.Photo
                            //App.ApiAddress + p.Photo
                            )
                        );
                    }
                }
            }

            lv.IsVisible = true;
            pg.IsVisible = false;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            products.Add
            (
                new ProductModel(4, "Product 4", 4300, "lol petite description", App.ApiAddress + "/Product/GetPhoto/2")
            );
        }
    }
}
