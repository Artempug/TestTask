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

        // Розв'язання задачі
        int result = SolveHedgehogs(red, green, blue, targetColor);

        // Виведення результату
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
        // Перевірка вхідних даних
        if (targetColor < 0 || targetColor > 2)
            return -1;

        int total = red + green + blue;
        int[] counts = { red, green, blue };

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

        // Перевірка математичної можливості через інваріант модуля 3
        // При зустрічі різниці між кольорами змінюються на кратні 3
        if (!CanReachTarget(red, green, blue, targetColor, total))
            return -1;

        // Симулюємо процес зустрічей
        return SimulateMeetings(red, green, blue, targetColor);
    }

    static bool CanReachTarget(int red, int green, int blue, int targetColor, int total)
    {
        // Поточні залишки за модулем 3
        int mod1 = Mod3(red - green);
        int mod2 = Mod3(green - blue);
        int mod3 = Mod3(blue - red);

        // Цільові залишки всі їжачки одного кольору
        int[] target = new int[3];
        target[targetColor] = total;

        int targetMod1 = Mod3(target[0] - target[1]);
        int targetMod2 = Mod3(target[1] - target[2]);
        int targetMod3 = Mod3(target[2] - target[0]);

        return mod1 == targetMod1 && mod2 == targetMod2 && mod3 == targetMod3;
    }

    static int Mod3(int x)
    {
        int result = x % 3;
        return result < 0 ? result + 3 : result;
    }

    static int SimulateMeetings(int red, int green, int blue, int targetColor)
    {
        int[] current = { red, green, blue };
        int meetings = 0;
        int total = red + green + blue;

        while (current[targetColor] < total)
        {
            // Знаходимо найкращу пару для зустрічі
            int bestI = -1, bestJ = -1;
            int maxProfit = -1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = i + 1; j < 3; j++)
                {
                    if (current[i] > 0 && current[j] > 0)
                    {
                        int newColor = GetThirdColor(i, j);
                        int profit = CalculateProfit(i, j, newColor, targetColor);

                        if (profit > maxProfit)
                        {
                            maxProfit = profit;
                            bestI = i;
                            bestJ = j;
                        }
                    }
                }
            }

            if (bestI == -1) return -1; // Не можемо продовжити

            // Виконуємо зустріч
            current[bestI]--;
            current[bestJ]--;
            current[GetThirdColor(bestI, bestJ)] += 2;
            meetings++;

            
            if (meetings > total * 2) return -1;
        }

        return meetings;
    }

    static int GetThirdColor(int color1, int color2)
    {
        // 0+1+2 = 3, тому третій колір = 3 - перший - другий
        return 3 - color1 - color2;
    }

    static int CalculateProfit(int color1, int color2, int newColor, int targetColor)
    {
        // Визначаємо користь від зустрічі
        if (newColor == targetColor)
            return 3; // Найкраще  створюємо цільовий колір
        else if (color1 == targetColor || color2 == targetColor)
            return 0; // Погано  втрачаємо цільовий колір
        else
            return 1; // Добре  зменшуємо некільові кольори
    }
}
