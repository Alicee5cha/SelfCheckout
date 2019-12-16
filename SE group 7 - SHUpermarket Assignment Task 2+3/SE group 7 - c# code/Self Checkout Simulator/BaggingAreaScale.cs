using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_Checkout_Simulator
{
    class BaggingAreaScale
    {
        // Attributes
        private int weight;
        private int expected;
        private int allowedDifference;
        private SelfCheckout sc;

        // Operations
        public int GetCurrentWeight()
        {
            return weight;
        }

        public bool IsWeightOk()
        {
            if (expected == weight + allowedDifference)
            {
                return true;
            }
            else
            return false; 
        }

        public int GetExpectedWeight()
        {
            return expected;
        }

        public void SetExpectedWeight(int expected)
        {
            this.expected = expected;
        }

        public void OverrideWeight()
        {
            allowedDifference = weight - expected;
        }

        public void Reset()
        {
            expected = 0; 
            weight = 0;
            allowedDifference = 0;
        }

        public void LinkToSelfCheckout(SelfCheckout sc)
        {
            this.sc = sc;
        }

        // NOTE: In reality the difference wouldn't be passed in here, the
        //       scale would detect the change and notify the self checkout
        public void WeightChangeDetected(int difference)
        {
            weight = difference + weight;
            sc.BaggingAreaWeightChanged();
        }
    }
}