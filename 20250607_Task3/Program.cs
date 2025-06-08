namespace _20250607_Task3
{
    /*
     Создать любой класс и при завершении метода, который создавал объект класса, вызывать метод Dispose.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            UseResource(); 

            GC.Collect(); // Force garbage collection 
            GC.WaitForPendingFinalizers(); // Wait for finalizers to complete
            Console.ReadKey();
        }

        // Method to create and dispose of a resource manually
        static void UseResource()
        {
            Console.WriteLine("Entering UseResource()");

            DemoResource resource = new DemoResource(); // Create the resource
            // Do some work with the resource...
            Console.WriteLine("Using resource...");

            resource.Dispose(); // Explicitly release memory via Dispose()

            Console.WriteLine("Exiting UseResource()");
        }
    }
}
