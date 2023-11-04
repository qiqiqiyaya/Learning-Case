// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualBasic;

Console.WriteLine("Hello, World!");

string str = "my string";

unsafe
{
    fixed (char* p = str)
    {
        // Print the value of *p:
        System.Console.WriteLine("Value at the location pointed to by p: {0:X}", *p);

        // Print the address stored in p:
        System.Console.WriteLine("The address stored in p: {0}", p->ToString());

        var aa = (long)p;
        Console.WriteLine(aa);
    }
}


var array = new int[] { 1, 2, 3, 1, 54, 6 };

var aaa = Marshal.UnsafeAddrOfPinnedArrayElement(array, 0);
Console.WriteLine(aaa);

Console.Read();