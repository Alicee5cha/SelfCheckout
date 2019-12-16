using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_Checkout_Simulator
{
    class ScannedProducts 
    {
        //Attributes
        public List<Product> products = new List<Product>();
       
        public ScannedProducts()
        {
            
        }

        // Operations
        public List<Product> GetProducts()
        {
            return products;
        }

        public int CalculateWeight() 
        {
            int totalWeight = 0;
            foreach (Product product in products)
            {
                totalWeight += (product.GetWeight());
            }
            return totalWeight;
        }

        public int CalculatePrice() 
        {
           int totalPrice = 0;
            foreach (Product product in products)
            {
                totalPrice += (product.CalculatePrice());
            }
            return totalPrice;
        }

        public void Reset()
        {
            products.Clear();
        }

        public void Add(Product p) 
        {
            products.Add(p);
        }

        public void Remove(Product p)
        {
            products.Remove(p);
        }

        public bool HasItems() 
        {
            if (products.Count() != 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}