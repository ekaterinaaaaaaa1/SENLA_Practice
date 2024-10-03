namespace task_6
{
    using System.Linq;

    public class Program
    {
        public const int CAPACITY = 5;

        public static void Main()
        {
            Item[] items = {new Item("Учебник", 3, 500),
                            new Item("Ручка", 1, 250),
                            new Item("Карандаш", 1, 200),
                            new Item("Тетрадь", 2, 300),
                            new Item("Ножницы", 2, 350)
            };

            int numberOfItems = items.Length;
            Backpack[][] backpack = new Backpack[numberOfItems + 1][];

            for (int i = 0; i < numberOfItems + 1; i++)
            {
                backpack[i] = new Backpack[CAPACITY + 1];
                for (int j = 0; j < CAPACITY + 1; j++)
                {
                    if (i == 0 | j == 0)
                    {
                        backpack[i][j] = new Backpack(new Item[] { }, 0);
                    }
                    else if (i == 1)
                    {
                        if (items[0].Volume <= j)
                        {
                            backpack[i][j] = new Backpack(new Item[] { items[0] }, items[0].Price);
                        }
                        else
                        {
                            backpack[i][j] = new Backpack(new Item[] { }, 0);
                        }
                    }
                    else
                    {
                        if (items[i - 1].Volume > j)
                        {
                            backpack[i][j] = backpack[i - 1][j];
                        }
                        else
                        {
                            int optValue = items[i - 1].Price + backpack[i - 1][j - items[i - 1].Volume].Price;
                            if (backpack[i - 1][j].Price > optValue)
                            {
                                backpack[i][j] = backpack[i - 1][j];
                            }
                            else
                            {
                                backpack[i][j] = new Backpack((new Item[] { items[i - 1] }).Concat(backpack[i - 1][j - items[i - 1].Volume].Items).ToArray(), optValue);
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Суммарная ценность груза: {backpack[numberOfItems][CAPACITY].Price}");

            foreach (Item item in backpack[numberOfItems][CAPACITY].Items)
            {
                Console.WriteLine($"{item.Name}: вес - {item.Volume}, стоимость - {item.Price}");
            }
        }
    }
}