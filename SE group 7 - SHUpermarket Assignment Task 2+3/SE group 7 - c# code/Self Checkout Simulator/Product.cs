using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_Checkout_Simulator
{
    abstract class Product
    {
        // Attributes
        protected int barcode;
        protected string name;
        protected int weightInGrams;

        // need a constructor (like in the inheritance tutorial)
        public Product(int barcode, string name)
        {
            this.barcode = barcode;
            this.name = name;
        }
        
        // Operations
        public string GetName()
        {
            return name;
        }

        public int GetBarcode()
        {
            return barcode;
        }

        public abstract bool IsLooseProduct();

        public int GetWeight()
        {
            return weightInGrams;
        }

        public void SetWeight(int weightInGrams)
        {
            this.weightInGrams = weightInGrams;
        }

        public abstract int CalculatePrice();
    }

    class PackagedProduct : Product
    {
        // Attributes
        private int priceInPence;

        // Constructor
        public PackagedProduct(int barcode, string name, int priceInPence, int weightInGrams) : base(barcode, name)
        {
            this.priceInPence = priceInPence;
            this.weightInGrams = weightInGrams;
        }

        public override int CalculatePrice()
        {
            return priceInPence;
        }
        public override bool IsLooseProduct()
        {
            return false;
        }

        // Operations
    }

    class LooseProduct : Product
    {
        // Attributes
        private int pencePer100g;

        // Constructor
        public LooseProduct(int barcode, string name, int pencePer100g) : base(barcode, name)
        {
            this.pencePer100g = pencePer100g;
        }

        // Operations
        public int GetPencePer100g()
        {
            return pencePer100g;
        }

        public override int CalculatePrice()
        {
           return (pencePer100g* weightInGrams)/10;
        }

        public override bool IsLooseProduct()
        {
            return true;
        }
    }
}