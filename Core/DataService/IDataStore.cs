using MyCart.Models.Ecommerce;
using MyCart.Core.Models.Ecommerce;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyCart.Services
{
    public interface IDataStore
    {
        Task<bool> AddItem(string pid);
        Task<bool> RemoveProduct(string pid);
        Task<bool> ClearProducts();
        Task<IEnumerable<string>> GetItems();
        Task<bool> ValidateUser(string email, string password);
        List<Product> GetProducts();
        List<MainCategory> GetCategories();
    }
}