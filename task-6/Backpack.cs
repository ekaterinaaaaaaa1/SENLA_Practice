namespace task_6
{
    /// <summary>
    /// Represent a backpack with items and value.
    /// </summary>
    public class Backpack
    {
        private Item[] _items;
        private int _value;

        public Backpack(Item[] items, int value)
        {
            _items = items;
            _value = value;
        }

        public Item[] Items { get { return _items; } }
        public int Value { get { return _value; } }
    }
}
