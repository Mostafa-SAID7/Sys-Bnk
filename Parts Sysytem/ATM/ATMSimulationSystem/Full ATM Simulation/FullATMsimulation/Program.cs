using System;
using System.Collections.Generic;

class Program
{
    static Dictionary<string, User> users;

    static void Main(string[] args)
    {
        users = DataManager.LoadUsers();

        while (true)
        {
            Console.WriteLine("\n=== Welcome to the Console ATM ===");
            Console.Write("Enter card number (or type 'exit' to quit): ");
            string card = Console.ReadLine();

            if (card?.ToLower() == "exit")
            {
                Console.WriteLine("Goodbye!");
                break;
            }

            Console.Write("Enter PIN: ");
            string pin = ReadMaskedInput();

            if (card == "admin" && pin == "admin123")
            {
                AdminMenu.Show(users);
            }
            else if (users.TryGetValue(card, out var user))
            {
                if (user.IsLocked())
                {
                    var timeLeft = user.TimeUntilUnlock();
                    Console.WriteLine($"Account locked. Try again in {timeLeft?.Minutes}m {timeLeft?.Seconds}s.");
                    continue;
                }

                if (user.Pin == pin)
                {
                    user.FailedAttempts = 0;
                    user.LockoutTime = null;
                    DataManager.SaveUsers(users);
                    UserMenu.Show(user, users);
                }
                else
                {
                    user.FailedAttempts++;
                    if (user.FailedAttempts >= 3)
                    {
                        user.LockoutTime = DateTime.Now;
                        Console.WriteLine("Too many failed attempts. Account locked for 5 minutes.");
                    }
                    else
                    {
                        Console.WriteLine($"Incorrect PIN. Attempts left: {3 - user.FailedAttempts}");
                    }
                    DataManager.SaveUsers(users);
                }
            }
            else
            {
                Console.WriteLine("Card not recognized.");
            }
        }
    }

    static string ReadMaskedInput()
    {
        string input = "";
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                input = input[..^1];
                Console.Write("\b \b");
            }
            else if (!char.IsControl(key.KeyChar))
            {
                input += key.KeyChar;
                Console.Write("*");
            }
        } while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();
        return input;
    }
}