using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class DataManager
{
    private static readonly string filePath = "users.json";

    public static Dictionary<string, User> LoadUsers()
    {
        if (!File.Exists(filePath)) return new();
        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<Dictionary<string, User>>(json) ?? new();
    }

    public static void SaveUsers(Dictionary<string, User> users)
    {
        string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }
}