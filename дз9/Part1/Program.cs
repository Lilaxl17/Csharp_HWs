using System.Text.Json;

// Часть 1: создаём персонажа, сохраняем его в JSON и читаем обратно.
PlayerSave originalPlayer = new PlayerSave
{
    Name = "Hero",
    Level = 5,
    Coins = 100
};

string savePath = Path.Combine(AppContext.BaseDirectory, "player.json");
var jsonOptions = new JsonSerializerOptions { WriteIndented = true };

string json = JsonSerializer.Serialize(originalPlayer, jsonOptions);
File.WriteAllText(savePath, json);
Console.WriteLine($"Сохранение создано: {savePath}");

string loadedJson = File.ReadAllText(savePath);
PlayerSave? loadedPlayer = JsonSerializer.Deserialize<PlayerSave>(loadedJson);

if (loadedPlayer == null)
{
    Console.WriteLine("Не удалось восстановить персонажа из JSON.");
    return;
}

Console.WriteLine($"Имя: {loadedPlayer.Name}");
Console.WriteLine($"Уровень: {loadedPlayer.Level}");
Console.WriteLine($"Монеты: {loadedPlayer.Coins}");

public class PlayerSave
{
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public int Coins { get; set; }
}
