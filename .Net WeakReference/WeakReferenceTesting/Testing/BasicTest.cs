using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    public class BasicTest
    {
        public static readonly WeakReference<ConcurrentDictionary<string, WeakReference<SemaphoreSlim>>>
            WeakReferenceTest = new(new ConcurrentDictionary<string, WeakReference<SemaphoreSlim>>());

        private string Id;

        public BasicTest()
        {
            Id = GetType().FullName;
            Concurrent.TryAdd(Id, new WeakReference<SemaphoreSlim>(new SemaphoreSlim(1)));
        }

        public SemaphoreSlim SemaphoreSlim
        {
            get
            {
                var se = Concurrent[Id];
                if (se.TryGetTarget(out var semaphore))
                {
                    return semaphore;
                }

                semaphore = new SemaphoreSlim(1);
                se.SetTarget(semaphore);
                return semaphore;
            }
        }

        public ConcurrentDictionary<string, WeakReference<SemaphoreSlim>> Concurrent => Get();

        private ConcurrentDictionary<string, WeakReference<SemaphoreSlim>> Get()
        {
            if (WeakReferenceTest.TryGetTarget(out var con))
            {
                return con;
            }

            con = new ConcurrentDictionary<string, WeakReference<SemaphoreSlim>>();
            WeakReferenceTest.SetTarget(con);
            return con;
        }
    }
}
