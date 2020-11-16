using Prism.Mvvm;
using SalesWorkforce.MobileApp.Managers.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesWorkforce.MobileApp.Models
{
    public class PurchaseOrderProductItemModel : BindableBase
    {
        private ProductEntity _product;
        public ProductEntity Product
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }

        private double _quantity;
        public double Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }

    }
}
