using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using MyCart.Models.Ecommerce;
using System.Web;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using MyCart.Core.Helper;
using MyCart.Core.Services;
using System.Threading.Tasks;

namespace MyCart.ViewModels.Ecommerce
{
   
    public class CatalogPageViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<Category> filterOptions;

        private ObservableCollection<string> sortOptions;

        private Command addFavouriteCommand;

        private Command itemSelectedCommand;

        private Command sortCommand;

        private Command filterCommand;

        private Command addToCartCommand;

        private Command expandingCommand;

        public Command cardItemCommand;

        private string cartItemCount;

        INavigationService navigationService;

        IDialogService dialogService;

        #endregion

        #region Constructor

        public CatalogPageViewModel(INavigationService navigation, IDialogService dialogService, string selectedCategory)
        {
            navigationService = navigation;
            this.dialogService = dialogService;           
            SelectedCategory = selectedCategory;          
        }

        #endregion

        #region Public properties

        public List<Product> Products { get; set; }

        private string selectedItem = "Watches";

        public string SelectedCategory
        {
            get { return selectedItem; }
            set
            {
                ValidateCategory(value);
            }
        }

        private async Task ValidateCategory(string value)
        {
            if (SetProperty(ref selectedItem, value))
            {
                Products = AllProducts.Where(item => item.Category.ToLower() == selectedItem.ToLower()).ToList();
                if (Products.Count == 0)
                {
                    await dialogService.Show("Nothing to show", "In this demo, only watch products are loaded.", "Close");
                    navigationService.NavigateBack();

                    return;
                }
                var items = await GetCartList();
                CartItemCount = items.Count.ToString();
            }
        }

        public ObservableCollection<Category> FilterOptions
        {
            get
            {
                return this.filterOptions;
            }

            set
            {
                SetProperty(ref filterOptions, value);
            }
        }

        public ObservableCollection<string> SortOptions
        {
            get
            {
                return this.sortOptions;
            }

            set
            {
                SetProperty(ref sortOptions, value);
            }
        }

        public string CartItemCount
        {
            get
            {
                return this.cartItemCount;
            }
            set
            {
                SetProperty(ref cartItemCount, value);
            }
        }

        #endregion

        #region Methods
        private void ItemSelected(object attachedObject)
        {
            Product product = attachedObject as Product;

            if (product == null && attachedObject is string)
            {
                navigationService.NavigateTo(typeof(DetailPageViewModel), "selectedProduct", attachedObject as string);
                return;
            }

            navigationService.NavigateTo(typeof(DetailPageViewModel), "selectedProduct", product.Id.ToString());
        }

        private void SortClicked(object attachedObject)
        {
            
        }

        private void FilterClicked(object obj)
        {
            
        }

     
        private void AddFavouriteClicked(object obj)
        {
            if (obj is Product product)
                product.IsFavourite = !product.IsFavourite;
        }

        private void AddToCartClicked(object obj)
        {
            navigationService.NavigateTo(typeof(CartPageViewModel), string.Empty, string.Empty);
        }
        private void ExpanderClicked(object obj)
        {           
        }
       
        private void CartClicked(object obj)
        {
            navigationService.NavigateTo(typeof(CartPageViewModel), string.Empty, string.Empty);
        }

        #endregion
    }
}