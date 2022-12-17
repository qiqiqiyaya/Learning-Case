namespace WeakReferenceTest
{
    // This class creates byte arrays to simulate data.
    public class Data
    {
        private byte[] _data;
        private string _name;

        public Data(int size)
        {
            _data = new byte[size * 1024];
            _name = size.ToString();
        }

        // Simple property.
        public string Name
        {
            get { return _name; }
        }
    }
}
