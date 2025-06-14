using ATMApp.Domain.Models;

namespace ATMApp.Application.Services
{
    public class AccountService
    {
        private BankAccount account = new BankAccount { AccountNumber = "123456", Balance = 1000.00M };

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than 0.");
            account.Balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than 0.");
            if (amount > account.Balance)
                return false;
            account.Balance -= amount;
            return true;
        }

        public decimal GetBalance() => account.Balance;
    }
}
