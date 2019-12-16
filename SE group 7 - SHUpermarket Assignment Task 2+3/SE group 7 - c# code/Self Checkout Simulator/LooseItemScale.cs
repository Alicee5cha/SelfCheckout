using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_Checkout_Simulator
{
    class LooseItemScale
    {
        // Attributes
        private bool enabled;
        private SelfCheckout sc;

        // Operations
        public void Enable()
        {
            this.enabled = true;
        }

        public void Disable()
        {
            this.enabled = false;
        }

        public bool IsEnabled()
        {
            return true;
        }

        public void LinkToSelfCheckout(SelfCheckout sc)
        {
            this.sc = sc;
        }

        // NOTE: In reality the weight wouldn't be passed in here, the
        //       scale would detect the change and notify the self checkout
        public void WeightChangeDetected(int weight)
        {
            sc.BaggingAreaWeightChanged();
        }
    }
}