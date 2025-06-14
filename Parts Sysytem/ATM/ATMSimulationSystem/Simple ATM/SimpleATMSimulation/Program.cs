using System;

namespace SimpleATMSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount("123456", 1000);

            bool running = true;

            Console.WriteLine("Welcome to the Simple ATM Simulation\n");

            while (running)
            {
                Console.WriteLine("ATM MENU:");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Exit");
                Console.Write("Select option: ");
                string option = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    switch (option)
                    {
                        case "1":
                            Console.WriteLine($"Current Balance: {account.Balance:C}\n");
                            break;

                        case "2":
                            Console.Write("Enter amount to deposit: ");
                            decimal deposit = decimal.Parse(Console.ReadLine());
                            account.Deposit(deposit);
                            Console.WriteLine("Deposit successful.\n");
                            break;

                        case "3":
                            Console.Write("Enter amount to withdraw: ");
                            decimal withdraw = decimal.Parse(Console.ReadLine());

                            if (account.Withdraw(withdraw))
                                Console.WriteLine("Withdrawal successful.\n");
                            else
                                Console.WriteLine("Insufficient funds.\n");
                            break;

                        case "4":
                            Console.WriteLine("Thank you for using the ATM. Goodbye!");
                            running = false;
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please try again.\n");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}\n");
                }
            }
        }
    }
}
    