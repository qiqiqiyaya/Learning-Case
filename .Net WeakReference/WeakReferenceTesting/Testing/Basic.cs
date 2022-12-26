using System.Collections.Concurrent;

namespace Testing
{
    public class Basic
    {
        private static readonly WeakReference<ConcurrentDictionary<string, SemaphoreSlim>> WeakReference =
            new(new ConcurrentDictionary<string, SemaphoreSlim>());
        

        private string Id;

        public Basic()
        {
            Id = GetType().FullName;
            Concurrent.TryAdd(Id, new SemaphoreSlim(1));
        }

        public SemaphoreSlim SemaphoreSlim => Get()[Id];

        public ConcurrentDictionary<string, SemaphoreSlim> Concurrent => Get();

        private ConcurrentDictionary<string, SemaphoreSlim> Get()
        {
            if (WeakReference.TryGetTarget(out var con))
            {
                return con;
            }

            con = new ConcurrentDictionary<string, SemaphoreSlim>();
            WeakReference.SetTarget(con);
            return con;
        }
    }
}
