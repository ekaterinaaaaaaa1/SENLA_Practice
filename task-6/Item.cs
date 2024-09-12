namespace task_6
{
    /// <summary>
    /// Represent the item of the backpack.
    /// </summary>
    public class Item
    {
        private string _name;
        private int _volume;
        private int _value;

        public Item(string name, int volume, int value)
        {
            _name = name;
            _volume = volume;
            _value = value;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Volume
        {
            get { return _volume; }
            set
            {
                if (value > 0)
                _volume = value;
            }
        }

        public int Value
        {
            get { return _value; }
            set
            {
                if (value > 0)
                _value = value;
            }
        }
    }
}
