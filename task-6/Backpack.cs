namespace task_6
{
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
