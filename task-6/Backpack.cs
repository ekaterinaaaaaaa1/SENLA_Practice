namespace task_6
{
    /// <summary>
    /// Represent a backpack with items and value.
    /// </summary>
    public class Backpack
    {
        public Backpack(Item[] items, int price)
        {
            Items = items;
            Price = price;
        }

        public Item[] Items { get; }
        public int Price { get; }
    }
}
