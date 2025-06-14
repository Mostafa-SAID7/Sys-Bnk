using System;

namespace SimpleATMSimulation
{
    public partial class BankAccount
    {
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than 0.");

            Balance += amount;
        }
    }
}
    