using System;

namespace SimpleATMSimulation
{
    public partial class BankAccount
    {
        public decimal Balance
        {
            get => balance;
            private set
            {
                if (value < 0)
                    throw new InvalidOperationException("Balance cannot be negative.");
                balance = value;
            }
        }
    }
}
    