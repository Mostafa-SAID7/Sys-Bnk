using System;
using System.Collections.Generic;

public static class UserMenu
{
    public static void Show(User user, Dictionary<string, User> users)
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n===== ATM Menu =====");
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. View Transaction History");
            Console.WriteLine("5. Logout");
            Console.Write("Choose option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine($"Current balance: ${user.Balance:F2}");
                    user.TransactionHistory.Add($"Checked balance: ${user.Balance:F2} [{DateTime.Now}]");
                    break;

                case "2":
                    Console.Write("Enter deposit amount: ");
                    if (decimal.TryParse(Console.ReadLine(), out var deposit) && deposit > 0)
                    {
                        user.Balance += deposit;
                        Console.WriteLine($"Deposited ${deposit:F2}");
                        user.TransactionHistory.Add($"Deposited: ${deposit:F2} [{DateTime.Now}]");
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount.");
                    }
                    break;

                case "3":
                    Console.Write("Enter withdrawal amount: ");
                    if (decimal.TryParse(Console.ReadLine(), out var withdraw) && withdraw > 0)
                    {
                        if (withdraw <= user.Balance)
                        {
                            user.Balance -= withdraw;
                            Console.WriteLine($"Withdrew ${withdraw:F2}");
                            user.TransactionHistory.Add($"Withdrew: ${withdraw:F2} [{DateTime.Now}]");
                        }
                        else
                        {
                            Console.WriteLine("Insufficient funds.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount.");
                    }
                    break;

                case "4":
                    Console.WriteLine("Transaction History:");
                    if (user.TransactionHistory.Count == 0)
                        Console.WriteLine("No transactions.");
                    else
                        foreach (var entry in user.TransactionHistory)
                            Console.WriteLine(entry);
                    break;

                case "5":
                    Console.WriteLine("Logging out...");
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            DataManager.SaveUsers(users);
        }
    }
}