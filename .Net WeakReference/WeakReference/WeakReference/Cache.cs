
namespace WeakReferenceTest
{
    public class Cache
    {
        // Dictionary to contain the cache.
        static Dictionary<int, WeakReference> _cache;

        // Track the number of times an object is regenerated.
        int regenCount = 0;

        public Cache(int count)
        {
            _cache = new Dictionary<int, WeakReference>();

            // Add objects with a short weak reference to the cache.
            for (int i = 0; i < count; i++)
            {
                _cache.Add(i, new WeakReference(new Data(i), false));
            }
        }

        // Number of items in the cache.
        public int Count
        {
            get { return _cache.Count; }
        }

        // Number of times an object needs to be regenerated.
        public int RegenerationCount
        {
            get { return regenCount; }
        }

        // Retrieve a data object from the cache.
        public Data this[int index]
        {
            get
            {
                Data d = _cache[index].Target as Data;
                if (d == null)
                {
                    // If the object was reclaimed, generate a new one.
                    Console.WriteLine("Regenerate object at {0}: Yes", index);
                    d = new Data(index);
                    _cache[index].Target = d;
                    regenCount++;
                }
                else
                {
                    // Object was obtained with the weak reference.
                    Console.WriteLine("Regenerate object at {0}: No", index);
                }

                return d;
            }
        }
    }

}
