using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyCart.Models;
using MyCart.Services;
using System.Linq;
using MyCart.Models.Ecommerce;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyCart.Core.Services;

namespace MyCart.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public IDataStore DataStore => new LocalDataStore();

        public List<Product> AllProducts
        {
            get
            {
                return DataStore.GetProducts();
            }
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            throw new NotImplementedException();
        }

        protected async void ValidateCart()
        {
            //var email = Preferences.Get("email", "");
            //if (string.IsNullOrEmpty(email))
            //{
            //    return;
            //}

            var result = await DataStore.GetItems();

            var cartItems = result.ToList();

            if(cartItems != null && cartItems.Count != 0)
            {
                //TODO: Done navigation to cart
                //await Shell.Current.GoToAsync("cart");                 
            }
            else
            {
                //TODO: Done navigation to empty
                //await Shell.Current.GoToAsync("empty");                
            }
        }

        protected async Task<ObservableCollection<Product>> GetCartList()
        {
            //var email = Preferences.Get("email", "");
            //if (string.IsNullOrEmpty(email))
            //{
            //    return null;
            //}

            var result = await DataStore.GetItems();

            var cartList = new ObservableCollection<Product>();

            foreach (var productID in result)
            {
                var product = AllProducts.Where(item => item.Id.ToString() == productID).FirstOrDefault();
                if (product != null && !cartList.Contains(product))
                {
                    cartList.Add(product);
                }
                //TODO: Increase order count.
                continue;
            }
            return cartList;
        }

       
    }
}
