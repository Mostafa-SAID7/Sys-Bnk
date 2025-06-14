using System;

namespace SimpleATMSimulation
{
    public partial class BankAccount
    {
        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdraw amount must be greater than 0.");

            if (amount > Balance)
                return false;

            Balance -= amount;
            return true;
        }
    }
}
    