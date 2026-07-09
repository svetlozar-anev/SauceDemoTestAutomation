// <copyright file="DashboardSteps.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1600
namespace SauceDemo.Tests.Steps
{
    using SauceDemo.Core.TestData;
    using SauceDemo.UI.Components;

    public class DashboardSteps
    {
        private readonly DashboardComponent dashboard;
        
        public DashboardSteps(DashboardComponent dashboard)
        {
            this.dashboard = dashboard;
        }

        public void RefreshPage()
        {
            dashboard.RefreshPage();
            dashboard.WaitForDashboardToLoad();
        }

        public void AddProductToCart(int index)
        {
            dashboard.Products.AddItemToCart(index);
        }

        public void RemoveProductFromCart(int index)
        {
            this.dashboard.Products.ClickRemoveByIndex(index);
        }

        public int GetCartCount()
        {
            return dashboard.Header.GetCartCount();
        }

        public void OpenProductDetailPage(int index)
        {
            this.dashboard.Products.ClickTitleByIndex(index);
        }
        
        public IReadOnlyList<string> GetFirstProducts(int count)
        {
            return dashboard.Products.GetAllNames().Take(count).ToList();
        }
        
        public void AddProductToCart(string productName)
        {
            dashboard.Products.ClickAddToCartByName(productName);
        }

        public void SortProductsByNameAscending()
        {
            dashboard.Products.SelectSort(ProductSort.NameAscending);
        }

        public void SortProductsByNameDescending()
        {
            dashboard.Products.SelectSort(ProductSort.NameDescending);
        }
        
        public void SortProductsByPriceAscending()
        {
            dashboard.Products.SelectSort(ProductSort.PriceAscending);
        }
        
        public void SortProductsByPriceDescending()
        {
            dashboard.Products.SelectSort(ProductSort.PriceDescending);
        }

        public string AddFirstProductToCart()
        {
            var productName = this.dashboard.Products.GetAllNames().First();
            this.dashboard.Products.ClickAddToCartByName(productName);
            return productName;
        }

        public void OpenMenu()
        {
            dashboard.Menu.Open();
        }

        public void OpenCart()
        {
            dashboard.Header.ClickCartIcon();
        }

        public void Logout()
        {
            dashboard.Menu.Open();
            dashboard.Menu.Logout();
        }
    }
}
#pragma warning restore SA1600