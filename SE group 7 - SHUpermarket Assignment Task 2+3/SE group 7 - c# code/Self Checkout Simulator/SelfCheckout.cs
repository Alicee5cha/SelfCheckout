using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_Checkout_Simulator
{
    class SelfCheckout
    {
        // Attributes
        private Product currentProduct;
        BaggingAreaScale baggingArea;
        ScannedProducts scannedProducts;
        LooseItemScale looseItemScale;
        
        // Constructor
        public SelfCheckout(BaggingAreaScale baggingArea, ScannedProducts scannedProducts, LooseItemScale looseItemScale)
        {
            this.baggingArea = baggingArea;
            this.scannedProducts = scannedProducts;
            this.looseItemScale = looseItemScale;
        }

        // Operations
        public void LooseProductSelected()
        {
            currentProduct = ProductsDAO.GetRandomLooseProduct();
            looseItemScale.IsEnabled();
        }

        public void LooseItemAreaWeightChanged(int weightOfLooseItem)
        {
            currentProduct.SetWeight(weightOfLooseItem);
            scannedProducts.Add(currentProduct);
            int totalForLooseItem = scannedProducts.CalculateWeight();
            baggingArea.SetExpectedWeight(totalForLooseItem);
            looseItemScale.Disable();
        }

        public void BarcodeWasScanned(int barcode)
        {
            currentProduct = ProductsDAO.SearchUsingBarcode(barcode);
            scannedProducts.Add(currentProduct);
            int total = scannedProducts.CalculateWeight();
            baggingArea.SetExpectedWeight(total);
        }

        public void BaggingAreaWeightChanged()
        {
            currentProduct = null;
        }

        public void UserPaid()
        {
            baggingArea.Reset();
            scannedProducts.Reset();
        }

        public string GetPromptForUser()
        {
            if (currentProduct == null)
            {
                return "Scan an Item";
            }
            if (looseItemScale.IsEnabled())
            {
                return "Place your loose item on the scale";
            }
            if (!looseItemScale.IsEnabled())
            {
                return "Place your item in the bagging area.";
            }
            if (scannedProducts.HasItems())
            {
                return "Scan an item or pay";
            }
            else
                // TODO: Use the information we have to produce the correct message
                //       e.g. "Scan an item.", "Place item on scale.", etc.
                return "ERROR: Unknown state!";
        }

        public Product GetCurrentProduct()
        {
            return currentProduct;
        }

        public void AdminOverideWeight()
        {
            baggingArea.OverrideWeight();
        }
    }
}