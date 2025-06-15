using System;
using System.Collections.Generic;

public static class AdminMenu
{
    public static void Show(Dictionary<string, User> users)
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n=== Admin Menu ===");
            Console.WriteLine("1. View All Users");
            Console.WriteLine("2. Add User");
            Console.WriteLine("3. Delete User");
            Console.WriteLine("4. Unlock User");
            Console.WriteLine("5. Exit Admin Mode");
            Console.Write("Select option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    foreach (var user in users.Values)
                    {
                        string lockStatus = user.IsLocked() ? " (LOCKED)" : "";
                        Console.WriteLine($"Card: {user.CardNumber}, Balance: ${user.Balance:F2}{lockStatus}");
                    }
                    break;

                case "2":
                    Console.Write("Enter new card number: ");
                    string card = Console.ReadLine();
                    if (users.ContainsKey(card))
                    {
                        Console.WriteLine("User already exists.");
                        break;
                    }

                    Console.Write("Enter PIN: ");
                    string pin = Console.ReadLine();
                    Console.Write("Enter initial balance: ");
                    if (decimal.TryParse(Console.ReadLine(), out var balance))
                    {
                        users[card] = new User
                        {
                            CardNumber = card,
                            Pin = pin,
                            Balance = balance
                        };
                        Console.WriteLine("User added.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid balance.");
                    }
                    break;

                case "3":
                    Console.Write("Enter card number to delete: ");
                    string del = Console.ReadLine();
                    if (users.Remove(del))
                        Console.WriteLine("User deleted.");
                    else
                        Console.WriteLine("User not found.");
                    break;

                case "4":
                    Console.Write("Enter card number to unlock: ");
                    string unlock = Console.ReadLine();
                    if (users.TryGetValue(unlock, out var user))
                    {
                        user.FailedAttempts = 0;
                        user.LockoutTime = null;
                        Console.WriteLine("User unlocked.");
                    }
                    else
                    {
                        Console.WriteLine("User not found.");
                    }
                    break;

                case "5":
                    Console.WriteLine("Exiting admin mode...");
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            DataManager.SaveUsers(users);
        }
    }
}