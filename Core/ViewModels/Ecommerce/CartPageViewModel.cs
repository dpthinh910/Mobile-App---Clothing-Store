using MyCart.Models.Ecommerce;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Windows.Input;
using MyCart.Core.Helper;
using MyCart.Core.Services;

namespace MyCart.ViewModels.Ecommerce
{
    /// <summary>
    /// ViewModel for cart page.
    /// </summary>
    public class CartPageViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<Product> cartDetails;

        private double totalPrice;

        private double discountPrice;

        private double discountPercent;

        private double percent;

        private ObservableCollection<Product> products;

        private Command placeOrderCommand;

        private Command notificationCommand;

        private Command addToCartCommand;

        private Command saveForLaterCommand;

        private Command removeCommand;

        private Command quantitySelectedCommand;

        private Command variantSelectedCommand;

        private Command applyCouponCommand;

        #endregion

        #region Public properties
     
        public ObservableCollection<Product> CartDetails
        {
            get
            {
                return this.cartDetails;
            }

            set
            {
                SetProperty(ref cartDetails, value);
            }
        }

        public double TotalPrice
        {
            get
            {
                return this.totalPrice;
            }

            set
            {
                SetProperty(ref totalPrice, value);
            }
        }

        public double DiscountPrice
        {
            get
            {
                return this.discountPrice;
            }

            set
            {
                SetProperty(ref discountPrice, value);
            }
        }

        public double DiscountPercent
        {
            get
            {
                return this.discountPercent;
            }

            set
            {
                SetProperty(ref discountPercent, value);
            }
        }

        public ObservableCollection<Product> Products
        {
            get
            {
                return this.products;
            }
            set
            {
                SetProperty(ref products, value);
                GetProducts(Products);
                UpdatePrice();
            }
        }

        #endregion

        INavigationService navigationService;

        public CartPageViewModel(INavigationService navigation)
        {
            navigationService = navigation;
            FetchCartList();
        }

        private async void FetchCartList()
        {
            Products = await GetCartList();
        }

        #region Command

        public Command PlaceOrderCommand
        {
            get { return this.placeOrderCommand ?? (this.placeOrderCommand = new Command(this.PlaceOrderClicked)); }
        }

        public Command NotificationCommand
        {
            get { return this.notificationCommand ?? (this.notificationCommand = new Command(this.NotificationClicked)); }
        }

        public Command AddToCartCommand
        {
            get { return this.addToCartCommand ?? (this.addToCartCommand = new Command(this.AddToCartClicked)); }
        }

        public Command SaveForLaterCommand
        {
            get { return this.saveForLaterCommand ?? (this.saveForLaterCommand = new Command(this.SaveForLaterClicked)); }
        }

        public Command RemoveCommand
        {
            get { return this.removeCommand ?? (this.removeCommand = new Command(this.RemoveClicked)); }
        }

        public Command QuantitySelectedCommand
        {
            get { return this.quantitySelectedCommand ?? (this.quantitySelectedCommand = new Command(this.QuantitySelected)); }
        }

        public Command VariantSelectedCommand
        {
            get { return this.variantSelectedCommand ?? (this.variantSelectedCommand = new Command(this.VariantSelected)); }
        }

        public Command ApplyCouponCommand
        {
            get { return this.applyCouponCommand ?? (this.applyCouponCommand = new Command(this.ApplyCouponClicked)); }
        }

        #endregion

        #region Methods

        private async void PlaceOrderClicked(object obj)
        {
            
            navigationService.NavigateTo(typeof(CheckoutPageViewModel), string.Empty, string.Empty);
        }
        private void NotificationClicked(object obj)
        {
           
        }

        private void AddToCartClicked(object obj)
        {
            
        }
   
        private void SaveForLaterClicked(object obj)
        {
            
        }
  
        private async void RemoveClicked(object obj)
        {
            Product product = obj as Product;

            Products.Remove(product);

            await DataStore.RemoveProduct(product.Id.ToString());

            UpdatePrice();
        }
        private void QuantitySelected(object obj)
        {
            
        }

        private void VariantSelected(object obj)
        {
          
        }

        private void ApplyCouponClicked(object obj)
        {
           
        }

        private void GetProducts(ObservableCollection<Product> Products)
        {
            this.CartDetails = new ObservableCollection<Product>();
            if (Products != null && Products.Count > 0)
                this.CartDetails = Products;
        }

        private void UpdatePrice()
        {
            ResetPriceValue();

            if (this.CartDetails != null && this.CartDetails.Count > 0)
            {
                foreach (var cartDetail in this.CartDetails)
                {
                    if (cartDetail.TotalQuantity == 0)
                        cartDetail.TotalQuantity = 1;
                    this.TotalPrice += (cartDetail.ActualPrice * cartDetail.TotalQuantity);
                    this.DiscountPrice += (cartDetail.DiscountPrice * cartDetail.TotalQuantity);
                    this.percent += cartDetail.DiscountPercent;
                }

                this.DiscountPercent = this.percent > 0 ? this.percent / this.CartDetails.Count : 0;
            }
        }

        private void ResetPriceValue()
        {
            this.TotalPrice = 0;
            this.DiscountPercent = 0;
            this.DiscountPrice = 0;
            this.percent = 0;
        }

        #endregion
    }

}
