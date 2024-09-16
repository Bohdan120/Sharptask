using System;
using System.Collections.Generic;

class HedgehogColorChange
{
    public static int MinMeetingsToUnifyColors(int[] population, int targetColor)
    {
        if (population[targetColor] == population[0] + population[1] + population[2])
            return 0;

        if ((population[0] == 0 && population[1] == 0) || (population[0] == 0 && population[2] == 0) || (population[1] == 0 && population[2] == 0))
            return -1;

        Queue<(int[] population, int steps)> queue = new Queue<(int[], int)>();
        HashSet<string> visited = new HashSet<string>();

        queue.Enqueue((new int[] { population[0], population[1], population[2] }, 0));
        visited.Add($"{population[0]},{population[1]},{population[2]}");

        while (queue.Count > 0)
        {
            var (currentPop, steps) = queue.Dequeue();

            if (currentPop[targetColor] == currentPop[0] + currentPop[1] + currentPop[2])
                return steps;

            for (int i = 0; i < 3; i++)
            {
                for (int j = i + 1; j < 3; j++)
                {
                    if (currentPop[i] > 0 && currentPop[j] > 0)
                    {
                        int[] newPop = (int[])currentPop.Clone();
                        newPop[i]--;
                        newPop[j]--;
                        newPop[3 - i - j] += 2; 

                        string newPopKey = $"{newPop[0]},{newPop[1]},{newPop[2]}";

                        if (!visited.Contains(newPopKey))
                        {
                            visited.Add(newPopKey);
                            queue.Enqueue((newPop, steps + 1));
                        }
                    }
                }
            }
        }

        return -1;
    }

    static void Main(string[] args)
    {
        int[] population = { 5, 5, 10 };
        int targetColor = 2;

        int result = MinMeetingsToUnifyColors(population, targetColor);

        Console.WriteLine(result);
    }
}
