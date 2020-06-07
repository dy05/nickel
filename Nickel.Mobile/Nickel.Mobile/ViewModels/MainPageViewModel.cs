using Nickel.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Nickel.Mobile.ViewModels
{
    public class MainPageViewModel
    {
        public ObservableCollection<ProductModel> Products { get; set; }

        public MainPageViewModel() {}

        public MainPageViewModel(ObservableCollection<ProductModel> products) 
        {
            Products = products;
        }
    }
}
