using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Кольори: 0 - червоний, 1 - зелений, 2 - синій");

        Console.Write("Введіть кількість червоних їжачків: ");
        int red = int.Parse(Console.ReadLine());

        Console.Write("Введіть кількість зелених їжачків: ");
        int green = int.Parse(Console.ReadLine());

        Console.Write("Введіть кількість синіх їжачків: ");
        int blue = int.Parse(Console.ReadLine());

        Console.Write("Введіть бажаний колір (0-червоний, 1-зелений, 2-синій): ");
        int targetColor = int.Parse(Console.ReadLine());

        int result = SolveHedgehogs(red, green, blue, targetColor);

        Console.WriteLine();
        if (result == -1)
        {
            Console.WriteLine("-1");
        }
        else
        {
            Console.WriteLine($"Мінімальна кількість зустрічей: {result}");
        }

        Console.ReadKey();
    }

    static int SolveHedgehogs(int red, int green, int blue, int targetColor)
    {
        if (targetColor < 0 || targetColor > 2)
            return -1;

        long[] counts = { red, green, blue };
        long total = red + green + blue;

        // Якщо вже всі потрібного кольору
        if (counts[targetColor] == total)
            return 0;

        // Якщо всі їжачки одного кольору але не того що потрібно
        int nonZeroColors = 0;
        for (int i = 0; i < 3; i++)
        {
            if (counts[i] > 0) nonZeroColors++;
        }
        if (nonZeroColors <= 1)
            return -1;

        // Перевірка через інваріанти модуля 3
        if (!CanReachTarget(counts[0], counts[1], counts[2], targetColor, total))
            return -1;

        // Математичне обчислення мінімальної кількості зустрічей
        return CalculateMinMeetings(counts, targetColor, total);
    }

    static bool CanReachTarget(long red, long green, long blue, int targetColor, long total)
    {
        // Інваріанти за модулем 3
        long mod1 = Mod3(red - green);
        long mod2 = Mod3(green - blue);
        long mod3 = Mod3(blue - red);

        // Цільовий стан
        long[] target = new long[3];
        target[targetColor] = total;

        long targetMod1 = Mod3(target[0] - target[1]);
        long targetMod2 = Mod3(target[1] - target[2]);
        long targetMod3 = Mod3(target[2] - target[0]);

        return mod1 == targetMod1 && mod2 == targetMod2 && mod3 == targetMod3;
    }

    static long Mod3(long x)
    {
        long result = x % 3;
        return result < 0 ? result + 3 : result;
    }

    static int CalculateMinMeetings(long[] counts, int targetColor, long total)
    {
        // Індекси інших кольорів
        int other1 = (targetColor + 1) % 3;
        int other2 = (targetColor + 2) % 3;

        long target = counts[targetColor];
        long need1 = counts[other1];
        long need2 = counts[other2];

        // Мінімальна кількість зустрічей = сума їжачків не цільового кольору
        long meetings = need1 + need2;

        // Перевіряємо чи не перевищуємо int.MaxValue
        if (meetings > int.MaxValue)
            return -1;

        return (int)meetings;
    }
}
