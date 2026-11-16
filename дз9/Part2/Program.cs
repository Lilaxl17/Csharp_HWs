using System.Text.Json;

// Часть 2: при каждом запуске загружаем сохранение (или создаём новое),
// прибавляем 10 монет и перезаписываем файл.
string savePath = Path.Combine(AppContext.BaseDirectory, "player.json");
var jsonOptions = new JsonSerializerOptions { WriteIndented = true };

PlayerSave player;
if (File.Exists(savePath))
{
    string json = File.ReadAllText(savePath);
    player = JsonSerializer.Deserialize<PlayerSave>(json)
        ?? throw new InvalidDataException("Сохранение пустое или повреждено.");
    Console.WriteLine("Найдено сохранение. Персонаж загружен.");
}
else
{
    player = new PlayerSave
    {
        Name = "Hero",
        Level = 5,
        Coins = 100,
        Inventory = new[] { "Sword", "Potion" },
        HasKey = false
    };
    Console.WriteLine("Сохранения нет. Создан новый персонаж.");
}

Console.WriteLine($"Монеты до начисления: {player.Coins}");
player.Coins += 10;

string updatedJson = JsonSerializer.Serialize(player, jsonOptions);
File.WriteAllText(savePath, updatedJson);

Console.WriteLine($"Имя: {player.Name}");
Console.WriteLine($"Уровень: {player.Level}");
Console.WriteLine($"Монеты после начисления: {player.Coins}");
Console.WriteLine($"Инвентарь: {string.Join(", ", player.Inventory)}");
Console.WriteLine($"Есть ключ: {player.HasKey}");
Console.WriteLine($"Сохранено в файл: {savePath}");

public class PlayerSave
{
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public int Coins { get; set; }
    public string[] Inventory { get; set; } = Array.Empty<string>();
    public bool HasKey { get; set; }
}
