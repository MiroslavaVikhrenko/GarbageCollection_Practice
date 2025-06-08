using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20250607_Task3
{
    public class DemoResource : IDisposable
    {
        private byte[] largeBuffer; // Simulate large memory usage

        // Constructor
        public DemoResource()
        {
            largeBuffer = new byte[10 * 1024 * 1024]; // Allocate 10 MB buffer
            Console.WriteLine("DemoResource created with 10 MB buffer.");
        }

        // Dispose method to clean up the resource
        public void Dispose()
        {
            largeBuffer = null; // Release the buffer
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("DemoResource.Dispose() called. Buffer released.");
            Console.ResetColor();
        }

        // Finalizer in case Dispose is not called (not recommended, just for demo)
        ~DemoResource()
        {
            Console.WriteLine("Finalizer called for DemoResource (GC cleanup).");
        }
    }
}
