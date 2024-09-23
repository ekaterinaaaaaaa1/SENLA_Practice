namespace task_6
{
    /// <summary>
    /// Represent the item of the backpack.
    /// </summary>
    public class Item
    {
        private int _volume;
        private int _price;

        public Item(string name, int volume, int price)
        {
            Name = name;
            _volume = volume;
            _price = price;
        }

        public string Name { get; set; }

        public int Volume
        {
            get { return _volume; }
            set
            {
                if (value > 0)
                _volume = value;
            }
        }

        public int Price
        {
            get { return _price; }
            set
            {
                if (value > 0)
                _price = value;
            }
        }
    }
}
