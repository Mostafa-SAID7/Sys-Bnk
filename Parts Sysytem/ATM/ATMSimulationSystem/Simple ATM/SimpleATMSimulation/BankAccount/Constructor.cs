using System;

namespace SimpleATMSimulation
{
    public partial class BankAccount
    {
        public BankAccount(string accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber;

            if (initialBalance < 0)
                throw new ArgumentException("Initial balance must be non-negative.");

            Balance = initialBalance;
        }
    }
}
    