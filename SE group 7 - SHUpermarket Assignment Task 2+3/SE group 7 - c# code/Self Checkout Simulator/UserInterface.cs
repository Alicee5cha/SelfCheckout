using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace Self_Checkout_Simulator
{
    public partial class UserInterface : Form
    {
        // Attributes
        SelfCheckout selfCheckout;
        BarcodeScanner barcodeScanner;
        BaggingAreaScale baggingAreaScale;
        LooseItemScale looseItemScale;
        ScannedProducts scannedProducts;

        // Constructor
        public UserInterface()
        {
            InitializeComponent();

            // NOTE: This is where we set up all the objects,
            // and create the various relationships between them.

            baggingAreaScale = new BaggingAreaScale();
            scannedProducts = new ScannedProducts();
            barcodeScanner = new BarcodeScanner();
            looseItemScale = new LooseItemScale();
            selfCheckout = new SelfCheckout(baggingAreaScale, scannedProducts, looseItemScale);
            barcodeScanner.LinkToSelfCheckout(selfCheckout);
            baggingAreaScale.LinkToSelfCheckout(selfCheckout);
            looseItemScale.LinkToSelfCheckout(selfCheckout);

            UpdateDisplay();
            btnUserScansBarcodeProduct.Enabled = true;
            btnUserSelectsLooseProduct.Enabled = true;

            btnUserWeighsLooseProduct.Enabled = false;
            btnUserPutsProductInBaggingAreaCorrect.Enabled = false;
            btnUserPutsProductInBaggingAreaIncorrect.Enabled = false;
            btnUserChooseToPay.Enabled = false;
            btnAdminOverridesWeight.Enabled = false;
        }

        // Operations
        private void UserScansProduct(object sender, EventArgs e)
        {
            btnUserScansBarcodeProduct.Enabled = false;
            btnUserPutsProductInBaggingAreaCorrect.Enabled = true;
            btnUserPutsProductInBaggingAreaIncorrect.Enabled = true;
            btnUserSelectsLooseProduct.Enabled = false;
            btnUserWeighsLooseProduct.Enabled = false;
            btnUserChooseToPay.Enabled = false;
            btnAdminOverridesWeight.Enabled = false;

            barcodeScanner.BarcodeDetected();
            UpdateDisplay();
        }

        private void UserPutsProductInBaggingAreaCorrect(object sender, EventArgs e)
        {
            btnUserScansBarcodeProduct.Enabled = true;
            btnUserPutsProductInBaggingAreaCorrect.Enabled = false;
            btnUserPutsProductInBaggingAreaIncorrect.Enabled = false;
            btnUserSelectsLooseProduct.Enabled = true;
            btnUserWeighsLooseProduct.Enabled = false;
            btnUserChooseToPay.Enabled = true;
            btnAdminOverridesWeight.Enabled = false;

            // NOTE: we use the correct item weight here
            baggingAreaScale.WeightChangeDetected(scannedProducts.CalculateWeight());
            UpdateDisplay();
        }

        private void UserPutsProductInBaggingAreaIncorrect(object sender, EventArgs e)
        {
            btnUserScansBarcodeProduct.Enabled = false;
            btnUserPutsProductInBaggingAreaCorrect.Enabled = false;
            btnUserPutsProductInBaggingAreaIncorrect.Enabled = false;
            btnUserSelectsLooseProduct.Enabled = false;
            btnUserWeighsLooseProduct.Enabled = false;
            btnUserChooseToPay.Enabled = false;
            btnAdminOverridesWeight.Enabled = true;

            // NOTE: We are pretending to put down an item with the wrong weight.
            // To simulate this we'll use a random number, here's one for you to use.
            int weight = new Random().Next(20, 100);
          
            baggingAreaScale.WeightChangeDetected(weight);
            UpdateDisplay();
        }

        private void UserSelectsALooseProduct(object sender, EventArgs e)
        {
            btnUserScansBarcodeProduct.Enabled = false;
            btnUserPutsProductInBaggingAreaCorrect.Enabled = false;
            btnUserPutsProductInBaggingAreaIncorrect.Enabled = false;
            btnUserSelectsLooseProduct.Enabled = false;
            btnUserWeighsLooseProduct.Enabled = true;
            btnUserChooseToPay.Enabled = false;
            btnAdminOverridesWeight.Enabled = false;

            selfCheckout.LooseProductSelected();
            UpdateDisplay();
        }

        private void UserWeighsALooseProduct(object sender, EventArgs e)
        {
            btnUserScansBarcodeProduct.Enabled = false;
            btnUserPutsProductInBaggingAreaCorrect.Enabled = true;
            btnUserPutsProductInBaggingAreaIncorrect.Enabled = true;
            btnUserSelectsLooseProduct.Enabled = false;
            btnUserWeighsLooseProduct.Enabled = false;
            btnUserChooseToPay.Enabled = false;
            btnAdminOverridesWeight.Enabled = false;

            // NOTE: We are pretending to weigh a banana or whatever here.
            // To simulate this we'll use a random number, here's one for you to use.
            int weight = new Random().Next(20, 100);
            
            looseItemScale.WeightChangeDetected(weight);
            looseItemScale.Disable();
            UpdateDisplay();
        }

        private void AdminOverridesWeight(object sender, EventArgs e)
        {
            btnUserScansBarcodeProduct.Enabled = true;
            btnUserPutsProductInBaggingAreaCorrect.Enabled = false;
            btnUserPutsProductInBaggingAreaIncorrect.Enabled = false;
            btnUserSelectsLooseProduct.Enabled = true;
            btnUserWeighsLooseProduct.Enabled = false;
            btnUserChooseToPay.Enabled = true;
            btnAdminOverridesWeight.Enabled = false;

            selfCheckout.AdminOverideWeight();
            UpdateDisplay();
        }

        private void UserChoosesToPay(object sender, EventArgs e)
        {
            btnUserScansBarcodeProduct.Enabled = true;
            btnUserPutsProductInBaggingAreaCorrect.Enabled = false;
            btnUserPutsProductInBaggingAreaIncorrect.Enabled = false;
            btnUserSelectsLooseProduct.Enabled = true;
            btnUserWeighsLooseProduct.Enabled = false;
            btnUserChooseToPay.Enabled = false;
            btnAdminOverridesWeight.Enabled = false;

            selfCheckout.UserPaid();
            UpdateDisplay();
        }

        void UpdateDisplay()
        { 
            lblScreen.Text = selfCheckout.GetPromptForUser();
            lbBasket.Items.Clear();
            foreach (Product p in (scannedProducts.GetProducts()))
            {
                string display =  p.CalculatePrice().ToString() +" pence" + " - " + p.GetName();
                lbBasket.Items.Add(display);
            }
            lblBaggingAreaExpectedWeight.Text = baggingAreaScale.GetExpectedWeight().ToString();
            lblBaggingAreaCurrentWeight.Text = baggingAreaScale.GetCurrentWeight().ToString();
            lblTotalPrice.Text = scannedProducts.CalculatePrice().ToString();
        }
    }
}