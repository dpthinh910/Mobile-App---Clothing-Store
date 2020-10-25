using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyCart.Models.About;
using MyCart.Core.Helper;

namespace MyCart.ViewModels.About
{
    public class AboutUsViewModel : ViewModelBase
    {
        #region Fields

        private string productDescription;

        private string productVersion;

        private string productIcon;

        private string cardsTopImage;

        #endregion

        #region Constructor
        public AboutUsViewModel()
        {
            ProductDescription =
                "Add something ABOUT US in this field.";
            ProductIcon = "swimsuit.jpeg";           
            CardsTopImage = "swimsuit.jpeg";

            this.ItemSelectedCommand = new Command(this.ItemSelected);
        }

        #endregion
        #region Event handler

        #endregion

        #region Properties

        public string CardsTopImage
        {
            get { return this.cardsTopImage; }

            set
            {
                SetProperty(ref cardsTopImage, value);
            }
        }
        public string ProductDescription
        {
            get { return this.productDescription; }

            set
            {
                SetProperty(ref productDescription, value);
            }
        }
        public string ProductIcon
        {
            get { return this.productIcon; }

            set
            {
                SetProperty(ref productIcon, value);
            }
        }
        public string ProductVersion
        {
            get { return this.productVersion; }

            set
            {
                SetProperty(ref productVersion, value);
            }
        }

        public ObservableCollection<AboutUsModel> EmployeeDetails { get; set; }

        public Command ItemSelectedCommand { get; set; }      

        #endregion

        #region Methods

        private void ItemSelected(object selectedItem)
        {         
        }

        #endregion
    }
}