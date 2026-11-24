using System;

namespace Task2_3;

public interface IDamageCalculator
{
    int CalculateDamage(int baseDamage, int targetArmor);
}

public class PhysicalDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        return Math.Max(1, baseDamage - targetArmor / 2);
    }
}

public class MagicDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = Math.Max(1, baseDamage - targetArmor / 4);

        if (targetArmor == 0)
            damage *= 2;

        return damage;
    }
}

public class FireDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = Math.Max(1, baseDamage - targetArmor / 10);

        if (baseDamage > 50)
            damage += 10;

        return damage;
    }
}

public class Unit
{
    public string Name { get; }
    public int HP { get; set; }
    public int Armor { get; }

    public Unit(string name, int hp, int armor)
    {
        Name = name;
        HP = hp;
        Armor = armor;
    }
}

public class DamageSystem
{
    private readonly IDamageCalculator _calculator;

    // Зависимость передаётся через конструктор.
    public DamageSystem(IDamageCalculator calculator)
    {
        _calculator = calculator;
    }

    public void ApplyDamage(Unit target, int baseDamage)
    {
        int damage = _calculator.CalculateDamage(baseDamage, target.Armor);
        target.HP = Math.Max(0, target.HP - damage);
        Console.WriteLine($"Юнит {target.Name} получил {damage} урона. Осталось HP: {target.HP}");
    }
}

public class Program
{
    public static void Main()
    {
        Unit unit = new Unit("Манекен", 200, 40);
        int baseDamage = 60;

        DamageSystem physicalSystem = new DamageSystem(new PhysicalDamageCalculator());
        DamageSystem magicSystem = new DamageSystem(new MagicDamageCalculator());
        DamageSystem fireSystem = new DamageSystem(new FireDamageCalculator());

        Console.WriteLine("Физический урон:");
        physicalSystem.ApplyDamage(unit, baseDamage);

        Console.WriteLine("Магический урон:");
        magicSystem.ApplyDamage(unit, baseDamage);

        Console.WriteLine("Огненный урон:");
        fireSystem.ApplyDamage(unit, baseDamage);

        Console.WriteLine();
        Console.WriteLine("Проверка магии против юнита без брони:");
        Unit unarmoredUnit = new Unit("Без брони", 200, 0);
        magicSystem.ApplyDamage(unarmoredUnit, baseDamage);
    }
}

// Как добавить ледяной урон, не меняя DamageSystem?
// Нужно создать новый класс IceDamageCalculator, реализовать IDamageCalculator
// и написать в нём свой CalculateDamage. После этого можно создать:
// new DamageSystem(new IceDamageCalculator()).
// DamageSystem менять не нужно — он зависит от интерфейса, а не от типа урона.
