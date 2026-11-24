using System;
using System.Collections.Generic;

namespace Task2_2;

public enum Rarity
{
    Common,
    Rare,
    Legendary
}

public interface IUsableItem
{
    string Name { get; }
    string Description { get; }
    Rarity Rarity { get; }
    void Use(Player player);
}

public class Player
{
    public string Name { get; }
    public int HP { get; private set; }
    public int Damage { get; private set; }
    public bool IsDead => HP <= 0;

    public Player(string name, int hp, int damage)
    {
        Name = name;
        HP = hp;
        Damage = damage;
    }

    public void Heal(int amount)
    {
        HP += amount;
    }

    public void IncreaseDamage(int amount)
    {
        Damage += amount;
    }

    public void TakeDamage(int amount)
    {
        HP = Math.Max(0, HP - amount);
    }
}

public class HealthPotion : IUsableItem
{
    public string Name => "Зелье здоровья";
    public string Description => "Восстанавливает 50 HP";
    public Rarity Rarity => Task2_2.Rarity.Common;

    public void Use(Player player)
    {
        player.Heal(50);
        Console.WriteLine("Вы выпили зелье и восстановили 50 HP.");
    }
}

public class Sword : IUsableItem
{
    public string Name => "Меч";
    public string Description => "Увеличивает урон на 10";
    public Rarity Rarity => Task2_2.Rarity.Rare;

    public void Use(Player player)
    {
        player.IncreaseDamage(10);
        Console.WriteLine("Вы взяли меч: урон увеличен на 10.");
    }
}

public class AncientScroll : IUsableItem
{
    public string Name => "Древний свиток";
    public string Description => "Наносит 20 урона, но даёт +30 к урону";
    public Rarity Rarity => Task2_2.Rarity.Legendary;

    public void Use(Player player)
    {
        player.TakeDamage(20);
        player.IncreaseDamage(30);
        Console.WriteLine("Свиток проклял вас: -20 HP, но урон увеличен на 30.");
    }
}

public class Program
{
    public static void Main()
    {
        Player player = new Player("Герой", 100, 15);

        List<IUsableItem> items = new List<IUsableItem>
        {
            new HealthPotion(),
            new Sword(),
            new AncientScroll()
        };

        Console.WriteLine($"Начальные характеристики {player.Name}: HP = {player.HP}, урон = {player.Damage}");
        Console.WriteLine();

        foreach (IUsableItem item in items)
        {
            Console.WriteLine($"Предмет: {item.Name} ({item.Rarity}). {item.Description}");
            item.Use(player);
            Console.WriteLine();
        }

        Console.WriteLine($"Итоговые характеристики {player.Name}:");
        Console.WriteLine($"HP: {player.HP}");
        Console.WriteLine($"Урон: {player.Damage}");
        Console.WriteLine($"Состояние: {(player.IsDead ? "мёртв" : "жив")}");
    }
}
