using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarbageCollection_Practice
{
    public class LargeObjectManager
    {
        private List<byte[]> _objects;

        private readonly int _memoryLimit;

        public LargeObjectManager(int memoryLimit)
        {
            _objects = new List<byte[]>();
            _memoryLimit = memoryLimit;
        }

        public void AddLargeObject(int sizeMB)
        {
            _objects.Add(new byte[sizeMB * 1024 * 1024]);
        }

        private long GetCurrentMemoryUsage()
        {
            return Process.GetCurrentProcess().PrivateMemorySize64 / (1024 * 1024);
        }

        public void CheckLimits()
        {
            long currentMemory = GetCurrentMemoryUsage();
            Console.WriteLine($"Current memory usage is {currentMemory} MB.");

            if ( currentMemory >= _memoryLimit )
            {
                Console.WriteLine($"Current memory usage exceeds the limit of {_memoryLimit} MB. Cleaning...");

                GC.Collect();
            }

            long memoryAfter = GetCurrentMemoryUsage();
            Console.WriteLine($"Memory usage AFTER cleanup is {memoryAfter} MB.");
        }
    }
}
