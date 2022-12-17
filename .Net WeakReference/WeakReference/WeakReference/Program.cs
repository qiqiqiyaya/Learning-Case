// Create the cache.
using WeakReferenceTest;

int cacheSize = 50;
Random r = new Random();
Cache c = new Cache(cacheSize);

string DataName = "";
GC.Collect(0);

// Randomly access objects in the cache.
for (int i = 0; i < c.Count; i++)
{
    int index = r.Next(c.Count);

    // Access the object by getting a property value.
    DataName = c[index].Name;
    GC.Collect(0);
}
// Show results.
double regenPercent = c.RegenerationCount / (double)c.Count;
Console.WriteLine($"Cache size: {c.Count}, Regenerated: {regenPercent:P0}");

Console.Read();